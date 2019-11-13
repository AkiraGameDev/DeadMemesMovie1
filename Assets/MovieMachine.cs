using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieMachine : MonoBehaviour {
    public GameObject leftButton;
    public GameObject middleButton;
    public GameObject rightButton;
    public GameObject leftText;
    public GameObject middleText;
    public GameObject rightText;

    private string currentStage;
    private CanvasRenderer leftButtonRenderer;
    private CanvasRenderer middleButtonRenderer;
    private CanvasRenderer rightButtonRenderer;
    private CanvasRenderer leftTextRenderer;
    private CanvasRenderer middleTextRenderer;
    private CanvasRenderer rightTextRenderer;

    // Start is called before the first frame update
    void Start () {
        leftButtonRenderer = leftButton.GetComponent<CanvasRenderer>();
        middleButtonRenderer = middleButton.GetComponent<CanvasRenderer>();
        rightButtonRenderer = rightButton.GetComponent<CanvasRenderer>();
        leftTextRenderer = leftText.GetComponent<CanvasRenderer>();
        middleTextRenderer = middleText.GetComponent<CanvasRenderer>();
        rightTextRenderer = rightText.GetComponent<CanvasRenderer>();

        leftButtonRenderer.SetAlpha(0.0f);
        middleButtonRenderer.SetAlpha(0.0f);
        rightButtonRenderer.SetAlpha(0.0f);
        leftTextRenderer.SetAlpha(0.0f);
        middleTextRenderer.SetAlpha(0.0f);
        rightTextRenderer.SetAlpha(0.0f);

        //LoadClip(StartingClip)
    }

    // Update is called once per frame
    void Update () 
    {
    }

    //check if current clip has finished playing
    bool ClipFinished (VideoPlayer videoPlayer) {
        if(videoPlayer.frame == (long)videoPlayer.frameCount)
        {
            Debug.Log("Job's done!");
            return true;
        }
        else
        {
            return false;
        }
    }

    //loads clip, as necessary
    void LoadClip (VideoPlayer targetVideoPlayer, VideoClip targetVideoClip) 
    { 
        targetVideoPlayer.clip = targetVideoClip;
    }

    //pre-loads clip when possible/necessary for polish
    void PreLoadClip () { }

    //makes buttons visible by setting alpha values, true=fade-in(on), false=fade out(off)
    void FadeObjects (bool direction, List<CanvasRenderer> objectList, float fadeLength) {
        if (direction) {
            StartCoroutine(FadeIn(objectList.ToArray(), objectList.Count, fadeLength));
        }

        if (!direction) {
           StartCoroutine(FadeOut(objectList.ToArray(), objectList.Count, fadeLength));
        }
    }

    IEnumerator FadeIn (CanvasRenderer[] objectArray, int arraySize, float fadeLength) {
        for (float t = 0.0f; t < fadeLength; t += Time.deltaTime) {
            for (int i = 0; i < arraySize; i++) {
                objectArray[i].SetAlpha(Mathf.Lerp(0, 1, t/fadeLength));
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator FadeOut (CanvasRenderer[] objectArray, int arraySize, float fadeLength) {
        for (float t = 0.0f; t < fadeLength; t += Time.deltaTime) {
            for (int i = 0; i < arraySize; i++) {
                objectArray[i].SetAlpha(Mathf.Lerp(1, 0, t/fadeLength));
            }
            Debug.Log("Outputting 2!");
            yield return null;
        }
        yield return null;
    }

    //redirect buttons to load proper clips
    void SetButtonDirection () { }
}