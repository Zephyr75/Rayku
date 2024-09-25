using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour {
    public Transform enemyPunch, enemyThrow;
    public bool canSpawn = true;
    Vector3 spawnLocation = new Vector3(-30, 4, -2);
    Quaternion spawnRotation = new Quaternion(0, 180, 0, 0);
    float waveNumber = 1, enemyNumber = 0;
    float timeS, timeE;
    int enemyType;
    Collider box;
    public GameObject boss, smoke1, smoke2, smoke3,  smoke4, smoke5;

    public Image circle;
    public Text nbrWave, timerWave;

    void Update () {
        timeE = Time.time;
        if (waveNumber < 9)
        {
            timerWave.text = (30 - Mathf.FloorToInt(timeE - timeS)).ToString();
            circle.fillAmount = (float)(1 - (0.0333 * Mathf.FloorToInt(timeE - timeS)));
        }
        if (waveNumber == 9)
        {
            timerWave.text = (45 - Mathf.FloorToInt(timeE - timeS)).ToString();
            circle.fillAmount = (float)(1 - (0.0222 * Mathf.FloorToInt(timeE - timeS)));
        }
        nbrWave.text = waveNumber.ToString();
        box = GameObject.Find("Factory").GetComponent<Collider>();
        if (canSpawn == true)
        {
            box.enabled = true;
            if (waveNumber == 1)
            {
                Wave1();
            }
            if (waveNumber == 2)
            {
                Wave2();
            }
            if (waveNumber == 3)
            {
                Wave3();
            }
            if (waveNumber == 4)
            {
                Wave4();
            }
            if (waveNumber == 5)
            {
                Wave5();
            }
            if (waveNumber == 6)
            {
                Wave6();
            }
            if (waveNumber == 7)
            {
                Wave7();
            }
            if (waveNumber == 8)
            {
                Wave8();
            }
            if (waveNumber == 9)
            {
                Wave9();
            }
            if (waveNumber==10)
            {
                Boss();
            }

            box.enabled = false;
        }
    }

    IEnumerator WaitForWave(float seconds)
    {
        canSpawn = false;
        yield return new WaitForSeconds(seconds);
        waveNumber += 1;
        canSpawn = true;
        timeS = Time.time;
        timeE = timeS;
    }

    IEnumerator WaitForSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(2f);
        canSpawn = true;
    }

    void Wave1()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        StartCoroutine(WaitForWave(30f));
    }
    void Wave2()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 3)
        {
            StartCoroutine(WaitForWave(28f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave3()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 6)
        {
            StartCoroutine(WaitForWave(26f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave4()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 10)
        {
            StartCoroutine(WaitForWave(24f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave5()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 15)
        {
            StartCoroutine(WaitForWave(22f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave6()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 21)
        {
            StartCoroutine(WaitForWave(20f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave7()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 28)
        {
            StartCoroutine(WaitForWave(18f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave8()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 36)
        {
            StartCoroutine(WaitForWave(16f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Wave9()
    {
        enemyType = Random.Range(1, 3);
        if (enemyType == 1)
        {
            Instantiate(enemyPunch, spawnLocation, spawnRotation);
        }
        else
        {
            Instantiate(enemyThrow, spawnLocation, spawnRotation);
        }
        enemyNumber += 1;
        if (enemyNumber == 45)
        {
            StartCoroutine(WaitForWave(29f));
        }
        else
        {
            StartCoroutine(WaitForSpawn());
        }
    }
    void Boss()
    {
        boss.SetActive(true);
    }

    IEnumerator Final()
    {
        smoke1.SetActive(true);
        smoke2.SetActive(true);
        smoke3.SetActive(true);
        smoke4.SetActive(true);
        smoke5.SetActive(true);
        yield return new WaitForSeconds(.2f);
        boss.SetActive(true);
        yield return new WaitForSeconds(3f);
        smoke1.SetActive(false);
        smoke2.SetActive(false);
        smoke3.SetActive(false);
        smoke4.SetActive(false);
        smoke5.SetActive(false);
    }
}
