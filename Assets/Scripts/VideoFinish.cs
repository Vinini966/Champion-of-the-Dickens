using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoFinish : MonoBehaviour
{

    public VideoPlayer video;
    public string scene;
    public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SceneChange.Instance().sceneToChange = scene;
            SceneChange.Instance().startChange();
        }
        if (!video.isPlaying && started)
        {
            SceneChange.Instance().sceneToChange = scene;
            SceneChange.Instance().startChange();
            started = false;
        }
        if (video.isPlaying)
        {
            started = true;
        }
    }
}
