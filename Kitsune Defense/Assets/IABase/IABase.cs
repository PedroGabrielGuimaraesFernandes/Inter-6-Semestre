﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    public float dToAttack;
    public float hp ;
    public float Damage;
    public float WaitToAttack;
    public GameObject redSpirit;
    public float ChaseDistance;
    public float MoveSpeed;

    [HideInInspector] public GameObject Objective;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public GameObject MainObjective;
    [HideInInspector] public GameObject PlayerObj;
    [HideInInspector] public PlayerHPManager PlayerManagerScript;
    [HideInInspector] public MainObjectiveManager ObjctiveManagerScript;
    [HideInInspector] public bool AtkBool;
    [HideInInspector] public WaypointSistem wayPointSistem;

    // Start is called before the first frame update
    public void InicialSetup()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        MainObjective = GameObject.FindGameObjectWithTag("Objective");

        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();
        PlayerManagerScript = PlayerObj.GetComponent<PlayerHPManager>();
        ObjctiveManagerScript = MainObjective.GetComponent<MainObjectiveManager>();
        wayPointSistem = gameObject.GetComponent<WaypointSistem>();

        NavAgent.speed = MoveSpeed;
        Objective = MainObjective;   
    }
    public void Idle()
    {
        Anim.SetBool("Moving", false);
        NavAgent.isStopped = true;
    }
    public void GoObjective()
    {
        if (Objective == PlayerObj)
        {
            NavAgent.SetDestination(PlayerObj.transform.position);
        }
        else
        {
            wayPointSistem.MoveToObjective();
        }

        Anim.SetBool("Moving", true);
        NavAgent.isStopped = false;
    }
    public void ChasePlayer()
    {
        NavAgent.SetDestination(PlayerObj.transform.position);
        Anim.SetBool("Attack", false);
        NavAgent.isStopped = false;
    }
    public IEnumerator AttackCorroutine()
    {
        if (!AtkBool)
        {
            AtkBool = true;         
            Anim.SetBool("Attack",true);
            Anim.SetBool("Moving", false);
            NavAgent.isStopped = true; 
            yield return new WaitForSeconds(WaitToAttack);
            Anim.SetBool("Attack", false);
            AtkBool = false;
        }
    }
    public void Death()
    {
        SpawnControl.KilledEnemies++;
        SpawnControl.CurrentEnemyNumber--;
        print(SpawnControl.CurrentEnemyNumber);
        //SpawnControl.AllKilledEnemies++;
        GameObject orb = Instantiate(redSpirit, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
        return;
    }
    public void EnterInObjective()
    {
        float Distance = Vector3.Distance(transform.position, MainObjective.transform.position);

        if (Distance <= 7)
        {
            SpawnControl.AllKilledEnemies++;
            SpawnControl.CurrentEnemyNumber--;
            ObjctiveManagerScript.Damage(1f);
            Destroy(gameObject);
            return;
        }      
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
    /*public void onFire(float dano)
    {
        Debug.Log("enemy burning");
        StartCoroutine(DamageOverTime(dano));
    }*/
    public void ReduceSpeed()
    {
        NavAgent.speed = 0.5f;
    }
    public void SpeedBackToNormal()
    {
        NavAgent.speed = MoveSpeed;
    }
    public void LookAtLerp(GameObject Target)
    {
        Vector3 TargetDiretion = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(TargetDiretion);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime *4);
    }

    public void CauseBurnDamage(float danoInicial,float tempo)
    {
        TakeDamage(danoInicial);
        StartCoroutine(DamageOverTime(tempo));
    }

    public IEnumerator DamageOverTime(float dano)
    {
        float value = (hp - dano);
        while (hp > value /*&& damaged == false*/)
        {
            hp--;
            //HPSlider.value = health;
            yield return new WaitForSeconds(1F);

        }
    }
}
