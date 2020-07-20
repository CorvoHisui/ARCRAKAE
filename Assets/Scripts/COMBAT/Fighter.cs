using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 10; //vida
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f; //cuanto mas alto mas rapido te recuperas

    //Inmunity
    protected float inmuneTime = 1f;
    protected float lastInmune;

    //Push
    protected Vector3 pushDirection;

    //All figter can recieveDamage and Die
    protected virtual void recieveDamage(Damage dmg)
    {
        if(Time.time - lastInmune > inmuneTime)
        {
            lastInmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //Show Damage (REVISAR)
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 15, 0.5f);

            if(hitpoint<=0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        Debug.Log("death");
    }
}
