using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPScript : MonoBehaviour {
    public Transform wheel1, wheel2, wheel3, wheel4;
    public Transform enemy;
    public Transform enemyShattered;
    public bool isAttacking;
    bool isAttacked, isRunningAway = false, underAttack = false;
    float rotationSpeed = 500;
    int attackType;
    float timeS, timeE;
    public float health;
    public GameObject impact, impact2;
    public GameObject hitSfx, attackSfx;

    public Transform arms;
    public Transform player;
    public NavMeshAgent agent;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (underAttack == true)
        {
            if (timeE - timeS < .6 && timeE - timeS > .1)
            {
                if (Input.GetMouseButtonDown(0) && attackType == 2)
                {
                    health -= 40;
                    StartCoroutine(RunAway());
                }
            }
            if (isAttacked == true && timeE - timeS < .6)
            {
                timeE = Time.time;
            }
            if (isAttacked == true && timeE - timeS > .6)
            {
                underAttack = false;
            }

        }

        attackType = GameObject.Find("Rayku").GetComponent<RaykuScript>().attackType;
        wheel1.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel2.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel3.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel4.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        agent.SetDestination(player.position);

        if (Vector3.Distance(enemy.position, player.position) < 3f)
        {
            StartCoroutine(ChargeAttack());
        }

        if (Vector3.Distance(enemy.position, player.position) > 3f)
        {
            isAttacking = false;
            attackSfx.SetActive(false);
        }

        if (isAttacking == true)
        {
            arms.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        
        if (health <= 0)
        {
            Instantiate(enemyShattered, enemy.position, enemy.rotation);
            Destroy(enemy.gameObject);
        }
        

    }
    
    void OnCollisionStay(Collision col)
    {
        isAttacked = GameObject.Find("Rayku").GetComponent<RaykuScript>().isAttacking;
        if (col.gameObject.name == "Rayku" && isAttacked == true && isRunningAway == false && underAttack == false)
        {
            if (attackType == 1)
            {
                health -= 20;
            }
            if (attackType == 2)
            {
                health -= 40;
            }
            timeS = Time.time;
            timeE = Time.time;
            underAttack = true;
            StartCoroutine(RunAway());
        }
    }

    IEnumerator RunAway()
    {
        isRunningAway = true;
        hitSfx.SetActive(true);
        yield return new WaitForSeconds(.2f);
        impact.SetActive(true);
        impact2.SetActive(true);
        yield return new WaitForSeconds(.8f);
        isRunningAway = false;
        impact.SetActive(false);
        impact2.SetActive(false);
        hitSfx.SetActive(false);

    }

    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = true;
        attackSfx.SetActive(true);

    }
}

        