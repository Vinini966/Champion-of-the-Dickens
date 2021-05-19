using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public void loadGame() //Starts the Game
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(2);
    }

    public void quitGame() //Goes back to Title Scene
    {
        Debug.Log("Quit Game");
        SceneManager.LoadScene(0);

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