using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieMachine : MonoBehaviour {
    
    [System.Serializable]
    public class Node 
    {   
        [SerializeField]
        public VideoClip nodeClip;

        int numPaths;
        Node[] paths = new Node[0];


        //constructors for 1,2,3 nodes respectively
        public Node(Node first, Node second, Node third)
        {
            numPaths = 3;
            paths = new Node[numPaths];
            paths[0] = first;
            paths[1] = second;
            paths[2] = third;
        }
        public Node(Node first, Node second)
        {
            numPaths = 2;
            paths = new Node[numPaths];
            paths[0] = first;
            paths[1] = second;

        }
        public Node(Node first)
        {
            numPaths = 1;
            paths = new Node[numPaths];
            paths[0] = first;

        }

        //constructor for no paths
        public Node()
        {
            numPaths = 0;
        }
    }

    public static Node introNode;

    public static Node bobRossNode = new Node(shrekNoNode, shrekYesNode);
    public static Node shrekNoNode = new Node(shrekHeartBreakNode, shrekRunNode, shrekFightNode);
    public static Node shrekYesNode = new Node(shrekRunNode,shrekFightNode,shrekFlirtNode);
    public static Node shrekHeartBreakNode = new Node();
    public static Node shrekRunNode; 
    public static Node shrekFightNode;
    public static Node shrekFlirtNode;

    public static Node datBoiNode;
    public static Node datBoiDeathNode;
    //quicktime?
    public static Node datBoitoArea51Node;
    //quicktime fail result?
    public static Node knightDeathNode;
    public static Node area51Node;
    public static Node cat51Node;
    public static Node infinite51Node;
    public static Node alienEndNode;
    public static Node harambeNode;

    public static Node floridaManNode;
    public static Node shiaYesNode;
    public static Node shiaNoNode;
    public static Node shiaHelpNode;
    public static Node shiaNoHelpNode;
    public static Node shiaRunNode;
    public static Node shiaFightNode;
    


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
    private char[] stateString = new char[10] ;
    private Node currentNode;
    private int userInput;

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
        //check for input from user
        //if(inputDetected)
        //{
        //  userInput = userInputTransformer();
        //  traverseNode(userInput)
        //}

    }

    void UpdateState()
    {

    }

    //check if current clip has finished playing
    bool ClipFinished (VideoPlayer videoPlayer) {
        if(videoPlayer.frame == (long)videoPlayer.frameCount)
        {
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
}