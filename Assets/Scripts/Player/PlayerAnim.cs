using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMovment playerMovment;
    private bool isAttackProgress = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(bool state){
        this.anim.SetBool("isWalk", state);
    }

    public void MelleAtack(){
        if(isAttackProgress) return;
        this.anim.SetTrigger("Attack");
        StartCoroutine(SetPlayerSpeedToZero());
    }

    private IEnumerator SetPlayerSpeedToZero(){
        isAttackProgress = true;
        var speed = playerMovment.movementSpeed;
        playerMovment.movementSpeed = 0f;
        yield return new WaitForSeconds(0.7f);
        playerMovment.movementSpeed = speed;
        isAttackProgress = false;
    }

}
