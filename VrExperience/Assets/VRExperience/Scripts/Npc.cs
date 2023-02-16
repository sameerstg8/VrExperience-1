using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using TMPro;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{


    [Header("Debug")]
    public string stateName;
    // Permits
    public bool changeStatePermit;
    public int changeStatePermitCount;
    public bool attackPermit;
    public string stateType;
    public NpcState lastState;
    public Vector3 whereGoing;
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]


    public bool friendlyFire;
    public bool NexusReacted;
    bool damageLock;
    public string enemyName;
    [HideInInspector]
    internal Animator animator;
    [HideInInspector]
    internal NavMeshAgent agent;

    public GameObject player;
    public float distanceFromPlayer;
    #region States
    internal NpcState currentState;
    [HideInInspector]
    internal IdleState IdleState = new IdleState();
    [HideInInspector]
    internal ChillState ChillState = new ChillState();
    [HideInInspector]
    internal PattrollingState PattrollingState = new PattrollingState();
    [HideInInspector]
    internal AttackingState AttackingState = new AttackingState();
    [HideInInspector]
    internal ChasingState ChasingState = new ChasingState();
    [HideInInspector]
    internal GetHitState GetHitState = new GetHitState();
    [HideInInspector]
    internal DeathState DeathState = new DeathState();
    [HideInInspector]
    internal GoingToItsPositionState GoingToItsPositionState = new GoingToItsPositionState();
    internal UnconciousState unconciousState = new UnconciousState();

    #endregion

    // Assignment Properties

    AudioSource audioSource;
    TextMeshProUGUI text;

    public Terrain terrainData;
    internal Vector3 orignalPosition;
    UnityEngine.UI.Slider slider;
    internal Rigidbody rb;
    //  Npc Configuration properties
    public int level;
    public float attackValue;
    public float healthValue;
    public float defenceValue;
    public float criticalRate;
    public float criticalDamage;


    public bool hybridEnemy;
    public Tuple<Vector2, Vector2> minMaxRangeToPatrol;
    public float attackingDistance;
    [HideInInspector]
    public float attackingPointDistance;
    public float chasingDistance;
    public bool isChillEnemy;
    public int chillIndex;
    public bool isSpyMode;
    public float chasingLimitMax;
    public float defaultSpeed, runningSpeed;
    [HideInInspector]
    public Vector3 teampPosition;
    public int attackComboIndex;

    public bool isDead;
    bool timeStartPermit;
    public float time;
    public float attackCooldown;
    public float enemyAreaLimitation;
    public float distanceFromOrignalPos;
    public float distaceFromPatrolDestination;
    public bool isEnvy;

    //[SerializeField] float elementalBuffCDTime;
    [SerializeField] List<GameObject> elementalBuffIcons = new List<GameObject>();


 













    public enum StateEnum
    {
        Idle, IdleLookAround, Patrol, PatrolForward, PatrolLeft, PatrolRight
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        terrainData = FindObjectOfType<Terrain>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        damageLock = true;
        defaultSpeed = agent.speed;
        orignalPosition = transform.position;
        runningSpeed = agent.speed * 1.5f;
        enemyAreaLimitation = 50f;
        chasingLimitMax = 50;
        attackCooldown = .8f;









        if (isChillEnemy)
        {
            SwitchSate(ChillState);
        }
        else
        {
            SwitchSate(IdleState);
        }


        minMaxRangeToPatrol = new Tuple<Vector2, Vector2>(new Vector2(gameObject.transform.position.x - 25, gameObject.transform.position.z - 25), new Vector2(gameObject.transform.position.x + 25, gameObject.transform.position.z + 25));
        changeStatePermit = true;
        changeStatePermitCount = 0;
    }

    private void Update()
    {
            //distanceFromPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (timeStartPermit)
        {
            time += Time.deltaTime;
        }
        currentState.UpdateState(this);
        distaceFromPatrolDestination = Vector3.Distance(transform.position, teampPosition);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(teampPosition, 5);


    }
    private void OnTriggerEnter(Collider other)
    {
        currentState.TriggerState(this, other);
        //if (friendlyFire && other.gameObject.tag == "AttackingPointEnemy")
        //{
        //    TakeDamage(attackValue, 0);
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        currentState.TriggerState(this, other);
    }
    public void SwitchSate(NpcState state)
    {
        if (lastState == unconciousState)
        {
            agent.enabled = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        lastState = currentState;
        print(agent.stoppingDistance);
        attackingDistance = agent.stoppingDistance;
        audioSource.Stop();
        audioSource.clip = null;
        currentState = state;
        currentState.EnterState(this);
        changeStatePermitCount = 0;
        changeStatePermit = false;
        attackPermit = false;
        try
        {
            stateName = $"{currentState}";
        }
        catch (Exception)
        {


        }

    }
    public void DrawSphere(Vector3 position)
    {
        teampPosition = position;
    }
    public void ChangeStatePermit()
    {
        changeStatePermit = true;
        changeStatePermitCount += 1;
        distanceFromOrignalPos = Vector3.Distance(transform.position, orignalPosition);

    }
    public void ChangeStatePermitFalse()
    {
        changeStatePermit = false;
        changeStatePermitCount = 0;

    }
    public void AttackPermitTrue(int index)
    {
        attackPermit = true;
        attackComboIndex = index;

    }
    public void AttackPermitFalse()
    {
        attackPermit = false;
    }

    public void StartTime()
    {
        time = 0;
        timeStartPermit = true;
    }
    public void ResetTime()
    {
        time = 0;
        timeStartPermit = false;
    }
    public void GetHit()
    {
        SwitchSate(GetHitState);
        Debug.Log("Flinch");
    }
    public void Death()
    {
        if (currentState != DeathState)
        {
            SwitchSate(DeathState);


        }
        Debug.Log("Dead");
        isDead = true;
        /*InEnemyRange._instance.RemoveDeadEnemy(gameObject);*/

        /*        RPGCharacterAnims.InEnemyRange._instance.DiscardEnemy(gameObject);
        */
    }



}

[System.Serializable]
public abstract class NpcState
{

    public abstract void EnterState(Npc enemy);
    public abstract void UpdateState(Npc enemy);

    public abstract void TriggerState(Npc enemy, Collider collider);
}



[System.Serializable]
public class IdleState : NpcState
{
    public enum StateType
    {
        Ground, Fly
    }


    public override void EnterState(Npc enemy)
    {
        var randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0 && enemy.hybridEnemy)
        {
            enemy.stateType = StateType.Fly.ToString();
        }
        else
        {
            enemy.stateType = StateType.Ground.ToString();
        }

        enemy.StartTime();
        if (enemy.agent.isActiveAndEnabled && enemy.agent.isOnNavMesh)
        {
            enemy.agent.isStopped = true;
        }

        enemy.animator.SetTrigger(enemy.stateType);
        enemy.animator.SetTrigger("Idle 1");
        try
        {
            enemy.transform.LookAt(enemy.player.transform);

        }
        catch (Exception)
        {


        }
        enemy.distanceFromOrignalPos = Vector3.Distance(enemy.transform.position, enemy.orignalPosition);

    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {
        if (enemy.time > 1)
        {
            if (enemy.distanceFromPlayer <= enemy.attackingDistance && enemy.attackCooldown <= enemy.time && !enemy.isSpyMode)
            {
                enemy.SwitchSate(enemy.AttackingState);
            }
            else if (enemy.distanceFromPlayer < enemy.chasingLimitMax && enemy.changeStatePermit && !enemy.isSpyMode)
            {
                enemy.SwitchSate(enemy.ChasingState);
            }
            else if (enemy.distanceFromOrignalPos >= enemy.enemyAreaLimitation && enemy.changeStatePermit)
            {
                enemy.SwitchSate(enemy.GoingToItsPositionState);
            }
            else if (enemy.changeStatePermit && enemy.changeStatePermitCount >= 2)
            {
                enemy.SwitchSate(enemy.PattrollingState);
            }
        }

    }
}
[System.Serializable]
public class ChillState : NpcState
{
    public enum StateType
    {
        Ground, Fly
    }

    public override void EnterState(Npc enemy)
    {

        var randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0 && enemy.hybridEnemy)
        {
            enemy.stateType = StateType.Fly.ToString();
        }
        else
        {
            enemy.stateType = StateType.Ground.ToString();
        }
        enemy.agent.isStopped = true;
        enemy.animator.SetTrigger(enemy.stateType);
        if (enemy.chillIndex == 0)
        {
            enemy.animator.SetTrigger("Chill 1");
        }
        else if (enemy.chillIndex == 1)
        {
            enemy.animator.SetTrigger("Chill 2");

        }
        else if (enemy.chillIndex == 2)
        {
            enemy.animator.SetTrigger("Chill 3");

        }
        enemy.transform.LookAt(enemy.player.transform);
    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {
        if (enemy.distanceFromPlayer < enemy.attackingDistance && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.AttackingState);
        }
        else if (enemy.distanceFromPlayer < 50 && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.ChasingState);
        }
        /*        else if (enemy.changeStatePermit && enemy.changeStatePermitCount >=2)
                {
                    enemy.SwitchSate(enemy.PattrollingState);
                }
        */
    }
}
[System.Serializable]
public class PattrollingState : NpcState
{
    Vector3 pointToGo = new Vector3();

    public override void EnterState(Npc enemy)
    {
        enemy.agent.speed = enemy.defaultSpeed;
        enemy.agent.isStopped = false;
        int rand1 = UnityEngine.Random.Range(0, 2);
        int rand2 = UnityEngine.Random.Range(0, 2);
        enemy.animator.SetTrigger(enemy.stateType);
        enemy.animator.SetTrigger("Patrol 1");

        if (rand1 == 0)
        {
            if (rand2 == 0)
            {
                pointToGo.x = enemy.minMaxRangeToPatrol.Item1.x;
                pointToGo.z = enemy.minMaxRangeToPatrol.Item1.y;
            }
            else
            {
                pointToGo.x = enemy.minMaxRangeToPatrol.Item1.x;
                pointToGo.z = enemy.minMaxRangeToPatrol.Item2.y;

            }
        }
        else
        {
            if (rand2 == 0)
            {
                pointToGo.x = enemy.minMaxRangeToPatrol.Item2.x;
                pointToGo.z = enemy.minMaxRangeToPatrol.Item1.y;
            }
            else
            {
                pointToGo.x = enemy.minMaxRangeToPatrol.Item2.x;
                pointToGo.z = enemy.minMaxRangeToPatrol.Item2.y;
            }
        }
        //pointToGo.y = enemy.transform.position.y;
        pointToGo.y = enemy.terrainData.SampleHeight(pointToGo);

        /*        Debug.Log($"going to {pointToGo.x},{pointToGo.z}");
        */
        enemy.DrawSphere(pointToGo);


        enemy.agent.SetDestination(pointToGo);
        enemy.whereGoing = pointToGo;
        enemy.DrawSphere(pointToGo);
        if (enemy.player)
        {
            enemy.transform.LookAt(pointToGo);

        }
        else
        {
            Debug.Log("Npc Not Found");
        }
    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {


        if (enemy.distanceFromPlayer < enemy.attackingDistance && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.AttackingState);

        }
        else if (enemy.distanceFromPlayer < 50 && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.ChasingState);

        }
        else if (Vector3.Distance(pointToGo, enemy.transform.position) <= 5 && enemy.changeStatePermit)
        {
            enemy.SwitchSate(enemy.IdleState);
        }
        /*        Debug.Log(Vector3.Distance(pointToGo, enemy.transform.position));
        */
    }
}
[System.Serializable]
public class AttackingState : NpcState
{
    int skillIndex;
    public override void EnterState(Npc enemy)
    {
        enemy.agent.speed = enemy.defaultSpeed;
        enemy.transform.LookAt(enemy.player.transform);
        enemy.agent.isStopped = true;
        float rand = UnityEngine.Random.Range(0, 10);
        if (rand <= 4)
        {
            enemy.animator.SetTrigger(enemy.stateType);
            enemy.animator.SetTrigger("Attack 1");
            skillIndex = 0;
        }
        else if (rand <= 7.5)
        {

            enemy.animator.SetTrigger(enemy.stateType);
            enemy.animator.SetTrigger("Attack 2");
            skillIndex = 1;
        }
        else if (rand <= 10)
        {

            enemy.animator.SetTrigger(enemy.stateType);
            enemy.animator.SetTrigger("Attack 3");
            skillIndex = 3;
        }
        enemy.StartTime();

    }

    public override void TriggerState(Npc enemy, Collider collider)
    {
        throw new NotImplementedException();
    }

    public override void UpdateState(Npc enemy)
    {

        if (enemy.changeStatePermit)
        {
            enemy.SwitchSate(enemy.IdleState);
        }
        else if (enemy.distanceFromPlayer > enemy.attackingDistance && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.ChasingState);
        }

    }
}





[System.Serializable]
public class ChasingState : NpcState
{
    public override void EnterState(Npc enemy)
    {
        enemy.agent.speed = enemy.runningSpeed;

        enemy.transform.LookAt(enemy.player.transform);
        if (enemy.agent.isOnNavMesh)
        {
            enemy.agent.isStopped = false;
        }
        enemy.animator.SetTrigger(enemy.stateType);

        enemy.animator.SetTrigger("Chasing 1");
        enemy.StartTime();

    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {

        enemy.agent.SetDestination(enemy.player.transform.position);
        enemy.transform.LookAt(enemy.player.transform);
        if (enemy.distanceFromPlayer < enemy.attackingDistance && enemy.attackCooldown <= enemy.time && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.AttackingState);
        }
        if (enemy.distanceFromPlayer < enemy.attackingDistance && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.AttackingState);
        }
        else if (enemy.distanceFromPlayer > enemy.chasingLimitMax && enemy.changeStatePermit)
        {
            enemy.SwitchSate(enemy.IdleState);

        }

    }
}
[System.Serializable]
public class GetHitState : NpcState
{
    public override void EnterState(Npc enemy)
    {

        enemy.transform.LookAt(enemy.player.transform);

        try
        {
            enemy.agent.isStopped = true;
        }
        catch (Exception)
        {

        }

        enemy.animator.SetTrigger(enemy.stateType);
        enemy.animator.SetTrigger("Get Hit 1");
    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {
        if (enemy.changeStatePermit)
        {
            enemy.SwitchSate(enemy.IdleState);
        }
    }
}
[System.Serializable]
public class DeathState : NpcState
{
    public override void EnterState(Npc enemy)
    {
        enemy.transform.LookAt(enemy.player.transform);

        enemy.agent.isStopped = true;
        enemy.animator.SetTrigger(enemy.stateType);

        enemy.animator.SetTrigger("Death 1");
        enemy.agent = null;
        enemy.Death();
    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {

    }
}

[System.Serializable]
public class GoingToItsPositionState : NpcState
{


    public override void EnterState(Npc enemy)
    {
        if (!enemy.agent.isOnNavMesh)
        {
            return;
        }
        enemy.agent.speed = enemy.defaultSpeed;

        enemy.agent.isStopped = false;

        enemy.animator.SetTrigger(enemy.stateType);
        enemy.animator.SetTrigger("Patrol 1");



        enemy.transform.LookAt(enemy.orignalPosition);

        enemy.agent.SetDestination(enemy.orignalPosition);
    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {
        if (enemy.distanceFromPlayer < enemy.attackingDistance && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.AttackingState);

        }
        else if (enemy.distanceFromPlayer < 50 && enemy.changeStatePermit && !enemy.isSpyMode)
        {
            enemy.SwitchSate(enemy.ChasingState);
        }
        else if (enemy.distanceFromOrignalPos <= 5f)
        {
            enemy.SwitchSate(enemy.IdleState);
        }
    }
}




[System.Serializable]
public class UnconciousState : NpcState
{
    public override void EnterState(Npc enemy)
    {
        enemy.agent.speed = 0;



        // Unconcious Animation will be played
    }

    public override void TriggerState(Npc enemy, Collider collider)
    {

    }

    public override void UpdateState(Npc enemy)
    {

    }
}
