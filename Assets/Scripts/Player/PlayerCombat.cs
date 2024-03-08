using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerAnim playerAnim;
    void Start()
    {
        playerAnim = GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackKeyboard();
    }

    private void Attack(){
        playerAnim.MelleAtack();
    }

    private void AttackKeyboard(){
        if(Input.GetKeyDown(KeyCode.Space)){
            this.Attack();
        }
    }
}
