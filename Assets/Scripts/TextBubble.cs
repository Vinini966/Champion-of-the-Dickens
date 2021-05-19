using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubble : MonoBehaviour
{
    [Header("Text")]
    [TextArea]
    public string TextToShow;
   // public bool lockPlayer;
    public Image textBox;
    public TMPro.TMP_Text text;
    public Febucci.UI.TextAnimatorPlayer textAnimatorPlayer;

    private string textShown;

    float imagescale = 7.5f;
    float textscale = 100f;
    int charOn = 0;

    [Header("Timing")]
    Timer nextLetter;
    [Range(0f, 1.0f)]
    public float TimeToNextLetter;

    bool isDone = false;


    // Start is called before the first frame update
    void Start()
    {
        nextLetter = new Timer(TimeToNextLetter);
        nextLetter.zeroTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDone)
        {
            //var tmp = textBox.GetComponent<RectTransform>().rect;
            //while (text.isTextOverflowing)
            //{
            //    tmp = textBox.GetComponent<RectTransform>().rect;
            //    if (TextToShow[charOn - 1] == '\n')
            //        tmp.height += imagescale * 2;
            //    else
            //        tmp.height += imagescale;
            //    tmp.width += imagescale;
            //    textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(tmp.width, tmp.height);
            //    tmp = text.GetComponent<RectTransform>().rect;
            //    tmp.height += textscale;
            //    tmp.width += textscale;
            //    text.GetComponent<RectTransform>().sizeDelta = new Vector2(tmp.width, tmp.height);
            //}
        }
    }

    void OnDisable()
    {
        isDone = false;
        //charOn = 0;
        //textShown = "";
        //text.text = "";
        //var tmp = textBox.GetComponent<RectTransform>().rect;
        //tmp.height = 81.12f;
        //tmp.width = 144.92f;
        //textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(tmp.width, tmp.height);
        //tmp = text.GetComponent<RectTransform>().rect;
        //tmp.height = 837.13f;
        //tmp.width = 1881.7f;
        //text.GetComponent<RectTransform>().sizeDelta = new Vector2(tmp.width, tmp.height);
    }

    private void OnEnable()
    {
        if (TextToShow != null)
        {
            textAnimatorPlayer.ShowText(TextToShow);
            
        }
            
    }

    public bool isFinished()
    {
        return isDone;
    }

    public void TextShown()
    {
        isDone = true;
    }
}
