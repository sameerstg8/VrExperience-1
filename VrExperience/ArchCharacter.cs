using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArchCharacter : MonoBehaviour
{
    bool isContacted;
    GameObject player;
    public float distance;
    public GameObject goToPosition;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Vector3.Distance(player.transform.position,transform.position)<distance && !isContacted)
        {
            isContacted = true;
            GoTo();
        }
        else if(isContacted)
        {
            transform.LookAt(goToPosition.transform);
        }
    }
    void GoTo()
    {
        GetComponent<NavMeshAgent>().SetDestination(goToPosition.transform.position);
    }
}
