using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    private Animator monsterAnimator;
    private MonsterState state;
    private NavMeshAgent monsterNavMesh;
    [Header("Monster Controls")]
    [SerializeField] private float monsterWaitTime = 1.5f;
    [SerializeField] private float remaininnStopDistance = 2f;
    [SerializeField] private int HP = 3;
    private int currentHp;
    [SerializeField] private Transform[] patrolWaitPoints;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = HP;
        monsterAnimator = GetComponent<Animator>();
        monsterNavMesh = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }

    void Update()
    {
        
    }

    private IEnumerator Idle(){
        state = MonsterState.IDLE;
        monsterNavMesh.destination = transform.position;
        yield return new WaitForSeconds(monsterWaitTime);
        StayStill(60);
    }

    private IEnumerator Patrol(){
        state = MonsterState.PATROL;
        var idWaypoints = Random.Range(0, patrolWaitPoints.Length);
        var monsterDestination = patrolWaitPoints[idWaypoints].position;
        monsterNavMesh.destination = monsterDestination;
        monsterAnimator.SetBool("isWalk", true);
        yield return new WaitUntil(()=> monsterNavMesh.remainingDistance <= remaininnStopDistance);
        monsterAnimator.SetBool("isWalk", false);
        StayStill(30);
    }

    private void StayStill(int stayStillPorcentage){
        if(Rand() <= stayStillPorcentage){
            StartCoroutine(Idle());
        }else{
            StartCoroutine(Patrol());
        }
    }

    private int Rand(){
        return Random.Range(0,100);
    }

    public void GetHit(int amount){
        monsterAnimator.SetBool("isWalk", false);
        currentHp -= amount;
        StopAllCoroutines();
        if(currentHp <= 0){
            this.gameObject.SetActive(false);
            Respawn();
        }
    }

    private void Respawn(){
        currentHp = HP;
        var respawnPosition = patrolWaitPoints[Random.Range(0, patrolWaitPoints.Length)].position;
        transform.position = respawnPosition;
        gameObject.SetActive(true);
        StayStill(30);
    }
}
