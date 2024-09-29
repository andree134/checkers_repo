using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset_Adjustment : MonoBehaviour
{
    [SerializeField]
    private Player_HealthSystem playerSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSystem.state == Player_HealthSystem.characterState.Idle){
            transform.localPosition = new Vector3(0.0f , -0.876f , 0.0f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
