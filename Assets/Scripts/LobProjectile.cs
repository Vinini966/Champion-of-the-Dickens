using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobProjectile : Projectile
{

    public Vector2 angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = new Vector2(1f, 1.8f);
        dir = player.dir;
        if (dir == Player_Controler.facing.RIGHT)
            velocity = new Vector2(speed, speed);
        else
            velocity = new Vector2(-speed, speed);

        velocity.x += player.velocity.x;
        velocity *= angle;
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit");
        if (coll.collider.tag == "Baddie")
        {
            BasicEne health = coll.collider.gameObject.GetComponent<BasicEne>();
            health.TakeDamage(1);
            if (health.health == 0)
            {
                health.dying = true;
                health.gameObject.GetComponent<Animator>().SetTrigger("Death");
                health.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
        Destroy(gameObject);

    }
}
