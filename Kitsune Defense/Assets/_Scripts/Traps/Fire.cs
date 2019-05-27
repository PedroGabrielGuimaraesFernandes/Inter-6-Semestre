using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public ParticleSystem fireAlpha;
    public ParticleSystem fireAdd;
    public ParticleSystem glow;
    public ParticleSystem sparks;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            fireAlpha.emission.SetBurst(10,fireAlpha.emission.GetBurst(0)) ;

            Debug.Log("Particula Colidiu");
            IABase enemyScript = other.GetComponent<IABase>();
            enemyScript.CauseBurnDamage(5,5);

            return;
        }
    }
}
