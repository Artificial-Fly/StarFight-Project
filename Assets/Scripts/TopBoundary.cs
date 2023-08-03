using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBoundary : MonoBehaviour
{
    //Components:
    GlobalVariables globalVariables;
    private GameObject globalVars;
    // Start is called before the first frame update
    void Start()
    {
        globalVars = GameObject.FindWithTag("Globals");
        globalVariables = globalVars.GetComponent<GlobalVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag!="Player"){//TopBoundary
            //globalVariables.PlayerAtScene = false;
            Debug.Log("PLAYER HAS LEFT THE SCENE!");
        }
    }
}
