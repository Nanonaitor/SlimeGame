using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialogue;
    DialogueManager manager;

    void Start()
    {
        manager = FindObjectOfType<DialogueManager>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CheckLayer("Player"))
        {
            manager.ShowDialoguePanel(true);
            manager.SetDialogueText(dialogue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CheckLayer("Player"))
        {
            manager.ShowDialoguePanel(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
