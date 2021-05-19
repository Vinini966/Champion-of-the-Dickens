using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static SceneChange i;
    public Image slider;
    Timer timeOut;
    [Range(0.0f, 1.0f)]
    public float timer;
    public bool started;
    public bool GO = false;
    bool down = false;
    float alpha;
    public string sceneToChange;
    AsyncOperation comp;

    // Start is called before the first frame update
    void Start()
    {
        if (i == null)
        {
            DontDestroyOnLoad(this.gameObject);
            i = this;
        }
        else
            Destroy(this);
        timeOut = new Timer(timer);
        slider = gameObject.transform.GetChild(0).GetComponent<Image>();
        slider.color = new Color(0f, 0f, 0f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(started && timeOut.checkTimer())
        {
            if (!down)
                alpha += 0.1f;
            else
                alpha -= 0.1f;
            slider.color = new Color(0f, 0f, 0f, alpha);
            timeOut.startTimer();
        }
        if (alpha >= 1f && comp == null)
        {
            started = false;
            comp = SceneManager.LoadSceneAsync(sceneToChange);
        }
        if (alpha <= 0f && started && down)
        {
            started = false;
            gameObject.SetActive(false);
        }
        if(comp != null)
            if (comp.isDone)
            {
                started = true;
                down = true;
                comp = null;
            }
        timeOut.timerUpdate();
    }

    public static SceneChange Instance()
    {
        return i;
    }

    public void startChange()
    {
        if (!started)
        {
            started = true;
            down = false;
            timeOut.startTimer();
            gameObject.SetActive(true);
            Object[] ob = FindObjectsOfType<Player_Controler>();
            foreach (Player_Controler pC in ob)
            {
                pC.lockInputs = false;
            }
        }  
    }
}
