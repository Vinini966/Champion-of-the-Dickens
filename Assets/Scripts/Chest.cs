using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator anim;
    public bool isRandom;
    public GameObject pickUp;
    public Sprite[] meleeImage;
    public Sprite[] rangedImage;
    [Tooltip("Check to make it a melee Pickup.")]
    public bool isMelee;
    [SerializeField]
    public GameManager.melee sword = GameManager.melee.BASIC;
    [SerializeField]
    public GameManager.thrown arrow = GameManager.thrown.BASIC;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetTrigger("Open");
            
            print("OPEN");
        }
            
    }

    public void open()
    {
        GameObject tmp = Instantiate(pickUp, gameObject.transform.position, gameObject.transform.rotation);
        tmp.GetComponent<Pickup>().isMelee = isMelee;
        if (isMelee)
        {
            tmp.GetComponent<Pickup>().sword = sword;
            tmp.GetComponent<SpriteRenderer>().sprite = meleeImage[(int)sword];
        }
        else
        {
            tmp.GetComponent<Pickup>().arrow = arrow;
            tmp.GetComponent<SpriteRenderer>().sprite = rangedImage[(int)arrow];
        }
        
    }
}
