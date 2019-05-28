using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
    public Transform reference;
    //public ParticleSystem[] shootPosition;
    public ParticleSystem fireAlpha;
    public ParticleSystem fireAdd;
    public ParticleSystem glow;
    public ParticleSystem sparks;
    public float maxNumShoot = 12;

    private int attackIndex;

    // Start is called before the first frame update
    void Start()
    { 
        anim = GetComponent<Animator>();
        //attackIndex = Animator.StringToHash("attack");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void Shoot()
    {
        for (int i = 0; i < maxNumShoot; i++)
        {
            int r = Random.Range(0, 12);
            shootPosition.Emit(1);
            Debug.Log("Tentou atirar");
        }
        ReloadTrap(reloadTime);
    }*/

    public void ReloadTrap(float tempo)
    {
        StartCoroutine(ReloadTimer(tempo));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + reference.transform.up * 6);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && reloading == false)
        {
            Debug.Log("viu inimigo");
            reloading = true;
            //Debug.Log("Kunai Trap Activated");
            StartCoroutine(ShootFire());
            //Shoot();
            //anim.SetTrigger(attackIndex);
        }
        /*IABase enemyScript = other.transform.GetComponent<IABase>();
        CauseDamage(enemyScript);*/
    }

    /*public void ShootFire()
    {
        if (other.CompareTag("Enemy") && reloading == false)
        {
            reloading = true;

            Debug.Log("Kunai Trap Activated");
            StartCoroutine(ShootShuriken());
            //Shoot();
            //anim.SetTrigger(attackIndex);
        }
        /*shootPosition.emission.enabled = true;
        ReloadTrap(reloadTime);*
    }*/

    public IEnumerator ShootFire()
    {
        int r = 0;
        //vai se repetir a te dar o tempo do reload

        /*for (int i = 0; i < shootPosition.Length ; i++)
        {*/
        //shootPosition[r].Play();
        Debug.Log("fire Trap Activated");
        fireAlpha.Play();
        fireAdd.Play();
        glow.Play();
        sparks.Play();
        //r++;
        //}
       
        yield return new WaitForSeconds(5f);
        /*for (int i = 0; i < shootPosition.Length; i++)
        {*/
        //shootPosition[r].Play();
        Debug.Log("fire Trap Deactivated");
        fireAlpha.Stop();
            fireAdd.Stop();
            glow.Stop();
            sparks.Stop();  
            r++;
        //}
        ReloadTrap(reloadTime);
    }


    public IEnumerator ReloadTimer(float time)
    {
        float r = 0;
        //vai se repetir a te dar o tempo do reload
        while (r < reloadTime)
        {
            r++;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Relodou");
        reloading = false;
    }
}
