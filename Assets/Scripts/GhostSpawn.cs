using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    public GameObject ghost;
    public GameObject leftSpawn;
    public GameObject rightSpawn;
    Timer spawnTime;
    [Range(0.1f, 5.0f)]
    public float timeToSpawn;
    public bool spawn;

    private void Start()
    {
        spawnTime = new Timer(timeToSpawn);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spawn = true;
            spawnTime.startTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime.checkTimer())
        {
            if((int)Random.Range(0, 1) == 0)
            {
                Instantiate(ghost, leftSpawn.transform.position, leftSpawn.transform.rotation);
            }
            else
            {
                Instantiate(ghost, leftSpawn.transform.position, leftSpawn.transform.rotation);
            }
            spawnTime.startTimer();
        }
        spawnTime.timerUpdate();
    }
}
