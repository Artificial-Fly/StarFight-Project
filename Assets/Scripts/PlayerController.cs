using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject globVars;
    //Components:
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    //--
    GlobalVariables globalVariables;
    private GameObject globalVars;
    //
    //
    //
    //states:
    private bool isOnGround = false;//player avatar has hit ground
    private bool jumpKeyReady = true;//jump is ready for use
    //
    //
    private float waitTime = 2.0f;
    private float jumpForce = 24.9f;
    private float gravityMod = 0.15f;
    //
    //
    // Start is called before the first frame update
    void Start()
    {
        //Getting the components
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        //--
        globalVars = GameObject.FindWithTag("Globals");
        globalVariables = globalVars.GetComponent<GlobalVariables>();
        //Setting parameters
        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void Jump(){
        if(!isOnGround){
            if(Input.GetKeyDown(KeyCode.Space)){
                if(jumpKeyReady){
                    playerRb.AddForce(jumpForce* Vector3.up, ForceMode.VelocityChange);
                    jumpKeyReady = false;
                    Debug.Log("JUMP!");
                    StartCoroutine("JumpWindow");
                }else{
                    FlipJump();
                }
            }
            //if(!globalVariables.PlayerAtScene){
            //    playerRb.AddForce((-1.35f * jumpForce)* Vector3.up, ForceMode.VelocityChange);
            //    globalVariables.PlayerAtScene = true;
            //}
        }
    }
    void FlipJump(float flipStrength=-1.35f){
        playerRb.AddForce((flipStrength * jumpForce)* Vector3.up, ForceMode.VelocityChange);
        Debug.Log("FLIP!");
        jumpKeyReady = true;
    }
    IEnumerator JumpWindow(){
        yield return new WaitForSeconds(waitTime);
        jumpKeyReady = true;
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Ground")){
            Debug.Log("Player has hit the ground!");
            isOnGround = true;
        }else if(collision.gameObject.CompareTag("TopBoundary")){
            Debug.Log("Player has left the scene!");
            FlipJump(0.75f);
        }
    }
}
