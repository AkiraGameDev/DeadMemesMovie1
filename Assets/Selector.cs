using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{
    GameObject arduino;
    wrmhlRead wrmhlReader;
    bool canInput;
    public CanvasRenderer topButtonRenderer;
    public CanvasRenderer middleButtonRenderer;
    public CanvasRenderer bottomButtonRenderer;
    public CanvasRenderer topTextRenderer;
    public CanvasRenderer middleTextRenderer;
    public CanvasRenderer bottomTextRenderer;
    // Start is called before the first frame update
    void Start()
    {
        arduino = GameObject.Find("wrmhlRead");
        wrmhlReader = arduino.GetComponent<wrmhlRead>();
        canInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canInput)
        {
            if(Input.GetKeyDown("1") || wrmhlReader.arduinoOutput() == "0")
            {
                canInput = false;
                wrmhlReader.closeStuff();
                wrmhlReader.closeStuff();
                wrmhlReader.closeStuff();
                wrmhlReader.closeStuff();
                Destroy(wrmhlReader);
                Destroy(arduino);
                FadeObjects(false, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer, bottomButtonRenderer, bottomTextRenderer}, 1.0f);
            }
            if(Input.GetKeyDown("2") || wrmhlReader.arduinoOutput() == "1")
            {
            }
            if(Input.GetKeyDown("3") || wrmhlReader.arduinoOutput() == "2")
            {
            }
        }
    }

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
                    objectArray[i].SetAlpha(Mathf.Lerp(1, -1, t/fadeLength));
                }
                yield return null;
            }
            SceneManager.LoadScene("GameCore",LoadSceneMode.Single);
            yield return null;
        }
}
