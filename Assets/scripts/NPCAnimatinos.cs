using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimatinos : MonoBehaviour
{
    private static readonly int StartAnimation = Animator.StringToHash("startAnimation");
    private void OnTriggerEnter(Collider other)
    {
        UpdateAnimation(other, true);
    }
    private void OnTriggerExit(Collider other)
    {
        UpdateAnimation(other, false);
    }

    void UpdateAnimation(Collider other, bool startAnimation)
    {
        if (other.CompareTag("Player"))
        {
            var animator= GetComponent<Animator>();
            animator.SetBool(StartAnimation,startAnimation);
        }
    }
}
