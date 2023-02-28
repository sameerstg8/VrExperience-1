using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterSoilOpeningScript : MonoBehaviour
{
    public GameObject SoiloPeningTrigger,AnimatorObject;
    Animator Animator;
    void Start()
    {
        Animator = AnimatorObject.GetComponent<Animator>();
        Animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Animator.enabled = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
