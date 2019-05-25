using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDialogue : MonoBehaviour {
    //public MySceneManager managear;
    public string level;
    public int levelIndex;
    public PersonTalking holdDialogue;
    public GameObject button;
    public GameObject canvas;
    //public Movimentação player;
    Animator anim;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   

    public void StopAnimation()
    {
        anim.SetBool("entrega", false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            button.SetActive(true);
            canvas.SetActive(false);
            holdDialogue.isTalk = false;
            
        }

        if(gameObject.tag == "emmy")
        {
            anim.SetBool("entrega", true);
            
            /*Animator playerAnim = col.gameObject.GetComponent<Animator>();
            playerAnim.SetBool("pulou", false);
            playerAnim.SetBool("agaichou", false);
            playerAnim.SetBool("empurrou", false);*/
        }

        }
}
