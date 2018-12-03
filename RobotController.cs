using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : EnemyController
{
    public float amplitude = 0.5f;
    public float frequency = 1;
    public float range;
    public float shotDelay;
    public Transform sight;
    public Transform frontCheck;
    public Transform shotPoint;
    public LayerMask groundMask;
    public LayerMask playerMask;
    public AudioClip laserClip;
    
    private LineRenderer shot;
    private AudioSource audioSource;

    private float floatY;
    private Vector3 tempPos;
    private int facing = -1;

    protected override void Start()
    {
        base.Start();

        shot = GetComponent<LineRenderer>();
        shot.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        bool blocked = Physics2D.Linecast(transform.position, frontCheck.position, groundMask);

        floatY = Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        if (blocked)
        {
            Flip(facing);
            facing *= -1;
        }

        RaycastHit2D aim = Physics2D.Linecast(shotPoint.position, shotPoint.position + (sight.position - shotPoint.position) * 100, groundMask + playerMask);

        shot.SetPositions(new Vector3[] { shotPoint.position, aim.point });

        if (!shot.enabled)
        {
            if (aim.collider.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Shoot());
            }
            else
            {
                transform.Translate(new Vector2(facing * speed, floatY) * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(new Vector2(0, floatY) * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Hurt(damage);
        }
    }

    IEnumerator Shoot()
    {
        shot.enabled = true;

        Vector2 endPos = Physics2D.Linecast(shotPoint.position, shotPoint.position + (sight.position - shotPoint.position) * range, groundMask).point;

        shot.SetPositions(new Vector3[] { shotPoint.position, endPos});

        yield return new WaitForSeconds(shotDelay);

        RaycastHit2D shotHit = Physics2D.Raycast(shotPoint.position, sight.position - shotPoint.position, range, playerMask);

        if (shotHit)
        {
            endPos = shotHit.point;

            GameObject hitObject = shotHit.collider.gameObject;

            if (hitObject.CompareTag("Player"))
            {
                hitObject.GetComponent<PlayerController>().Hurt(damage);
            }
        }
        else
        {
            endPos = Physics2D.Linecast(shotPoint.position, shotPoint.position + (sight.position - shotPoint.position) * range, groundMask).point;
        }

        audioSource.PlayOneShot(laserClip);

        shot.SetPositions(new Vector3[] { shotPoint.position, endPos });
        shot.widthMultiplier = .6f;

        yield return new WaitForSeconds(.1f);
        shot.widthMultiplier = .02f;
        shot.enabled = false;
    }
}
