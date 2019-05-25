using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    // tranformar num array
    //public Dialogue[] dialogue;
    public string dialogueToCall;
    public int numDialog;
    public bool stopPlayer;
    public int dialogueIndex;
    public bool colDeactive;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueToCall);
    }


    public void StopPlayer()
    {
        FindObjectOfType<DialogueManager>().stopPlayer = true;
    }

    public void Start()
    {
        
    }

    private void Update()
    {
        //numDialog = Mathf.Clamp(numDialog, 0, dialogue.Length - 1);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (colDeactive == false) {
            if (stopPlayer == true)
            {
                StopPlayer();
            }
            TriggerDialogue();

            //SaveManager.dialogoStatus[dialogueIndex - 1] = 1;
            //numDialog++;
            GetComponent<Collider>().enabled = false;
        }
    }
}
