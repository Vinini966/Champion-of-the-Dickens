using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject gm;
    [Tooltip("Check to make it a melee Pickup.")]
    public bool isMelee;
    [SerializeField]
    public GameManager.melee sword = GameManager.melee.BASIC;
    [SerializeField]
    public GameManager.thrown arrow = GameManager.thrown.BASIC;

    private void Start()
    {
        gm = GameObject.Find("Game Manager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(isMelee)
                gm.GetComponent<GameManager>().WeaponTypeSword(sword);
            else
                gm.GetComponent<GameManager>().WeaponTypeRanged(arrow);
            Destroy(gameObject);
        }
    }
}
