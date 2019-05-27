using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;

    public bool inDialogue = false;

    //public Animator anim;
    public int talkinPerson;
    public int[] persont;
	public GameObject person1;
    public GameObject person2;
    public GameObject gameCanvas;
    //public Button proxButton;
    public bool stopPlayer;
    public bool contandoHistoria = false;

    public Dialogue[] dialogue;

    //public bool inMinigame;

    // Use this for initialization
    void Start () {
		sentences = new Queue<string>();
        if (contandoHistoria == false)
        {
           
        }
        //anim = GetComponent<Animator>();
	}
	
	public void StartDialogue(string dialogueName)
    {
        //FindObjectOfType<AudioManager>().Play("Beep Comunicacao");
        inDialogue = true;
        talkinPerson = 0;

        foreach (Dialogue dialog in dialogue)
        {

            if (dialogueName == dialog.name) {

                sentences.Clear();


                for (int i = 0; i < dialog.personTalkin.Length; i++)
                {
                    persont[i] = dialog.personTalkin[i];
                }

               

                foreach (string sentence in dialog.sentences)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentence();
            }
        }
    }

    public void DisplayNextSentence()
    {

        if (persont[talkinPerson] == 0)
        {
            person1.SetActive(true);
            person2.SetActive(false);
            talkinPerson++;
        }
        else
        {
            person1.SetActive(false);
            person2.SetActive(true);
            talkinPerson++;
        }
        if (sentences.Count == 0)
        {
           // anim.SetBool("Open", false);
            EndDialogue();
            return;
        }
        gameCanvas.SetActive(true);

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        //proxButton.interactable = false;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
           // sfx.StopSound();
            yield return null;
        }
        StartCoroutine(PassSentence());
        // proxButton.interactable = true;
    }

    public void EndDialogue()
    {
        if (stopPlayer)
        {
            stopPlayer = false;
        }

        Debug.Log("End of Conversation ");
		//nextButton.SetActive (false);
		gameCanvas.SetActive (false);
        
        talkinPerson = 0;

        inDialogue = false;
        //FindObjectOfType<AudioManager>().Stop("Beep Comunicacao");
    }

    IEnumerator PassSentence()
    {
        print(1);
        yield return new WaitForSeconds(5f);
        print(2);
        DisplayNextSentence();
    }
}
