using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float damage;
    public float range;
    public Transform arm;
    public Transform shotPoint;
    public Transform reticle;
    public LayerMask targetMask;
    public LayerMask groundMask;
    public Transform[] groundChecks;


    [HideInInspector]
    public float time;

    private Rigidbody2D rb2d;
    private LineRenderer shot;
    private AudioSource audioSource;
    private SpriteRenderer spriteRender;

    // Use this for initialization
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        shot = GetComponent<LineRenderer>();
        shot.enabled = false;

        audioSource = GetComponent<AudioSource>();

        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(Grounded())
            {
                rb2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
        }

        arm.right = ((Vector2)reticle.position - (Vector2)shotPoint.position).normalized;

        transform.localScale = new Vector2(Mathf.Sign(arm.right.x), transform.localScale.y);
        arm.localScale = new Vector2(Mathf.Sign(arm.right.x), Mathf.Sign(arm.right.x));

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }

        Debug.DrawRay(shotPoint.position, reticle.position - shotPoint.position, Color.red);
        Debug.DrawLine(shotPoint.position, reticle.position, Color.green);
    }

    void FixedUpdate()
    {
        if(shot.isVisible) shot.enabled = false;

        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        rb2d.velocity = new Vector2(horizontalMovement * speed, rb2d.velocity.y);
    }

    void FireWeapon()
    {
        RaycastHit2D shotHit = Physics2D.Raycast(shotPoint.position, reticle.position - shotPoint.position, range, targetMask + groundMask);

        Vector2 endPos = Vector2.zero;

        if (shotHit)
        {
            endPos = shotHit.point;

            GameObject hitObject = shotHit.collider.gameObject;

            if (hitObject.CompareTag("Enemy"))
            {
                hitObject.GetComponent<EnemyController>().Hurt(damage);
            }
        }
        else
        {
            endPos = Physics2D.Linecast(shotPoint.position, shotPoint.position + (reticle.position - shotPoint.position) * range, groundMask).point;
        }
        shot.SetPositions(new Vector3[] {shotPoint.position, endPos});
        shot.enabled = true;

        audioSource.Play();
        
        Debug.DrawLine(shotPoint.position, shotPoint.position + (reticle.position - shotPoint.position) * range, Color.green, 5);
        Debug.DrawRay(shotPoint.position, reticle.position - shotPoint.position, Color.red, 5);

        time--;
    }

    public void Hurt(float damage)
    {
        StartCoroutine(ColorHurt());
        time -= damage;
    }

    private bool Grounded()
    {
        foreach(Transform groundCheck in groundChecks)
        {
            if (Physics2D.Linecast(transform.position, groundCheck.position, groundMask))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerator ColorHurt()
    {
        spriteRender.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRender.color = Color.white;
    }
}
