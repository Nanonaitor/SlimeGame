using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Player player;
    public GameObject dialoguePanel;
    public Text dialogueText;

    public void ShowDialoguePanel(bool x)
    {
        dialoguePanel.SetActive(x);
    }

    public void SetDialogueText(string t)
    {
        dialogueText.text = t;
    }
}
