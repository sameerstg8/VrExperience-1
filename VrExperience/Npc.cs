using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{

    public States currentState;
    NavMeshAgent agent;
    public float radiusToWalk;
    public Vector3 toGoPosition,orignalPosition;
    public Terrain terrain;
    
        
    public float time, idleTime,walkingTime,runningTime,randWalkingTime,randIdleTime;
    private Animator animator;

    private void Awake()
    {
        terrain = FindObjectOfType<Terrain>();
        agent = GetComponent<NavMeshAgent>();
        orignalPosition = transform.position;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        ChangeState(States.idle);
    }
    void ChangeState(States state)
    {
        currentState = state;
        time = 0;
        if (state == States.idle)
        {
            animator.SetBool("walk", false);
            agent.isStopped = true;
            randIdleTime = Random.Range(idleTime - 1, idleTime + 1);

        }
        else if(state == States.walking)
        {
            randWalkingTime = Random.Range(walkingTime - 1, walkingTime + 1);

            agent.isStopped = false;
            toGoPosition = new Vector3(orignalPosition.x + Random.Range(-radiusToWalk, radiusToWalk), 0,orignalPosition.z + Random.Range(-radiusToWalk, radiusToWalk));
            toGoPosition.y= terrain.SampleHeight(toGoPosition);
            animator.SetBool("walk", true);
            transform.LookAt(toGoPosition);

        }

    }
    void Update()
    {
        time += Time.deltaTime;
        if (currentState==States.walking )
        {
            if (time >= randWalkingTime|| Vector3.Distance(transform.position,toGoPosition)<=agent.stoppingDistance)
            {
                ChangeState(States.idle);
            }
            else
            {
                agent.SetDestination(toGoPosition);
            }

        }
        else if (currentState == States.idle )
        {
            if (time >= randIdleTime)
            {

                 ChangeState(States.walking);
                
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(toGoPosition, 1);
    }

}
public enum States
{
    idle,running,walking,taunt
}
