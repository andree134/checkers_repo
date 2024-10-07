using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    private float bonusProbabilityStack;
    private float generatedProbability;
    private float totalProbability;
    private float suceeseProbability;

    public bool isAcornEvent;
    private int acornEventHappenedTime;

    public static PlayerInputManager inputManager; 

    // Start is called before the first frame update
    void Start()
    {
        shakeController = GetComponent<ShakeController>();
        bonusProbabilityStack = 1.2f;
        acornEventHappenedTime = 0;
        suceeseProbability = successRateForStartGame;
        isAcornEvent = false;
        StartCoroutine(TryCallingSpawnAcorn(Random.Range(20.0f,30.0f)));

        inputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {

            
    }

    IEnumerator TryCallingSpawnAcorn(float delay){
            if (delay != 0)
                yield return new WaitForSeconds(delay);

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

            isAcornEvent = false;
            Debug.Log("Acorn event false");
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

            isAcornEvent = false;
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

            isAcornEvent = false;
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

            isAcornEvent = false;
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
}
