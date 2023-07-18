using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerManager : MonoBehaviour
{
    private bool hasActivated = false;
    
    public Animator blocker1Animator;
    public Animator blocker2Animator;

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated)
            return;

        Character character;
        
        switch (other.tag)
        {
            case "Player":
                character = other.GetComponentInParent<Character>();
                break;
            case "Bot":
                character = other.GetComponent<Character>();
                break;
            default:
                return;
        }
        if (character.currentLap == 2)
        {
            blocker1Animator.SetTrigger("Trigger");
            blocker2Animator.SetTrigger("Trigger");
            hasActivated = true;
        }
    }
}
