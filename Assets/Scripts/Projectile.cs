using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Player_Controler player;

    public float speed;

    protected Vector2 velocity;

    public Player_Controler.facing dir = Player_Controler.facing.RIGHT;

    protected CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        dir = player.dir;
        if (dir == Player_Controler.facing.RIGHT)
            velocity = new Vector2(speed, 0);
        else
            velocity = new Vector2(-speed, 0);

        //velocity.x += player.velocity.x;
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);

        //Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.05f, 0.05f), 0);
        //foreach (Collider2D hit in hits)
        //{
        //    if (hit == col)
        //        continue;

        //   
        //}
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
