using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob1 : MonoBehaviour
{
  
    public Transform Target;

    //nav
    private UnityEngine.AI.NavMeshAgent agent;

    //distance en ennemi et joueur
    private float Distance;

    //distance de son point d'origine
    private float DistanceBase;

    //distance de chasse
    public float chaseRange = 15;

    //portée d'attaque
    public float attackRange = 2;

    //Cooldown d'attaque
    public float attackRepeatTime = 1;
    private float attackTime;

    //degat
    public float TheDammage;
    public float enemyHealth = 100;
    public bool isDead = false;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
       
        attackTime = Time.time;


    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //rechercher le joueur en permance
            Target = GameObject.Find("coeur").transform;


            //calculer la distance entre ennemi et joueur
            Distance = Vector3.Distance(Target.position, transform.position);





            //chase
            if (Distance < chaseRange && Distance > attackRange)
            {
                Chase();
            }

            //idle
            if (Distance < attackRange)
            {
                Attack();
            }

       }
    }

    
    void Chase()
    {
        
        agent.destination = Target.position;
    }

    void Attack()
    {
        //empeche de traverser le joueur
        agent.destination = transform.position;

        //si pas de cooldown
        if (Time.time > attackTime)
        {
           
            //Target.GetComponent<PlayerStats>().ApplyDamage(TheDammage);
            Debug.Log("L'ennemi a envoyé " + TheDammage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
    }

   public void ApplyDamage(float TheDamage)
    {
        if (!isDead)
        {
            enemyHealth = enemyHealth - TheDamage;
            print(gameObject.name + "a subit " + TheDamage + " points de dégâts.");

            if (enemyHealth <= 0)
            {
                Dead();
            }
        }
    }

    void Dead()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        isDead = true;
       	//Instantiate(Die, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

