using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    private bool isShowing;
    private Animator anim;
    bool isSitting;
    public TextMeshProUGUI text;
    public GameObject camera;
    public GameObject player;
    public float distance;
    float time;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) < distance && !isShowing && !isSitting && time>1)
        {
            isShowing = true;
            text.gameObject.SetActive(isShowing);
            text.text = "Press ''E'' to Sit";
            time = 0;

        }
        else if (Vector3.Distance(transform.position, player.transform.position) > distance && isShowing &&time > 1)
        {
            isShowing = false;
            text.gameObject.SetActive(isShowing);
            time = 0;


        }
        if (isShowing && Input.GetKeyDown(KeyCode.E) && !isSitting && time > 1)
        {
            camera.gameObject.SetActive(true);
            isSitting = true;
            text.text = "Press ''E'' to Stand";
            player.SetActive(false);
            time = 0;


        }
        if (isSitting && Input.GetKeyDown(KeyCode.E) &time > 1)
        {
            isSitting = false;
            time = 0;
            text.text = "Press ''E'' to Sit";
            player.SetActive(true);
            camera.SetActive(false);

        }
    }
}
