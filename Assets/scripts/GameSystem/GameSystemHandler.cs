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

    [SerializeField]
    private GameObject[] spawnableObjects;

    private GameObject spawningObject;

    //Values of AcornFallingEvent;
    private float bonusProbabilityStack;
    private float generatedProbability;
    private float totalProbability;

    public bool isAcornEvent;



    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(TryCallingSpawnAcorn());
        isAcornEvent = false;
}

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.P)){
            StartCoroutine(SpawnAcornEvent());
         }

            
    }

    // Try to start Acorn event if the float is >= 0.8
    IEnumerator TryCallingSpawnAcorn(){
        while (true){
             yield return new WaitForSeconds(Random.Range(6.0f , 12.0f));

            generatedProbability = Random.Range(0.0f , 1.0f);
            totalProbability = generatedProbability + bonusProbabilityStack;

            if(totalProbability >= 0.8f){
                bonusProbabilityStack = 0.0f;
                StartCoroutine(SpawnAcornEvent());
                StopCoroutine(TryCallingSpawnAcorn());
            }

            else{
                bonusProbabilityStack = bonusProbabilityStack + 0.2f;
            }

            
        }
    }

    // Acorn Event Content
    IEnumerator SpawnAcornEvent(){
        isAcornEvent = true;  //Acorns start dropping.

        int spawningNumber = Random.Range(2 , 5);
        for (int i=0 ; i < spawningNumber ; i++){
            spawningObject = Instantiate(spawnableObjects[0]);
            spawningObject.transform.position = new Vector3 (6.0f + Random.Range(-15.0f,15.0f), 44.0f , 25.0f + Random.Range(-25.0f , 15.0f));
                        spawningObject.transform.position = new Vector3 (-2.0f + Random.Range(-15.0f,15.0f), 44.0f , -23.0f + Random.Range(-25.0f , 15.0f));
            spawningObject.transform.rotation = Random.rotation;
            Destroy(spawningObject, 5f);
            yield return new WaitForSeconds(0.5f);
        }

        isAcornEvent = false; //Acorns stop dropping.

        yield return new WaitForSeconds(10.0f); // a 10s period that no acorn event happens.
        StartCoroutine(TryCallingSpawnAcorn());  // start to genarate float to call acorn event again.


    }
}
