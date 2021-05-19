using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Object[] ob = FindObjectsOfType<Player_Controler>();
            foreach (Player_Controler pC in ob)
            {
                pC.lockInputs = true;
            }
            SceneChange.Instance().sceneToChange = "TitleTest";
            SceneChange.Instance().GO = true;
            SceneChange.Instance().startChange();
        }
        else if(collider.tag != "Ground")
        {
            Destroy(collider.gameObject);
        }
    }
}
