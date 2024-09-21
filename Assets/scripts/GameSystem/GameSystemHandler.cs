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

    public bool isAcornEvent;



    // Start is called before the first frame update
    void Start()
    {
        bonusProbabilityStack = 0.0f;
        isAcornEvent = false;
        StartCoroutine(TryCallingSpawnAcorn());
        
}

    // Update is called once per frame
    void Update()
    {

            
    }

    // Try to start Acorn event if the float is >= 0.8
    IEnumerator TryCallingSpawnAcorn(){
             //yield return new WaitForSeconds(Random.Range(6.0f , 12.0f));

            generatedProbability = Random.Range(0.0f , 1.0f);
            totalProbability = generatedProbability + bonusProbabilityStack;

            if(totalProbability >= 0.8f){
                bonusProbabilityStack = 0.0f;
                StartCoroutine(SpawnAcornEvent());
            }

            else{
                bonusProbabilityStack = bonusProbabilityStack + 0.2f;
                yield return new WaitForSeconds(Random.Range(5.0f , 10.0f));
                StartCoroutine(TryCallingSpawnAcorn());
            }
    }

    // Acorn Event Content
    IEnumerator SpawnAcornEvent(){
        isAcornEvent = true;  //Acorns start dropping.

        int spawningNumber = Random.Range(3 , 6);
        for (int i=0 ; i < spawningNumber ; i++){

            if(i%2 == 0){
                spawningObject = Instantiate(spawnableObjects[0]);
                spawningLocRef = whiteSideSpawningREF[Random.Range(0,7)];
                spawningObject.transform.position = spawningLocRef.transform.position;
                spawningObject.transform.rotation = Random.rotation;
                Destroy(spawningObject, 5f);
                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));

                spawningObject = Instantiate(spawnableObjects[0]);
                spawningLocRef = blackSideSpawningREF[Random.Range(0,7)];
                spawningObject.transform.position = spawningLocRef.transform.position;
                spawningObject.transform.rotation = Random.rotation;
                Destroy(spawningObject, 5f);
                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));
            }

            else if(i%4 == 0){
                spawningObject = Instantiate(spawnableObjects[0]);
                spawningLocRef = whiteSideSpawningREF[Random.Range(10,13)];
                spawningObject.transform.position = spawningLocRef.transform.position;
                spawningObject.transform.rotation = Random.rotation;
                Destroy(spawningObject, 5f);
                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));

                spawningObject = Instantiate(spawnableObjects[0]);
                spawningLocRef = blackSideSpawningREF[Random.Range(10,13)];
                spawningObject.transform.position = spawningLocRef.transform.position;
                spawningObject.transform.rotation = Random.rotation;
                Destroy(spawningObject, 5f);
                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));
            }

            else{
                spawningObject = Instantiate(spawnableObjects[0]);
                spawningLocRef = whiteSideSpawningREF[Random.Range(0,10)];
                spawningObject.transform.position = spawningLocRef.transform.position;
                spawningObject.transform.rotation = Random.rotation;
                Destroy(spawningObject, 5f);
                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));

                spawningObject = Instantiate(spawnableObjects[0]);
                spawningLocRef = blackSideSpawningREF[Random.Range(0,10)];
                spawningObject.transform.position = spawningLocRef.transform.position;
                spawningObject.transform.rotation = Random.rotation;
                Destroy(spawningObject, 5f);
                yield return new WaitForSeconds(Random.Range(0.2f,0.6f));
            }
            
        }

        isAcornEvent = false; //Acorns stop dropping.

        yield return new WaitForSeconds(10.0f); // a 10s period that no acorn event happens.
        StartCoroutine(TryCallingSpawnAcorn());  // start to genarate float to call acorn event again.


    }
}
