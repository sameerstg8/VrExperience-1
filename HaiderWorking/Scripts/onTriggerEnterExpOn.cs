using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerEnterExpOn : MonoBehaviour
{
    public GameObject ForceFeildFX, EXPCanvas, PortalGateRenederer;
    Animator animator;
    private void Awake()
    {

        animator = ForceFeildFX.GetComponent<Animator>();
    }
    void Start()
    {
        ForceFeildFX.SetActive(false);
    }

    // Update is called once per fra
    // me
    public void CloseFX()
    {
        animator.SetBool("Close",true);
    }
    private void OnTriggerEnter(Collider other)
    {
        ForceFeildFX.SetActive(true);
        EXPCanvas.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
       Destroy(PortalGateRenederer);
    }
    void Update()
    {
       
    }
}
