using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMelee : IABase
{
    enum States { Idle, GoObjective, Battle,Chase};
    States ActualState;

    // Start is called before the first frame update
    void Start()
    {
        InicialSetup();
        ActualState = States.GoObjective;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ActualState)
        {
            case States.Idle:
                Idle();
                break;
            case States.GoObjective:
                GoObjective();
                break;
            case States.Battle:
                StartCoroutine(AttackCorroutine());
                break;
            case States.Chase:
                ChasePlayer();
                break;
        }

        if (Objective != null)
        {
            float Distance = Vector3.Distance(transform.position, PlayerObj.transform.position);

            if (Distance <= dToAttack)
            {
                ActualState = States.Battle;
                LookAtLerp(PlayerObj);
            }
            else if(Distance<=ChaseDistance)
            {
                ActualState = States.Chase;
            }
            else
            {
                ActualState = States.GoObjective;
            }
//            Debug.Log(ActualState);
        }
        else
        {
  //          Debug.Log("Idle");
            ActualState = States.Idle;
        }
        if (hp <= 0)
        {
            Death();
        }
        if (Input.GetKey(KeyCode.K))
        {
            hp = 0;
        }
        EnterInObjective();
    }
    public void DealDamage()
    {
            PlayerManagerScript.Damage(Damage);
        
    }
}
