using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovieMachine : MonoBehaviour {
    
    [System.Serializable]
    public class Node 
    {   
        public VideoClip nodeClip;
        public string[] buttonText;
        public bool quicktime;
        public bool quicktime2;
        public int quicktimeNodeNum;
        public Node[] paths;
        public int numPaths;
        public bool continuedNode;
        public ulong qtStart;
        public ulong qtEnd;

        //constructors for 1,2,3 nodes respectively
        public Node(Node first, Node second, Node third)
        {
            numPaths = 3;
            paths = new Node[numPaths];
            paths[0] = first;
            paths[1] = second;
            paths[2] = third;
            this.SetNumPaths();
        }
        public Node(Node first, Node second)
        {
            numPaths = 2;
            paths = new Node[numPaths];
            paths[0] = first;
            paths[1] = second;
            this.SetNumPaths();
        }
        public Node(Node first)
        {
            numPaths = 1;
            paths = new Node[numPaths];
            paths[0] = first;
            this.SetNumPaths();
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
        public Node  bobRossNoNode;
        public Node  bobRossYesNode;
        public Node  shrekIntroNoNode;
        public Node  shrekIntroYesNode;
        public Node  shrekHeartBreakNode;
        public Node  shrekRunNode;
        public Node  shrekNoFightNode;
        public Node  shrekYesFightNode;
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
        public Node  floridaManYesNode;
        public Node  floridaManNoNode;
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
        public Node dominanceQT2Node;
        public Node dominanceNode;
    #endregion

    #region SlenderTroll Branch
        public Node  slenderTrollNode;
        public Node  sneakDeathNode;
        public Node  headEndNode;
        public Node  danceDeathNode;
    #endregion

    public Node startingNode;
    public Node currentNode;


    public GameObject leftButton;
    public GameObject middleButton;
    public GameObject rightButton;
    public GameObject leftText;
    public GameObject middleText;
    public GameObject rightText;
    
    GameObject arduino;
    wrmhlRead wrmhlReader;

    public VideoPlayer videoPlayer1;

    private CanvasRenderer leftButtonRenderer;
    private CanvasRenderer middleButtonRenderer;
    private CanvasRenderer rightButtonRenderer;
    private CanvasRenderer leftTextRenderer;
    private CanvasRenderer middleTextRenderer;
    private CanvasRenderer rightTextRenderer;

    private char[] stateString = new char[10];
    private int userInput;
    private bool notPlaying;
    private bool canInput = false;
    bool restartingClip = false;
    float buttonFourDown = 0.0f;

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

        bobRossNode.paths = new Node[2] {bobRossNoNode, bobRossYesNode};
        bobRossNoNode.paths = new Node[1] {shrekIntroNoNode};
        bobRossYesNode.paths = new Node[1] {shrekIntroYesNode};
        shrekIntroNoNode.paths = new Node[3] {shrekHeartBreakNode, shrekRunNode, shrekNoFightNode};
        shrekIntroYesNode.paths = new Node[3] {shrekFlirtNode, shrekRunNode, shrekYesFightNode};
        shrekHeartBreakNode.paths = new Node[1] {shrekIntroNoNode};
        shrekRunNode.paths = new Node[1] {fortniteNode};
        shrekNoFightNode.paths = new Node[1] {shrekIntroNoNode};
        shrekYesFightNode.paths = new Node[1] {shrekIntroYesNode};
        shrekFlirtNode.paths = new Node[1] {slenderTrollNode};

        datBoiNode.paths = new Node[2] {datBoiDeathNode, datBoitoArea51Node};
        datBoiDeathNode.paths = new Node[1] {datBoiNode};
        datBoitoArea51Node.paths = new Node[3] {knightDeathNode, area51Node, harambeNode};
        knightDeathNode.paths = new Node[1] {datBoitoArea51Node};
        area51Node.paths = new Node[3] {infinite51Node, alienEndNode, cat51Node};
        cat51Node.paths = new Node[1] {area51Node};
        infinite51Node.paths = new Node[1] {area51Node};
        alienEndNode.paths = new Node[1] {introNode};
        harambeNode.paths = new Node[2] {harambeEndNode, harambeSaveNode};
        harambeEndNode.paths = new Node[2] {harambeNode, introNode};
        harambeSaveNode.paths = new Node[1] {harambeNode};


        floridaManNode.paths = new Node[2] {floridaManYesNode, floridaManNoNode};
        floridaManYesNode.paths = new Node[1] {shiaYesNode};
        floridaManNoNode.paths = new Node[1] {shiaNoNode};
        shiaYesNode.paths = new Node[3] {shiaHelpNode, shiaRunNode, shiaFightNode};
        shiaNoNode.paths = new Node[3] {shiaRunNode, shiaFightNode, shiaNoHelpNode};
        shiaHelpNode.paths = new Node[1] {fortniteNode};
        shiaNoHelpNode.paths = new Node[1] {floridaManNode};
        shiaRunNode.paths = new Node[1] {floridaManNode};
        shiaFightNode.paths = new Node[1] {slenderTrollNode};
        fortniteNode.paths = new Node[2] {bannedDeathNode,dominanceQTNode};
        bannedDeathNode.paths = new Node[1] {fortniteNode};
        dominanceQTNode.paths = new Node[2] {dominanceQT2Node, dominanceQTNode};
        dominanceQT2Node.paths = new Node[2] {dominanceNode, dominanceQTNode};
        dominanceNode.paths = new Node[1] {introNode};


        slenderTrollNode.paths = new Node[3]{sneakDeathNode, headEndNode, danceDeathNode};
        sneakDeathNode.paths = new Node[1] {slenderTrollNode};
        headEndNode.paths = new Node[2] {slenderTrollNode, introNode};
        danceDeathNode.paths = new Node[1] {slenderTrollNode};

        startingNode = introNode;
        currentNode = startingNode;
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
        if(((long)videoPlayer1.frameCount-videoPlayer1.frame < 30) && !canInput && !currentNode.quicktime)
        {
            canInput = true;
            FadeLogic(true);
            Debug.Log("Can Input = True!");
        }
        
        CheckForInput();

        if(Input.GetKeyDown("4") || wrmhlReader.arduinoOutput() == "3")
        {
            buttonFourDown += Time.deltaTime;
            if(buttonFourDown >= 1.0f)
            {
                wrmhlReader.closeStuff();
                wrmhlReader.closeStuff();
                wrmhlReader.closeStuff();
                wrmhlReader.closeStuff();
                Destroy(wrmhlReader);
                Destroy(arduino);
                SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
                buttonFourDown = 0.0f;
            }
        }
        else
        {
            buttonFourDown = 0.0f;
        }
    }

    //check if current clip has finished playing
    bool ClipFinished (VideoPlayer videoPlayer) {
        if(videoPlayer.frame == (long)videoPlayer.frameCount-1)
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
                    objectArray[i].SetAlpha(Mathf.Lerp(1, -1, t/fadeLength));
                }
                yield return null;
            }
            yield return null;
        }

        void FadeLogic(bool direction){
            if(currentNode.buttonText.Length == 3) 
            { 
                leftText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                middleText.GetComponentInChildren<Text>().text = currentNode.buttonText[1];
                rightText.GetComponentInChildren<Text>().text = currentNode.buttonText[2];
                FadeObjects(direction, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, middleButtonRenderer, middleTextRenderer, rightButtonRenderer,rightTextRenderer}, 0.5f);
            }
            else if(currentNode.buttonText.Length == 2) 
            { 
                leftText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                rightText.GetComponentInChildren<Text>().text = currentNode.buttonText[1];
                FadeObjects(direction, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, rightButtonRenderer,rightTextRenderer}, 0.5f);
            }
            else if(currentNode.buttonText.Length == 1) 
            {
                middleText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                FadeObjects(direction, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.5f);
            }
        }
    #endregion

    void CheckForInput(){
        if(currentNode.continuedNode && ClipFinished(videoPlayer1))
        {
            if(currentNode.quicktime2)
            {
                FadeObjects(false, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, rightButtonRenderer, rightTextRenderer}, 0.01f);
            }
            canInput = false;
            notPlaying = true;
            currentNode = currentNode.paths[0];
            videoPlayer1.clip = currentNode.nodeClip;
            videoPlayer1.Prepare();
        }

        if(currentNode.quicktime)
        {
            if((videoPlayer1.frame >= (long)(videoPlayer1.frameCount-currentNode.qtStart) && videoPlayer1.frame <= (long)(videoPlayer1.frameCount-currentNode.qtEnd)))
            {
                Debug.Log("Start Frame " + (videoPlayer1.frameCount-currentNode.qtStart));
                Debug.Log("End Frame " + (videoPlayer1.frameCount-currentNode.qtEnd));
                Debug.Log("Current Frame " + videoPlayer1.frame);
                Debug.Log("Total Frames " + videoPlayer1.frameCount);
                if(!canInput)
                {
                    canInput = true;
                    middleText.GetComponentInChildren<Text>().text = currentNode.buttonText[1];
                    FadeObjects(true, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.01f);
                }
                if(Input.GetKeyDown("2") || wrmhlReader.arduinoOutput() == "1")
                {
                    canInput = false;
                    notPlaying = true;
                    FadeObjects(false, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.01f);
                    currentNode = currentNode.paths[0];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }
            }

            if(videoPlayer1.frame == (long)videoPlayer1.frameCount-(long)(currentNode.qtEnd-1))
            {
                restartingClip = false;
                Debug.Log("We should be fading out now!!!");
                canInput = false;
                FadeObjects(false, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.01f);
            }

            if(ClipFinished(videoPlayer1))
            {
                if(!canInput && !restartingClip)
                {
                    Debug.Log("rendering restart");
                    Debug.Log(videoPlayer1.frame);
                    Debug.Log(canInput);
                    canInput = true;
                    middleText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                    FadeObjects(true, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.5f);
                }

                if(Input.GetKeyDown("2") || wrmhlReader.arduinoOutput() == "1")
                {
                    canInput = false;
                    notPlaying = true;
                    FadeObjects(false, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.01f);
                    currentNode = currentNode.paths[1];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                    restartingClip = true;
                }
            }
        }

        if(currentNode.quicktime2)
        {
            if((videoPlayer1.frame >= (long)(videoPlayer1.frameCount-currentNode.qtStart) && videoPlayer1.frame <= (long)(videoPlayer1.frameCount-currentNode.qtEnd)))
            {
                Debug.Log("Start Frame " + (videoPlayer1.frameCount-currentNode.qtStart));
                Debug.Log("End Frame " + (videoPlayer1.frameCount-currentNode.qtEnd));
                Debug.Log("Current Frame " + videoPlayer1.frame);
                Debug.Log("Total Frames " + videoPlayer1.frameCount);
                if(!canInput)
                {
                    canInput = true;
                    leftText.GetComponentInChildren<Text>().text = currentNode.buttonText[0];
                    rightText.GetComponentInChildren<Text>().text = currentNode.buttonText[1];
                    FadeObjects(true, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, rightButtonRenderer, rightTextRenderer}, 0.01f);
                }
                if(Input.GetKeyDown("1") || wrmhlReader.arduinoOutput() == "0")
                {
                    canInput = false;
                    notPlaying = true;
                    FadeObjects(false, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, rightButtonRenderer, rightTextRenderer}, 0.01f);
                    currentNode = currentNode.paths[1];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }
                if(Input.GetKeyDown("3") || wrmhlReader.arduinoOutput() == "2")
                {
                    canInput = false;
                    notPlaying = true;
                    FadeObjects(false, new List<CanvasRenderer>{leftButtonRenderer, leftTextRenderer, rightButtonRenderer, rightTextRenderer}, 0.01f);
                    currentNode = currentNode.paths[2];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }
            }

            if(videoPlayer1.frame == (long)videoPlayer1.frameCount-(long)(currentNode.qtEnd-1))
            {
                restartingClip = false;
                Debug.Log("We should be fading out now!!!");
                canInput = false;
                FadeObjects(false, new List<CanvasRenderer>{middleButtonRenderer, middleTextRenderer}, 0.01f);
            }
        }


        if(canInput && !currentNode.quicktime && !currentNode.quicktime2)
        {
            if(Input.GetKeyDown("1") || wrmhlReader.arduinoOutput() == "0")
            {
                Debug.Log("Pressed 1!");
                if(currentNode.buttonText.Length == 3)
                {
                    canInput = false;
                    notPlaying = true;
                    FadeLogic(false);
                    currentNode = currentNode.paths[0];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }

                if(currentNode.buttonText.Length == 2 && canInput)
                {
                    canInput = false;
                    notPlaying = true;
                    FadeLogic(false);
                    currentNode = currentNode.paths[0];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }
            }

            if(Input.GetKeyDown("2") || wrmhlReader.arduinoOutput() == "1")
            {
                if(currentNode.buttonText.Length == 3)
                {
                    canInput = false;
                    notPlaying = true;
                    FadeLogic(false);
                    currentNode = currentNode.paths[1];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }

                if(currentNode.buttonText.Length == 1 && canInput)
                {
                    canInput = false;
                    notPlaying = true;
                    FadeLogic(false);
                    currentNode = currentNode.paths[0];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                } 
            }

            if(Input.GetKeyDown("3") || wrmhlReader.arduinoOutput() == "2")
            {
                if(currentNode.buttonText.Length == 3)
                {
                    canInput = false;
                    notPlaying = true;
                    FadeLogic(false);
                    currentNode = currentNode.paths[2];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }
                if(currentNode.buttonText.Length == 2 && canInput)
                {
                    canInput = false;
                    notPlaying = true;
                    FadeLogic(false);
                    currentNode = currentNode.paths[1];
                    videoPlayer1.clip = currentNode.nodeClip;
                    videoPlayer1.Prepare();
                }
            }
        }
    }

    void OnApplicationQuit() { // close the Thread and Serial Port
		Destroy(arduino);
        Destroy(wrmhlReader);
	}
}