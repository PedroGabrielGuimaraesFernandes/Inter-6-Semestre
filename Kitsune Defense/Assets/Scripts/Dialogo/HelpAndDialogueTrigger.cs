using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpAndDialogueTrigger : MonoBehaviour {

    //public GameObject objToDisplay;
    public SpriteRenderer objToDisplay;

    // Use this for initialization
    void Start () {
        //objToDisplay.SetActive(false);
        objToDisplay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //objToDisplay.SetActive(true);
            objToDisplay.enabled = true;
        }
    }

   /* private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            objToDisplay.SetActive(false);
        }
    }*/
}
