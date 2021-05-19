using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAi : MonoBehaviour
{
    public enum facing { RIGHT, LEFT };
    [SerializeField]
    public facing dir = facing.RIGHT;
    public GameObject player;
    [Range(0.1f, 5.0f)]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var plSign = player.transform.position.x - transform.position.x;
        if (Mathf.Sign(plSign) > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            dir = facing.LEFT;
        }
        else if (Mathf.Sign(plSign) < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            dir = facing.RIGHT;
        }
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 0.05)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            player.GetComponent<Player_Controler>().TakeDamage(1);
        }
    }

    public void death()
    {
        Destroy(gameObject);
    }
}
