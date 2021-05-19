using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void loadGame() //Starts the Game
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("StoryThusFar");
    }

    public void doExitGame() //Closes the Application
    {
        Application.Quit();
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
