using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaykuScript : MonoBehaviour {
    public Rigidbody player;
    public Transform cameraPlayer;
    float cameraRotationY = 0;
    float speed = 10, rotationSpeed = 500, timeS = 0f, timeE = 0f;
    bool canMove = true, isSliding = false, isGrappling = false, hasHitSomething = false, isRunningAway = false, canCombo = false;
    bool isAzerty = true, isRecovering = false, isAttacked = false;
    public bool isAttacking = false;
    public Animator animator;
    public float inputX, inputY, other;
    float health=100;
    float oldHealth;
    public int attackType = 1;

    public GameObject heart1, heart2, heart3, heart4, heart5, spacebar;
    public GameObject runSfx, pantingSfx, slideSfx, grapplingSfx, gruntSfx, vignette, loseScreen;
    public AudioSource music;
    int musicNbr;

    public RaycastHit hit;
    public LayerMask surfaces;
    public int maxDistance;
    public Vector3 HitPoint;
    public Transform hook;
    public LineRenderer LR;

    private void Start()
    {
        Time.timeScale = 1f;
        musicNbr = Random.Range(1, 20);
        SetMusic();
        music.Play();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        isAzerty = GameObject.Find("SettingsMenu").GetComponent<SettingsMenu>().isAzerty;
        if (isGrappling == true)
        {
            Grapplin();
        }

        inputY = Input.GetAxis("Vertical");
        animator.SetFloat("InputY", inputY);
        inputX = Input.GetAxis("Horizontal");
        animator.SetFloat("InputX", inputX);
        animator.SetFloat("Other", other);

        if (inputX == 0 && inputY == 0)
        {
            runSfx.SetActive(false);
        }

        if (canMove==true)
        {
        GoForward();
        GoBackward();
        GoRight();
        GoLeft();
        }
        
        Attack();
        Slide();
        Throw();
        Heart();
        
    }

    void Rotate()
    {
        if (Mathf.Abs(cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) > 1)
        {
            if (((cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) >= 0 && (cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) <= 180) || ((cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) <= -180 && (cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) >= -360))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            if ((cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) <= 0 && (cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) >= -180 || (cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) <= -360 || (cameraPlayer.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y) >= 180)
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }

    void RotateFixe()
    {
        if (Mathf.Abs(cameraRotationY - player.transform.rotation.eulerAngles.y) > 1)
        {
            if (((cameraRotationY - player.transform.rotation.eulerAngles.y) >= 0 && (cameraRotationY - player.transform.rotation.eulerAngles.y) <= 180) || ((cameraRotationY - player.transform.rotation.eulerAngles.y) <= -180 && (cameraRotationY - player.transform.rotation.eulerAngles.y) >= -360))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            if ((cameraRotationY - player.transform.rotation.eulerAngles.y) <= 0 && (cameraRotationY - player.transform.rotation.eulerAngles.y) >= -180 || (cameraRotationY - player.transform.rotation.eulerAngles.y) <= -360 || (cameraRotationY - player.transform.rotation.eulerAngles.y) >= 180)
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }

    void GoForward()
    {
        if ((isAzerty ==true && Input.GetKey("z")) || (isAzerty == false && Input.GetKey("w")))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            Rotate();
            runSfx.SetActive(true);
        }
    }

    void GoBackward()
    {
        if (Input.GetKey("s"))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
            Rotate();
            runSfx.SetActive(true);
        }
    }

    void GoRight()
    {
        if (Input.GetKey("d"))
        {
            transform.position += transform.right * Time.deltaTime * speed;
            Rotate();
            runSfx.SetActive(true);
        }
    }

    void GoLeft()
    {
        if ((isAzerty == true && Input.GetKey("q")) || (isAzerty == false && Input.GetKey("a")))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
            Rotate();
            runSfx.SetActive(true);
        }
    }

    public void Throw()
    {
        if (Input.GetKeyDown("e") && isGrappling == false && Physics.Raycast(cameraPlayer.transform.position, cameraPlayer.transform.forward, out hit, maxDistance, surfaces))
        {
            spacebar.SetActive(true);
            grapplingSfx.SetActive(true);
            cameraRotationY = cameraPlayer.transform.rotation.eulerAngles.y;
            other = 1;
            isGrappling = true;
            canMove = false;
            HitPoint = hit.point;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            LR.enabled = true;
        }

    }

    public void Grapplin()
    {

        RotateFixe();
        LR.SetPosition(1, HitPoint);
        LR.SetPosition(0, hook.position);

        if (hasHitSomething == true || Input.GetKeyDown("space"))
        {
            spacebar.SetActive(false);
            grapplingSfx.SetActive(false);
            other = 0;
            canMove = true;
            isGrappling = false;
            LR.enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * speed * 2;
        }
    }
    

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            isAttacking = true;
            timeS = Time.time;
            timeE = Time.time;
            attackType = 1;
            StartCoroutine(Combo());
        }
        if (isAttacking == true && timeE - timeS < .6 && timeE - timeS >.1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canCombo = true;
            }
        }
        if (isAttacking == true && timeE - timeS < .6)
        {
            timeE = Time.time;
        }
    }

    IEnumerator Combo()
    {
        speed /= 2;
        animator.Play("Attack", 0, 0);
        yield return new WaitForSeconds(.6f);
        if (canCombo == true)
        {
            animator.Play("Combo", 0, 0);
            attackType = 2;
            yield return new WaitForSeconds(1.2f);
            canCombo = false;
        }
        animator.Play("Movement", 0, 0);
        attackType = 1;
        speed *= 2;
        isAttacking = false;
        StopCoroutine(Combo());
    }

    void Slide()
    {
        if (Input.GetKeyDown("left shift") && isSliding == false)
        {
            slideSfx.SetActive(true);
            canMove = false;
            isSliding = true;
            timeS = Time.time;
            timeE = Time.time;
            animator.Play("Slide", 0, 0);
        }
        if (isSliding == true && timeE - timeS < 1.3)
        {
            timeE = Time.time;
        }
        if (isSliding == true && timeE - timeS > 1.1 && timeE - timeS < 1.3)
        {
            transform.position += transform.forward;
        }
        if (isSliding == true && timeE - timeS > 1.3)
        {
            canMove = true;
            isSliding = false;
            slideSfx.SetActive(false);
            animator.Play("Movement", 0, 0);
        }
    }

    void Die()
    {
        if (health==0)
        {
            canMove = false;
            StartCoroutine(Death());
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Floor")
        {
            hasHitSomething = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name != "Floor")
        {
            hasHitSomething = false;
        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name != "Floor")
        {
            if (col.gameObject.tag == "EnemyP" && isRunningAway == false)
            {
                isAttacked = col.gameObject.GetComponent<EnemyPScript>().isAttacking;
                if(isAttacked == true)
                {
                    health -= 20;
                    Die();
                    StartCoroutine(RunAway());
                    if (isRecovering == false)
                    {
                        StartCoroutine(Recover());
                    }
                }
            }
            if (col.gameObject.tag == "Bullet" && isRunningAway == false)
            {
                isAttacked = col.gameObject.GetComponent<BulletScript>().isAttacking;
                if (isAttacked == true)
                {
                    health -= 20;
                    Die();
                    StartCoroutine(RunAway());
                    if (isRecovering == false)
                    {
                        StartCoroutine(Recover());
                    }
                }
            }
            if (col.gameObject.tag == "Boss" && isRunningAway == false)
            {
                isAttacked = col.gameObject.GetComponent<BossScript>().isAttacking;
                if (isAttacked == true)
                {
                    health -= 20;
                    Die();
                    StartCoroutine(RunAway());
                    if (isRecovering == false)
                    {
                        StartCoroutine(Recover());
                    }
                }
            }
            if (col.gameObject.tag == "Body" && isRunningAway == false)
            {
                isAttacked = col.gameObject.GetComponent<BodyScript>().isAttacking;
                if (isAttacked == true)
                {
                    health -= 20;
                    Die();
                    StartCoroutine(RunAway());
                    if (isRecovering == false)
                    {
                        StartCoroutine(Recover());
                    }
                }
            }
        }
        
    }

    void Heart()
    {
        if (health>=20)
        {
            heart1.SetActive(true);
        }
        else
        {
            heart1.SetActive(false);
        }
        if (health >= 40)
        {
            heart2.SetActive(true);
            pantingSfx.SetActive(false);
            vignette.SetActive(false);
        }
        else
        {
            heart2.SetActive(false);
            pantingSfx.SetActive(true);
            vignette.SetActive(true);
        }
        if (health >= 60)
        {
            heart3.SetActive(true);
        }
        else
        {
            heart3.SetActive(false);
        }
        if (health >= 80)
        {
            heart4.SetActive(true);
        }
        else
        {
            heart4.SetActive(false);
        }
        if (health >= 100)
        {
            heart5.SetActive(true);
        }
        else
        {
            heart5.SetActive(false);
        }
    }


    IEnumerator RunAway()
    {
        gruntSfx.SetActive(true);
        isRunningAway = true;
        yield return new WaitForSeconds(1f);
        isRunningAway = false;
        gruntSfx.SetActive(false);
    }

    IEnumerator Recover()
    {
        isRecovering = true;
        oldHealth = health;
        yield return new WaitForSeconds(5f);
        if (health == oldHealth && health < 100)
        {
            health += 20;
        }
        else
        {
            isRecovering = false;
            StopCoroutine(Recover());
        }
        oldHealth = health;
        yield return new WaitForSeconds(3f);
        if (health == oldHealth && health < 100)
        {
            health += 20;
        }
        else
        {
            isRecovering = false;
            StopCoroutine(Recover());
        }
        oldHealth = health;
        yield return new WaitForSeconds(3f);
        if (health == oldHealth && health < 100)
        {
            health += 20;
        }
        else
        {
            isRecovering = false;
            StopCoroutine(Recover());
        }
        oldHealth = health;
        yield return new WaitForSeconds(3f);
        if (health == oldHealth && health < 100)
        {
            health += 20;
        }
        isRecovering = false;
        StopCoroutine(Recover());
    }

    IEnumerator Death()
    {
        animator.Play("Die", 0, 0.1f);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
    }


    void SetMusic()
    {
        if (musicNbr == 1)
        {
            music.time = 0;
        }
        if (musicNbr == 2)
        {
            music.time = 202;
        }
        if (musicNbr == 3)
        {
            music.time = 404;
        }
        if (musicNbr == 4)
        {
            music.time = 638;
        }
        if (musicNbr == 5)
        {
            music.time = 919;
        }
        if (musicNbr == 6)
        {
            music.time = 1129;
        }
        if (musicNbr == 7)
        {
            music.time = 1302;
        }
        if (musicNbr == 8)
        {
            music.time = 1522;
        }
        if (musicNbr == 9)
        {
            music.time = 1681;
        }
        if (musicNbr == 10)
        {
            music.time = 1876;
        }
        if (musicNbr == 11)
        {
            music.time = 2099;
        }
        if (musicNbr == 12)
        {
            music.time = 2351;
        }
        if (musicNbr == 13)
        {
            music.time = 2586;
        }
        if (musicNbr == 14)
        {
            music.time = 2769;
        }
        if (musicNbr == 15)
        {
            music.time = 2947;
        }
        if (musicNbr == 16)
        {
            music.time = 3201;
        }
        if (musicNbr == 17)
        {
            music.time = 3471;
        }
        if (musicNbr == 18)
        {
            music.time = 3702;
        }
        if (musicNbr == 19)
        {
            music.time = 3909;
        }
        if (musicNbr == 20)
        {
            music.time = 4172;
        }
    }
}