using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Values of AcornFallingEvent;
    private float bonusProbabilityStack;
    private float generatedProbability;
    private float totalProbability;
    private float suceeseProbability;

    public bool isAcornEvent;
    private int acornEventHappenedTime;



    // Start is called before the first frame update
    void Start()
    {
        bonusProbabilityStack = 0.0f;
        acornEventHappenedTime = 0;
        suceeseProbability = 0.85f;
        isAcornEvent = false;
        StartCoroutine(TryCallingSpawnAcorn(20.0f));
        
        
    }

    // Update is called once per frame
    void Update()
    {

            
    }

    // Try to start Acorn event if the float is >= 0.8
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
                bonusProbabilityStack = bonusProbabilityStack + 0.15f;
                //Depends on how many piece left, the timer should be adjust (the closer to the endgame, the lesser it is)
                if (checkerData.whitePieceLeft==0 || checkerData.blackPieceLeft ==0)
                {

                }

                else if (checkerData.whitePieceLeft<=3 || checkerData.blackPieceLeft <=3)
                {
                    suceeseProbability = 0.5f;
                    yield return new WaitForSeconds(Random.Range(6.0f , 8.0f));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }

                else if (checkerData.whitePieceLeft<=6 || checkerData.blackPieceLeft <=6)
                {
                    suceeseProbability = 0.65f;
                    yield return new WaitForSeconds(Random.Range(5.0f , 8.0f));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }

                else if (checkerData.whitePieceLeft<=9 || checkerData.blackPieceLeft <=9)
                {
                    suceeseProbability = 0.7f;
                    yield return new WaitForSeconds(Random.Range(7.0f , 8.0f));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }

                else
                {
                    yield return new WaitForSeconds(Random.Range(7.0f , 10.0f));
                    StartCoroutine(TryCallingSpawnAcorn(0.0f));
                }
                
            }
    }

    // Acorn Event Content
    IEnumerator SpawnAcornEvent(){
        isAcornEvent = true;  //Acorns start dropping. Camera shake required. shake for 1~2s max
        acornEventHappenedTime ++;

        if (acornEventHappenedTime <= 2)
        {
            int spawningNumber = Random.Range(2 , 3);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }
            
            }

            isAcornEvent = false;
        }

        else if (acornEventHappenedTime > 2 && acornEventHappenedTime <= 4)
        {
            int spawningNumber = Random.Range(2 , 4);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else if(i%4 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }
            
            }

            isAcornEvent = false;
        }

        else if (acornEventHappenedTime > 4 && acornEventHappenedTime <= 10)
        {
            int spawningNumber = Random.Range(3 , 5);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else if(i%4 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.3f,0.6f));
                }
            
            }

            isAcornEvent = false;
        }

        else 
        {
            int spawningNumber = Random.Range(4 , 6);
            for (int i=0 ; i < spawningNumber ; i++){

                if(i%2 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));
                }

                else if(i%4 == 0){
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));
                }

                else{
                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
                    yield return new WaitForSeconds(Random.Range(0.4f,0.6f));

                    spawningObject = Instantiate(spawnableObjects[0]);
                    spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                    spawningObject.transform.position = spawningLocRef.transform.position;
                    spawningObject.transform.rotation = Random.rotation;
                    Destroy(spawningObject, 5f);
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
            suceeseProbability = 0.5f;
            //yield return new WaitForSeconds(5.0f); // a 10s period that no acorn event happens.
            Debug.Log("Are you ready?");
            StartCoroutine(TryCallingSpawnAcorn(5.0f));  // start to genarate float to call acorn event again.
            Debug.Log("Yes!");
        }

        else if(checkerData.whitePieceLeft<=6 || checkerData.blackPieceLeft <=6)
        {
            suceeseProbability = 0.65f;
             //yield return new WaitForSeconds(10.0f); // a 10s period that no acorn event happens.
            Debug.Log("Are you ready?");
            StartCoroutine(TryCallingSpawnAcorn(6.0f));  // start to genarate float to call acorn event again.
            Debug.Log("Yes!");
        }

        else if(checkerData.whitePieceLeft<=9 || checkerData.blackPieceLeft <=9)
        {
            suceeseProbability = 0.7f;
            //yield return new WaitForSeconds(10.0f); // a 10s period that no acorn event happens.
            Debug.Log("Are you ready?");
            StartCoroutine(TryCallingSpawnAcorn(8.0f));  // start to genarate float to call acorn event again.
            Debug.Log("Yes!");
        }

        else
        {
            //yield return new WaitForSeconds(10.0f); // a 10s period that no acorn event happens.
            Debug.Log("Are you ready?");
            StartCoroutine(TryCallingSpawnAcorn(10.0f));  // start to genarate float to call acorn event again.
            Debug.Log("Yes!");
        }           
        
    }
}
