using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    // tranformar num array
    //public Dialogue[] dialogue;
    public DialogueManager dialogueManager;
    public SpawnControl spawnControl;
    public int[] triggerWave;
    public string[] dialogueToCall;
    public int numDialog;
    public bool stopPlayer;
    public int dialogueIndex;
    public bool waveDialogueReaded;

    public void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
        spawnControl = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnControl>();
    }

    private void Update()
    {
        //numDialog = Mathf.Clamp(numDialog, 0, dialogue.Length - 1);
        for (int i = 0; i <= triggerWave.Length-1; i++)
        {
            if (spawnControl.CurrentWave == triggerWave[i] && dialogueManager.inDialogue == false)
            {
                if (waveDialogueReaded == false && dialogueManager.inDialogue == false) {
                    waveDialogueReaded = true;
                    if (numDialog < triggerWave.Length)
                    {
                        TriggerDialogue();
                        numDialog++;
                    }
                }
            }
            else if(spawnControl.CurrentWave != triggerWave[i])
            {
                waveDialogueReaded = false;
            }
        }
        
    }


    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogueToCall[numDialog]);
    }


    public void StopPlayer()
    {
        FindObjectOfType<DialogueManager>().stopPlayer = true;
    }

    /*private void OnTriggerEnter(Collider other)
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
    }*/
}
