using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public Transform bullet;
    public NavMeshAgent agent;
    float rotationSpeed = 500, speed = 10;
    public Transform wheel1, wheel2, wheel3, wheel4, wheel5, wheel6, wheel7, wheel8;
    public Transform cameraEnemy;
    public GameObject body1, sphere1, body2, sphere2, body3, sphere3, tail;
    public Collider body1C, body2C, body3C, tailC;
    public GameObject head01, head03, head05, body11, body13, body14, body21, body22, body23, body31, body32, tail41;
    public RaycastHit hit;
    public LayerMask surfaces;
    public int maxDistance;
    int attackType;
    float timeS, timeE;
    float health = 300, health2 = 300, damage = 0, damage2 = 0;
    public bool isAttacking;
    bool canShoot = true, isAttacked, isRunningAway = false, underAttack = false;
    public Vector3 offset;
    public GameObject hitSfx, shootSfx, attackSfx;
    public GameObject impact, impact2;
    public GameObject attack;
    public GameObject winScreen;
    public GameObject smoke1, smoke2, smoke3,  smoke4, smoke5;
	
	// Update is called once per frame
	void Update ()
    {
        if (underAttack == true)
        {
            if (timeE - timeS < .6 && timeE - timeS > .1)
            {
                if (Input.GetMouseButtonDown(0) && attackType == 2)
                {
                    damage2 += 2;
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
        damage = GameObject.Find("Body1").GetComponent<BodyScript>().damage + GameObject.Find("Body2").GetComponent<BodyScript>().damage + GameObject.Find("Body3").GetComponent<BodyScript>().damage + GameObject.Find("Tail").GetComponent<BodyScript>().damage + damage2;
        health = 300 - (20 * damage);
        if (health2 != health)
        {
            if (health == 240)
            {
                body21.SetActive(false);
                body31.SetActive(false);
                tail41.SetActive(false);

                body22.SetActive(true);
                body32.SetActive(true);
                tailC.enabled = false;
                Destroy(sphere3);
                smoke5.SetActive(true);
            }
            if (health == 180)
            {
                head01.SetActive(false);
                body11.SetActive(false);
                body22.SetActive(false);
                body32.SetActive(false);

                head03.SetActive(true);
                body13.SetActive(true);
                body23.SetActive(true);
                body3C.enabled = false;
                Destroy(sphere2);
                smoke4.SetActive(true);
            }
            if (health == 120)
            {
                body13.SetActive(false);
                body23.SetActive(false);

                body14.SetActive(true);
                body2C.enabled = false;
                Destroy(sphere1);
                smoke3.SetActive(true);
            }
            if (health == 60)
            {
                head03.SetActive(false);
                body14.SetActive(false);

                head05.SetActive(true);
                body1C.enabled = false;
                smoke2.SetActive(true);
            }
            if (health == 0)
            {
                smoke1.SetActive(true);
                Time.timeScale = 0f;
                winScreen.SetActive(true);
            }
        }
        health2 = health;


        wheel1.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel2.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel3.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel4.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel5.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel6.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel7.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        wheel8.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);

        agent.SetDestination(player.position);
        if (Physics.Raycast(cameraEnemy.transform.position, cameraEnemy.transform.forward, out hit, maxDistance, surfaces) && canShoot == true)
        {
            Instantiate(bullet, enemy.position + offset, enemy.rotation);
            StartCoroutine(WaitForRecharge());
        }

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
                damage2 += 1;
            }
            if (attackType == 2)
            {
                damage2 += 2;
            }
            timeS = Time.time;
            timeE = Time.time;
            underAttack = true;
            StartCoroutine(RunAway());

        }
    }

    IEnumerator WaitForRecharge()
    {
        shootSfx.SetActive(true);
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
        shootSfx.SetActive(false);
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
