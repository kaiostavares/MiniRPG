using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment: MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private CharacterController playerController;
    public float movementSpeed = 6f;
    private float verticalMovmentValue;
    private float horizontalMovmentValue; 
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        MovementInputs();
    }

    private void MovementInputs(){
        this.horizontalMovmentValue = Input.GetAxis("Horizontal");
        this.verticalMovmentValue = Input.GetAxis("Vertical");
        MoveCharacter();
    }

    private void MoveCharacter(){
        Vector3 cameraForward = this.playerCamera.transform.forward;
        Vector3 cameraRight = this.playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 direction = ((cameraForward.normalized * this.verticalMovmentValue) + (cameraRight.normalized * this.horizontalMovmentValue)).normalized;

        if(direction.magnitude > 0.1f){
            transform.LookAt(transform.position + direction);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        playerController.SimpleMove(direction * movementSpeed);
    }
}
