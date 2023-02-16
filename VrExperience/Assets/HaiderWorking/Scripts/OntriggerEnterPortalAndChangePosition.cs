using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerEnterPortalAndChangePosition : MonoBehaviour
{
    public GameObject Player,Position,OpeningScene,CustomFadingAnimator,Size;
    
    
    void Start()
    {
        CustomFadingAnimator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.transform.position = Position.transform.position;
        Player.transform.localScale = Size.transform.localScale;
        CustomFadingAnimator.SetActive(true);
        Destroy(OpeningScene);
       
    }
    
    void Update()
    {
        
    }
}
