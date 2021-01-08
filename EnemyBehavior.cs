using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour
{
    
    public int health = 100;

    private float damageTimer = 0f;
    private float turnTimer = 0f;
   
    public Animator anim;

    public float lookRadius = 10f;

    Transform target;

   


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        PlayerPrefs.SetInt("EnemyIsDead", 0);
    }

    public void TakeDamage()
    {
        if (damageTimer <= 0)
        {
            health -= 10;
            print("Enemy health is " + health);
            damageTimer = 2.0f;
            anim.SetBool("TakeHit", true);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        //Chase Player if within lookRadius
        if(distance <= lookRadius)
        {
            transform.LookAt(target);
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        //Walk Otherwise and Turn random angle after timer reaches certain amount
            anim.SetBool("Moving", true);
            turnTimer++;
            
        
        if(turnTimer == 3000)
        {
            Vector3 euler = transform.eulerAngles;
            euler.y = Random.Range(0f, 360f);
            transform.eulerAngles = euler;
            turnTimer = 0;
        }

        damageTimer -= Time.deltaTime;

        //When out of health destroy object
        if (health == 0)
        {
           anim.SetBool("NoLife", true);
            Destroy(gameObject);
            PlayerPrefs.SetInt("EnemyIsDead", 1);
        }

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
