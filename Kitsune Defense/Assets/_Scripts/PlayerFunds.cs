using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFunds : MonoBehaviour
{

    public float playerFunds;
    public Text text;
    public int redOrbGain = 5;
    public int yellowOrbGain = 15;
    public int greenOrbGain = 50;

    private TrapPlacer bank;


    private void Start()
    {
        bank = GameObject.FindGameObjectWithTag("TrapPlacer").GetComponent<TrapPlacer>();
        playerFunds = bank.funds;
        text.text = playerFunds.ToString();
    }


    public void AtualizarHud()
    {
        playerFunds = bank.funds;
        text.text = playerFunds.ToString();
    }

    /* void OnCollisionEnter(Collision other)
    {
        Debug.Log("colidiu");
        if (other.gameObject.CompareTag("Orb"))
        {
            Debug.Log("colidiu mas ñ destruiu");
            bank.AddFunds(5);
            // atualiza o hud 
            //playerFunds = bank.funds;
            Destroy(other.gameObject);
        }
    }*/



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Red Orb"))
        {
            bank.AddFunds(redOrbGain);
            // atualiza o hud 
            AtualizarHud();

            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("Yellow Orb"))
        {
            bank.AddFunds(yellowOrbGain);
            // atualiza o hud 
            AtualizarHud();

            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("Green Orb"))
        {
            bank.AddFunds(greenOrbGain);
            // atualiza o hud 
            AtualizarHud();

            Destroy(hit.gameObject);
        }
    }
    
    

}
