using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerAnim playerAnim;
    [SerializeField] private Transform hitBox;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private int damage;
    [Range(0.2f, 4f)][SerializeField] private float range = 0.5f;
    [SerializeField] private Collider[] hitInfo;
    void Start()
    {
        playerAnim = GetComponent<PlayerAnim>();
    }
    void Update()
    {
        AttackKeyboard();
    }

    private void OnDrawGizmosSelected() {
        if(hitBox!= null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitBox.position, range);
        }
    }

    private void Attack(){
        playerAnim.MelleAtack();
        hitInfo = Physics.OverlapSphere(hitBox.position, range, hitMask);

        foreach(Collider c in hitInfo){
            c.gameObject.SendMessage("GetHit",damage,SendMessageOptions.DontRequireReceiver);
        }
    }

    private void AttackKeyboard(){
        if(Input.GetKeyDown(KeyCode.Space)){
            this.Attack();
        }
    }
}
