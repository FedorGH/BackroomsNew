using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("Walk", z);
        animator.SetFloat("Rotate", x);


        if (PlayerCont._trigger)
        {
            animator.SetFloat("Walk", 2f);
        }
        else
        {
            return;
        }

    }
}
