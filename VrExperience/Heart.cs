using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public TextMeshProUGUI text;
    private bool isShowing;
    private Animator anim;
    public float distance;
    GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (Vector3.Distance(transform.position,player.transform.position)< distance && !isShowing)
        {
            isShowing = true;
            text.text = "Press ''E'' to Squeeze";
            text.gameObject.SetActive(isShowing);
        }
        else if(Vector3.Distance(transform.position, player.transform.position) > distance && isShowing)
        {
            isShowing = false;
            text.gameObject.SetActive(isShowing);

        }
        if (isShowing && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Squeeze");
        }
    }
}
