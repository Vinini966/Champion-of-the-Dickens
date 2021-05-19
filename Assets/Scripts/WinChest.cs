using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChest : MonoBehaviour
{
    public Animator anim;
    public DialogSystem dialog;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        dialog = GetComponent<DialogSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !once)
        {
            anim.SetTrigger("Open");
            Object[] ob = FindObjectsOfType<GhostAi>();
            foreach (GhostAi am in ob)
            {
                am.enabled = false;
            }
            print("OPEN");
        }

    }

    private void Update()
    {
        if(once && !dialog.enabled)
        {
            SceneChange.Instance().gameObject.SetActive(true);
            SceneChange.Instance().sceneToChange = "Win";
            SceneChange.Instance().startChange();
        }
    }

    public void open()
    {
        dialog.enabled = true;
        once = true;

    }
}
