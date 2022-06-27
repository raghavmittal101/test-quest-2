using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateController : MonoBehaviour
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
        bool isJumping = animator.GetBool("IsJumping");
        bool upPressed = Input.GetKeyDown(KeyCode.UpArrow);
        if(!isJumping && upPressed)
        {
            animator.SetBool("IsJumping",true);
        }
        if(isJumping && !upPressed)
        {
            // animator.SetBool("IsJumping",false);
            StartCoroutine(waitCoroutine()); // see and learn this ******
        }
    }

    IEnumerator waitCoroutine() // see and learn this ******
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsJumping",false);
    }
}
