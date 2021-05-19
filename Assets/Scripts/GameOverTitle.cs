using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTitle : MonoBehaviour
{
    public GameObject camStart;
    public GameObject camEnd;
    public GameObject cam;
    public DialogSystem dialog;
    public GameObject hud;
    bool go;
    bool sarted = false;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneChange.Instance().GO)
        {
            cam.transform.position = camStart.transform.position;
            go = SceneChange.Instance().GO;
            hud.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(go && !SceneChange.Instance().gameObject.activeSelf && !sarted)
        {
            dialog.enabled = true;
            sarted = true;
        }
        if(sarted && !dialog.enabled)
        {
            Object[] ob = FindObjectsOfType<Player_Controler>();
            foreach (Player_Controler pC in ob)
            {
                pC.lockInputs = true;
            }
            if (Vector3.Distance(cam.transform.position, camEnd.transform.position) > 0)
            {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position,
                                                         camEnd.transform.position,
                                                         1.0f * Time.deltaTime);
            }
            else
            {
                hud.SetActive(true);
                ob = FindObjectsOfType<Player_Controler>();
                foreach (Player_Controler pC in ob)
                {
                    pC.lockInputs = false;
                }
            }
            
        }

    }
}
