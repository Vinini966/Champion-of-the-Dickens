using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public bool lockPlayer;

    [System.Serializable]
    public struct Dialog
    {
        [TextArea]
        public string txt;
        public int ID;
    }

    [SerializeField]
    public Dialog[] dialogs;
    public TextBubble[] bubbles;

    int boxOn = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (lockPlayer)
        {
            Object[] ob = FindObjectsOfType<Player_Controler>();
            foreach (Player_Controler pC in ob)
            {
                pC.lockInputs = true;
            }
        }
        bubbles[dialogs[boxOn].ID].TextToShow = dialogs[boxOn].txt;
        bubbles[dialogs[boxOn].ID].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (bubbles[dialogs[boxOn].ID].isFinished() && Input.anyKeyDown)
        {
            bubbles[dialogs[boxOn].ID].gameObject.SetActive(false);
            boxOn++;
            if(boxOn >= dialogs.Length)
            {
                boxOn = 0;
                Object[] ob = FindObjectsOfType<Player_Controler>();
                foreach (Player_Controler pC in ob)
                {
                    pC.lockInputs = false;
                }
                this.enabled = false;
                return;
            }
            else
            {
                bubbles[dialogs[boxOn].ID].TextToShow = dialogs[boxOn].txt;
                bubbles[dialogs[boxOn].ID].gameObject.SetActive(true);
            }
        }
    }
}
