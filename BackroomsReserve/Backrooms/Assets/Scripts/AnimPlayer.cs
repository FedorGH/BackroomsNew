using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    Animator animator;
    private float slowMouseX;
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

        float mouseX = Input.GetAxis("Mouse X");
        slowMouseX = Mathf.Lerp(slowMouseX, mouseX, 10 * Time.deltaTime);
        animator.SetFloat("MouseX", slowMouseX);

        if (PlayerCont._trigger)
        {
            animator.SetFloat("Walk", 6f);
        }
        else
        {
            return;
        }
    }
}
