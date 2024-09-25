using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour {
    public Transform player;
    public Transform enemy;
    public float damage, partsGone;
    public float enemyDeadNbr;
    public bool isAttacking;
    int attackType;
    float timeS, timeE;
    bool isAttacked, isRunningAway = false, underAttack = false;
    public GameObject hitSfx, attackSfx;
    public GameObject impact, impact2;
    public GameObject attack;
    
	// Update is called once per frame
	void Update ()
    {
        if (underAttack == true)
        {
            if (timeE - timeS < .6 && timeE - timeS > .1)
            {
                if (Input.GetMouseButtonDown(0) && attackType == 2)
                {
                    damage += 2;
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
        if (Vector3.Distance(enemy.position, player.position) < 3f)
        {
            StartCoroutine(ChargeAttack());
        }

        if (Vector3.Distance(enemy.position, player.position) > 3f)
        {
            isAttacking = false;
            attackSfx.SetActive(false);
            attack.SetActive(false);
        }
    }

    void OnCollisionStay(Collision col)
    {
        isAttacked = GameObject.Find("Rayku").GetComponent<RaykuScript>().isAttacking;
        if (col.gameObject.name == "Rayku" && isAttacked == true && isRunningAway == false && underAttack == false)
        {
            if (attackType == 1)
            {
                damage += 1;
            }
            if (attackType == 2)
            {
                damage += 2;
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
        attack.SetActive(true);
    }
}
