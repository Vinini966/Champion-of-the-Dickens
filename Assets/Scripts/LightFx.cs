using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFx : MonoBehaviour
{
    Timer lChange;
    // Start is called before the first frame update
    void Start()
    {
        lChange = new Timer(0.1f);
        lChange.startTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (lChange.checkTimer())
        {
            GetComponent<Light>().intensity = Random.Range(0.6f, 1.0f);
            lChange.startTimer();
        }
        else
            lChange.timerUpdate();
        
    }
}
