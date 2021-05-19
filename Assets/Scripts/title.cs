using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class title : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void loadGame() //Starts the Game
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Level 1");
    }

    public void quitGame() //Goes back to Title Scene
    {
        Debug.Log("Quit Game");
        SceneManager.LoadScene("TitleTest");
 
    }

    public void openServey()
    {
        Application.OpenURL("https://forms.gle/VyyzrSdM8JihPrvN8");
    }

    public void resume()
    {
        GameObject[] d = SceneManager.GetSceneByName("Level 1").GetRootGameObjects();
        d[0].GetComponent<GameManager>().OnGameResume();
    }
}
