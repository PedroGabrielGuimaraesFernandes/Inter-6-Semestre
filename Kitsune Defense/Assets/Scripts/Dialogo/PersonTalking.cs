using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonTalking : MonoBehaviour {


	public DialogueTrigger diatrigger;

	public bool isTalk;
    public bool final;

    public float numFinal = 0;

	public Image person1;
	public Image person2;

	// Use this for initialization
	void Start () {
		/*person1.color = new Color (person1.color.r, person1.color.g, person1.color.b, 1);
		person2.color = new Color (person2.color.r, person2.color.g, person2.color.b, 0.5f);
        */
		//diatrigger.TriggerDialogue ();


	}
	
	// Update is called once per frame
	void Update () {
        if(isTalk == false)
        {
            isTalk = true;
            diatrigger.TriggerDialogue();
        }

        if(final == true && numFinal == 4)
        {
            person1.color = new Color(person1.color.r, person1.color.g, person1.color.b, 0.5f);
            person2.color = new Color(person2.color.r, person2.color.g, person2.color.b, 1);
        }
	
	}

	public void ChangePerson(){
		if (person1.color.a > 0.9f) {
			person1.color=  new Color (person1.color.r, person1.color.g, person1.color.b, 0.5f);
			person2.color = new Color (person2.color.r, person2.color.g, person2.color.b, 1);
			Debug.Log("A");
		} else {
			person1.color = new Color (person1.color.r, person1.color.g, person1.color.b, 1);
			person2.color = new Color (person2.color.r, person2.color.g, person2.color.b, 0.5f);
			Debug.Log("B");
		}
        numFinal++;
	}
}
