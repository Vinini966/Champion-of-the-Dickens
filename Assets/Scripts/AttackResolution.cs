using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackResolution : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (collision.collider.tag == "Baddie")
        {
            BasicEne health = collision.collider.gameObject.GetComponent<BasicEne>();
            if(health == null)
            {
                collision.collider.gameObject.GetComponent<GhostAi>().speed = 0;
                collision.collider.gameObject.GetComponent<Animator>().SetTrigger("Death");
                return;
            }
            health.TakeDamage(1);
            if (health.health == 0)
            {
                health.dying = true;
                health.gameObject.GetComponent<Animator>().SetTrigger("Death");
                health.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            }
                
        }
    }
}
