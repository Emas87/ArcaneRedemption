using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpSpeed = 29f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }


    void  OnMove(InputValue val){
        moveInput = val.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    void OnJump(InputValue val){
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            if(val.isPressed){
                myRigidBody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }

}
