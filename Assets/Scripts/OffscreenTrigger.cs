using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenTrigger : MonoBehaviour
{
    public bool quit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            if (quit)
            {
                Application.Quit();
                print("Quitting");
            }
            else
            {
                SceneChange.Instance().GO = false;
                SceneChange.Instance().gameObject.SetActive(true);
                SceneChange.Instance().sceneToChange = "Level 1";
                SceneChange.Instance().startChange();
            }
                
    }
}
