using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBox : MonoBehaviour
{
    public bool isDialog;
    bool done = false;
    public GameObject textBox;
    public DialogSystem dialog;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !done)
            if (isDialog)
            {
                dialog.enabled = true;
                done = true;
            }
            else
                textBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isDialog)
            {
                dialog.enabled = false;
                
            }
                
            else
                textBox.SetActive(false);
            
        }
            
    }

}
