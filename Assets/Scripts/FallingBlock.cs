using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<BoxCollider2D>().enabled = false;
        }
            
    }
}
