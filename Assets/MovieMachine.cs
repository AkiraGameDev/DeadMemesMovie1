using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MovieMachine : MonoBehaviour {
    
    [System.Serializable]
    public class Node 
    {   
        public VideoClip nodeClip;
        public string[] buttonText;
        public bool quicktime;
        public int quicktimeNodeNum;
        public Node[] paths;
        public int numPaths;

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

        public void SetNumPaths()
        {
            this.numPaths = this.paths.Length;
        }
    }

    //all nodes
    public Node  introNode;
    #region Bob Ross Nodes
        public Node  bobRossNode;
        public Node  shrekNoNode;
        public Node  shrekYesNode;
        public Node  shrekHeartBreakNode;
        public Node  shrekRunNode;
        public Node  shrekFightNode;
        public Node  shrekFlirtNode;
    #endregion

    #region Dat Boi Branch
        public Node  datBoiNode;
        public Node  datBoiDeathNode;
        //quicktime?
        public Node  datBoitoArea51Node;
        //quicktime fail result?
        public Node  knightDeathNode;
        public Node  area51Node;
        public Node  cat51Node;
        public Node  infinite51Node;
        public Node  alienEndNode;
        public Node  harambeNode;
        public Node  harambeEndNode;
        public Node  harambeSaveNode;
    #endregion

    #region Florida Man Branch
        public Node  floridaManNode;
        public Node  shiaYesNode;
        public Node  shiaNoNode;
        public Node  shiaHelpNode;
        public Node  shiaNoHelpNode;
        public Node  shiaRunNode;
        public Node  shiaFightNode;
    #endregion

    #region Fortnite Branch
        public Node fortniteNode;
        public Node bannedDeathNode;
        public Node dominanceQTNode;
        public Node getRektNode;
        public Node dominanceQT2Node;
        public Node getSlappedNode;
        public Node dominanceNode;
    #endregion

    #region SlenderTroll Branch
        public Node  slenderTrollNode;
        public Node  sneakDeathNode;
        public Node  headEndNode;
        public Node  danceDeathNode;
    #endregion


    public GameObject leftButton;
    public GameObject middleButton;
    public GameObject rightButton;
    public GameObject leftText;
    public GameObject middleText;
    public GameObject rightText;
    GameObject arduino;
    wrmhlRead wrmhlReader;

    public Text leftScript;
    public Text middleScript;
    public Text rightScript;

    public VideoPlayer videoPlayer1;

    private CanvasRenderer leftButtonRenderer;
    private CanvasRenderer middleButtonRenderer;
    private CanvasRenderer rightButtonRenderer;
    private CanvasRenderer leftTextRenderer;
    private CanvasRenderer middleTextRenderer;
    private CanvasRenderer rightTextRenderer;

    private char[] stateString = new char[10];
    private Node currentNode;
    private int userInput;
    private bool notPlaying;
    private bool canInput = false;


    // Start is called before the first frame update
    void Start () {
        arduino = GameObject.Find("wrmhlRead");
        wrmhlReader = arduino.GetComponent<wrmhlRead>();
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

        introNode.paths = new Node[3] {bobRossNode, datBoiNode, floridaManNode};
        introNode.SetNumPaths();
        bobRossNode.paths = new Node[2] {shrekNoNode, shrekYesNode};
        bobRossNode.SetNumPaths();
        shrekNoNode.paths = new Node[3] {shrekHeartBreakNode, shrekRunNode, shrekFightNode};
        shrekNoNode.SetNumPaths();
        shrekYesNode.paths = new Node[3] {shrekRunNode, shrekFightNode, shrekFlirtNode};
        shrekYesNode.SetNumPaths();
        shrekHeartBreakNode.paths = new Node[1] {bobRossNode};
        shrekHeartBreakNode.SetNumPaths();
        shrekRunNode.paths = new Node[1] {fortniteNode};
        shrekRunNode.SetNumPaths();
        shrekFightNode.paths = new Node[1] {bobRossNode};
        shrekFightNode.SetNumPaths();
        shrekFlirtNode.paths = new Node[1] {slenderTrollNode};
        shrekFlirtNode.SetNumPaths();

        datBoiNode.paths = new Node[2] {datBoiDeathNode, datBoitoArea51Node};
        datBoiNode.SetNumPaths();
        datBoiDeathNode.paths = new Node[1] {datBoiDeathNode};
        datBoiDeathNode.SetNumPaths();
        datBoitoArea51Node.paths = new Node[3] {knightDeathNode, area51Node, harambeNode};
        datBoitoArea51Node.SetNumPaths();
        knightDeathNode.paths = new Node[1] {datBoiNode};
        knightDeathNode.SetNumPaths();
        area51Node.paths = new Node[3] {infinite51Node, alienEndNode, cat51Node};
        area51Node.SetNumPaths();
        cat51Node.paths = new Node[1] {area51Node};
        cat51Node.SetNumPaths();
        infinite51Node.paths = new Node[1] {area51Node};
        infinite51Node.SetNumPaths();
        alienEndNode.paths = new Node[1] {introNode};
        alienEndNode.SetNumPaths();
        harambeNode.paths = new Node[2] {harambeEndNode, harambeSaveNode};
        harambeNode.SetNumPaths();
        harambeEndNode.paths = new Node[2] {harambeNode, introNode};
        harambeEndNode.SetNumPaths();
        harambeSaveNode.paths = new Node[1] {harambeNode};
        harambeSaveNode.SetNumPaths();

        floridaManNode.paths = new Node[2] {shiaYesNode, shiaNoNode};
        floridaManNode.SetNumPaths();
        shiaYesNode.paths = new Node[3] {shiaHelpNode, shiaRunNode, shiaFightNode};
        shiaYesNode.SetNumPaths();
        shiaNoNode.paths = new Node[3] {shiaRunNode, shiaFightNode, shiaNoHelpNode};
        shiaNoNode.SetNumPaths();
        shiaHelpNode.paths = new Node[1] {fortniteNode};
        shiaHelpNode.SetNumPaths();
        shiaNoHelpNode.paths = new Node[1] {floridaManNode};
        shiaNoHelpNode.SetNumPaths();
        shiaRunNode.paths = new Node[1] {floridaManNode};
        shiaRunNode.SetNumPaths();
        shiaFightNode.paths = new Node[1] {slenderTrollNode};
        shiaFightNode.SetNumPaths();
        fortniteNode.paths = new Node[2] {bannedDeathNode,dominanceQTNode};
        fortniteNode.SetNumPaths();
        bannedDeathNode.paths = new Node[1] {fortniteNode};
        bannedDeathNode.SetNumPaths();
        dominanceQTNode.paths = new Node[2] {getRektNode, dominanceQT2Node};
        dominanceQTNode.SetNumPaths();
        getRektNode.paths = new Node[1] {fortniteNode};
        getRektNode.SetNumPaths();
        dominanceQT2Node.paths = new Node[2] {getSlappedNode, dominanceNode};
        dominanceQT2Node.SetNumPaths();
        getSlappedNode.paths = new Node[1] {fortniteNode};
        getSlappedNode.SetNumPaths();
        dominanceNode.paths = new Node[1] {fortniteNode};
        dominanceNode.SetNumPaths();

        slenderTrollNode.paths = new Node[3]{sneakDeathNode, headEndNode, danceDeathNode};
        slenderTrollNode.SetNumPaths();
        sneakDeathNode.paths = new Node[1] {slenderTrollNode};
        sneakDeathNode.SetNumPaths();
        headEndNode.paths = new Node[1] {slenderTrollNode};
        headEndNode.SetNumPaths();
        danceDeathNode.paths = new Node[1] {slenderTrollNode};
        danceDeathNode.SetNumPaths();

        currentNode = introNode;
        videoPlayer1.clip = currentNode.nodeClip;
        notPlaying = true;
        videoPlayer1.Prepare();
        
    }


    // Update is called once per frame
    void Update () 
    {
        //play prepped video
        if(videoPlayer1.isPrepared && notPlaying)
        {
            videoPlayer1.Play();
            if(videoPlayer1.isPlaying){ notPlaying = false; }
        }

        //fade in buttons x second(s) before end of clip
        if(((long)videoPlayer1.frameCount-videoPlayer1.frame < 30) && !canInput)
        {
            canInput = true;
            FadeLogic(true);
        }
        
        CheckForInput();
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
    void LoadClip (VideoPlayer targetVideoPlayer, VideoClip targetVideoClip) { 
        targetVideoPlayer.clip = targetVideoClip;
        notPlaying = false;
    }

    //pre-loads clip when possible/necessary for polish
    void PreLoadClip () { }

    //FadeObjects (bool direction, List<CanvasRenderer> objectList, float fadeLength)
    #region Fade In/Out Package
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
                yield return null;
            }
            yield return null;
        }

        void FadeLogic(bool direction){
            if(currentNode.paths.Length == 3) 
            { 
                leftText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                middleText.GetComponentInChildren<Text>().text = currentNode.buttonText[1];
                rightText.GetComponentInChildren<Text>().text = currentNode.buttonText[2];
                FadeObjects(direction, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, middleButtonRenderer, middleTextRenderer, rightButtonRenderer,rightTextRenderer}, 0.5f);
            }
            else if(currentNode.paths.Length == 2) 
            { 
                leftText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                rightText.GetComponentInChildren<Text>().text = currentNode.buttonText[1];
                FadeObjects(direction, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, rightButtonRenderer,rightTextRenderer}, 0.5f);
            }
            else if(currentNode.paths.Length == 1) 
            {
                middleText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                FadeObjects(direction, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.5f);
            }
        }
    #endregion

    void CheckForInput(){
        //check for input
        if(canInput && wrmhlReader.arduinoOutput() == "0")
        {
            if(currentNode.numPaths == 3)
            {
                FadeLogic(false);
                currentNode = currentNode.paths[0];
                videoPlayer1.clip = currentNode.nodeClip;
                notPlaying = true;
                videoPlayer1.Prepare();
                canInput = false;
            }

            if(currentNode.numPaths == 2)
            {
                FadeLogic(false);
                currentNode = currentNode.paths[0];
                videoPlayer1.clip = currentNode.nodeClip;
                notPlaying = true;
                videoPlayer1.Prepare();
                canInput = false;
            }
        }

        if(canInput && wrmhlReader.arduinoOutput() == "1")
        {
            if(currentNode.numPaths == 3)
            {
                FadeLogic(false);
                currentNode = currentNode.paths[1];
                videoPlayer1.clip = currentNode.nodeClip;
                notPlaying = true;
                videoPlayer1.Prepare();
                canInput = false;
            }

            if(currentNode.numPaths == 1)
            {
                FadeLogic(false);
                currentNode = currentNode.paths[0];
                videoPlayer1.clip = currentNode.nodeClip;
                notPlaying = true;
                videoPlayer1.Prepare();
                canInput = false;
            } 
        }

        if(canInput && wrmhlReader.arduinoOutput() == "2")
        {
            if(currentNode.numPaths == 3)
            {
                FadeLogic(false);
                currentNode = currentNode.paths[2];
                videoPlayer1.clip = currentNode.nodeClip;
                notPlaying = true;
                videoPlayer1.Prepare();
                canInput = false;
            }
            if(currentNode.numPaths == 2)
            {
                FadeLogic(false);
                currentNode = currentNode.paths[1];
                videoPlayer1.clip = currentNode.nodeClip;
                notPlaying = true;
                videoPlayer1.Prepare();
                canInput = false;
            }
        }
    }
}