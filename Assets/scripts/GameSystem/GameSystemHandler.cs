using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSystemHandler : MonoBehaviour
{
    //players REF;
    [SerializeField]
    private GameObject player1Ref;
    [SerializeField]
    private GameObject player2Ref;

    [Header("Checker")]
     [SerializeField] private checkersBoard checkerData; 

    [Header("Acorn Event")]
     [SerializeField] private GameObject[] spawnableObjects;
     [SerializeField] private GameObject[] whiteSideSpawningREF;
     [SerializeField] private GameObject[] blackSideSpawningREF;
     private GameObject spawningObject;
     private GameObject spawningLocRef;
     private ShakeController shakeController;

    //Values of AcornFallingEvent;
    [Header("Timer Values")]
     [SerializeField] private float maxTimerForStartGame = 10.0f;
     [SerializeField] private float minTimerForStartGame = 7.0f;
     [SerializeField] private float maxTimerForEarlyGame = 8.0f;
     [SerializeField] private float minTimerForEarlyGame = 7.0f;
     [SerializeField] private float maxTimerForMidGame = 8.0f;
     [SerializeField] private float minTimerForMidGame = 5.0f;
     [SerializeField] private float maxTimerForLateGame = 8.0f;
     [SerializeField] private float minTimerForLateGame = 6.0f;

    [Header("Success Rates")]
     [SerializeField] private float successRateForStartGame = 1.2f;
     [SerializeField] private float successRateForEarlyGame = 0.9f;
     [SerializeField] private float successRateForMidGame = 0.65f;
     [SerializeField] private float successRateForLateGame = 0.5f;

    [SerializeField] private AudioSource backGroundMusicAudioSource;
    [SerializeField] private AudioClip normal;
    private bool eG = false;
    [SerializeField] private AudioClip endgame;
    private bool gO = false;
    [SerializeField] private AudioClip gameOver;


    private float bonusProbabilityStack;
    private float generatedProbability;
    private float totalProbability;
    private float suceeseProbability;

    public bool isAcornEvent;
    private int acornEventHappenedTime;

    public static PlayerInputManager inputManager; //for easy refs 
    public static GameSystemHandler instance; 

    [Header("UI Refs")]
    private GameObject sueingButton1;
    private GameObject sueingButton2;
    [SerializeField] TextMeshProUGUI player1TurnText;
    [SerializeField] TextMeshProUGUI player2TurnText;

    [SerializeField] GameObject GameoverScene;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI loseText;


    private void Awake()
    {
        //Stuff to assign player controllers during runtime might come back to this later
        //inputManager = GetComponent<PlayerInputManager>();

        //InputDevice controller1 = Gamepad.all[0];
        //InputDevice controller2 = Gamepad.all[1];

        //Gamepad[] gamepads = Gamepad.all.ToArray();

        //Debug.Log(controller1);
        //Debug.Log(controller2);

        //PlayerInput input1;
        //PlayerInput input2;

        //input1 = PlayerInput.Instantiate(player1Ref, 0, pairWithDevice: gamepads[0]);
        // input2 = PlayerInput.Instantiate(player2Ref, 1, controlScheme: "Default", pairWithDevice: gamepads[1]);

        //inputManager.JoinPlayer(controlScheme: "Default");
        //inputManager.playerPrefab = player2Ref;
        //inputManager.JoinPlayer(controlScheme: "Default");
        //inputManager.enabled = true;


        //Debug.Log(GameObject.Find("Player").GetComponent<PlayerInput>().user.valid);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        

        shakeController = GetComponent<ShakeController>();
        bonusProbabilityStack = 1.2f;
        acornEventHappenedTime = 0;
        suceeseProbability = successRateForStartGame;
        isAcornEvent = false;
        StartCoroutine(TryCallingSpawnAcorn(Random.Range(20.0f,30.0f)));

        
        sueingButton1 = GameObject.Find("Sue Button P1");
        sueingButton2 = GameObject.Find("Sue Button P2");

        //player1Ref = GameObject.Find("Player(Clone)");
        //player2Ref = GameObject.Find("Opponent(Clone)"); 
        backGroundMusicAudioSource.clip = normal;
        backGroundMusicAudioSource.Play();

        //Invoke("PlayerGameOverScene", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkerData.IsWhiteTurn() && isAcornEvent)
        {
            sueingButton1.SetActive(false);
            sueingButton2.SetActive(true);
        }
        else if (isAcornEvent)
        {
            sueingButton1.SetActive(true);
            sueingButton2.SetActive(false);
        }
        else
        {
            sueingButton1.SetActive(false);
            sueingButton2.SetActive(false);
        }

        if (checkerData.IsWhiteTurn())
        {
            player1TurnText.color = Color.red;
            player2TurnText.color = Color.red;
            player1TurnText.text = "Red's Turn";
            player2TurnText.text = "Red's Turn";
        }
        else
        {
            player1TurnText.color = Color.blue;
            player2TurnText.color = Color.blue;
            player1TurnText.text = "Blue's Turn";
            player2TurnText.text = "Blue's Turn";
        }
    }

    IEnumerator TryCallingSpawnAcorn(float delay){

            if (checkerData.whitePieceLeft<=3 || checkerData.blackPieceLeft <=3){
                PlayEndgamePhase();
            }
            if (delay != 0){
                yield return new WaitForSeconds(5.0f);
                isAcornEvent = false;
                yield return new WaitForSeconds(delay - 5.0f);
            }
            generatedProbability = Random.Range(0.0f , 1.0f);
            totalProbability = generatedProbability + bonusProbabilityStack;

            if(totalProbability >= suceeseProbability){
                bonusProbabilityStack = 0.0f;
                StartCoroutine(SpawnAcornEvent());
            }

            else{
                bonusProbabilityStack = bonusProbabilityStack + 0.1f;
                //Depends on how many piece left, the timer should be adjust (the closer to the endgame, the lesser it is)
                if (checkerData.whitePieceLeft==0 || checkerData.blackPieceLeft ==0)
                {

                }

                else if (checkerData.whitePieceLeft<=3 || checkerData.blackPieceLeft <=3)
                {
                    suceeseProbability = successRateForLateGame;
                    yield return new WaitForSeconds(Random.Range(minTimerForLateGame , maxTimerForLateGame));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }

                else if (checkerData.whitePieceLeft<=6 || checkerData.blackPieceLeft <=6)
                {
                    suceeseProbability = successRateForMidGame;
                    yield return new WaitForSeconds(Random.Range(minTimerForMidGame , maxTimerForMidGame));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }

                else if (checkerData.whitePieceLeft<=9 || checkerData.blackPieceLeft <=9)
                {
                    suceeseProbability = successRateForEarlyGame;
                    yield return new WaitForSeconds(Random.Range(minTimerForEarlyGame , maxTimerForEarlyGame));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }

                else
                {
                    yield return new WaitForSeconds(Random.Range(minTimerForStartGame , maxTimerForStartGame));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }
                
            }
    }

    // Acorn Event Content
    IEnumerator SpawnAcornEvent(){
        isAcornEvent = true;  //Acorns start dropping. Camera shake required. shake for 1~2s max
        Debug.Log("Acorn Event is true");
        acornEventHappenedTime ++;
        Debug.Log("Acorn event happens the" + acornEventHappenedTime.ToString()+" times");
        shakeController.StartShaking();
        checkerData.SaveBoardState();

        if (acornEventHappenedTime <= 2)
        {
            int spawningNumber = 2;
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }
            
            }
        }

        else if (acornEventHappenedTime > 2 && acornEventHappenedTime <= 8)
        {
            int spawningNumber = Random.Range(2 , 4);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else if(i%4 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }
            
            }
        }

        else if (acornEventHappenedTime > 8 && acornEventHappenedTime <= 15)
        {
            int spawningNumber = Random.Range(3 , 5);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else if(i%4 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }
            
            }
        }

        else 
        {
            int spawningNumber = Random.Range(3 , 6);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));
                }

                else if(i%4 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 6f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));
                }
            
            }
        }
         //Acorns stop dropping.


        //Depends on how many piece left, the timer should be adjust (the closer to the endgame, the lesser it is)
        if (checkerData.whitePieceLeft==0 || checkerData.blackPieceLeft ==0)
        {

        }

        else if (checkerData.whitePieceLeft<=3 || checkerData.blackPieceLeft <=3)
        {
            suceeseProbability = successRateForLateGame;
            StartCoroutine(TryCallingSpawnAcorn(Random.Range(10.0f,15.0f)));  // start to genarate float to call acorn event again.
        }

        else if(checkerData.whitePieceLeft<=6 || checkerData.blackPieceLeft <=6)
        {
            suceeseProbability = successRateForMidGame;
            StartCoroutine(TryCallingSpawnAcorn(Random.Range(12.0f,17.0f)));  // start to genarate float to call acorn event again.
        }

        else if(checkerData.whitePieceLeft<=9 || checkerData.blackPieceLeft <=9)
        {
            suceeseProbability = successRateForEarlyGame;
            StartCoroutine(TryCallingSpawnAcorn(15.0f));  // start to genarate float to call acorn event again.
        }

        else
        {
            StartCoroutine(TryCallingSpawnAcorn(20.0f));  // start to genarate float to call acorn event again.
        }  


        
    }

    public void PlayEndgamePhase(){
        if(eG == false){
            eG = true;
            backGroundMusicAudioSource.clip = endgame;
            backGroundMusicAudioSource.Play();
        }
    }

    public void PlayGameoverPhase(){

        PlayerGameOverScene();
        if(gO == false){
            gO = true;
            backGroundMusicAudioSource.clip = gameOver;
            backGroundMusicAudioSource.loop = false;
            backGroundMusicAudioSource.Play();
        }
    }

    public void PlayerGameOverScene(){

        if (checkerData.whitePieceLeft==0 || player1Ref.GetComponent<Player_HealthSystem>().currentHP <=0)
        {
            winText.text = "Blue fly wins!";
            loseText.text = "Red fly loses ...";
        }
        else
        {
            winText.text = "Red fly wins!";
            loseText.text = "Blue fly loses...";
        }
        player1TurnText.gameObject.SetActive(false);
        player2TurnText.gameObject.SetActive(false);
        GameoverScene.SetActive(true);
    }


    public void PlayGame ()
    { // Load the checkers game scene
        if (GameObject.Find("Opponent(Clone)") != null)
        {
            GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>().SetHealth(3);
            GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>().state = Player_HealthSystem.characterState.Idle;
            GameObject.Find("Opponent(Clone)").GetComponent<Player_HealthSystem>().winState = Player_HealthSystem.winOrLose.Draw;
        }
        SceneManager.LoadScene("CheckersDefault");
    }

    public void BackToMainMenu ()
    { // Load the checkers game scene
        SceneManager.LoadScene("MainMenu");
    }

}
