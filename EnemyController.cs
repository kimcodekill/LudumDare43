using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;

    private SpriteRenderer spriteRender;

    protected virtual void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Hurt(float damage)
    {
        StartCoroutine(ColorHurt());
        health -= damage;
    }

    private void Die()
    {
        GameController.instance.targets--;
        GameObject.Destroy(gameObject);
    }

    protected void Flip(int scale)
    {
        Vector3 myScale = transform.localScale;
        myScale.x = scale;
        transform.localScale = myScale;
    }

    public IEnumerator ColorHurt()
    {
        spriteRender.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRender.color = Color.white;
    }
}
