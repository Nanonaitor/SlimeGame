using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenJump : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Move", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
