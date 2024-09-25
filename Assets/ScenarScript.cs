using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarScript : MonoBehaviour {
    public GameObject diapo1, diapo2, diapo3, diapo4;
    float timeS = 0, timeE = 0, timeF = 0;
    int diapoNbr = 1;
    //public GameObject diapo1, diapo2, diapo3, diapo4;
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown("space"))
        {
            timeS = Time.time;
            timeE = Time.time;
            if (diapoNbr == 1)
            {
                diapo1.SetActive(true);
                StartCoroutine(playDiapo("DiapoS", diapo1.GetComponent<Animator>()));
            }
            if (diapoNbr == 2)
            {
                StartCoroutine(playDiapo2(diapo1, diapo2));
            }
            if (diapoNbr == 3)
            {
                StartCoroutine(playDiapo2(diapo2, diapo3));
            }
            if (diapoNbr == 4)
            {
                StartCoroutine(playDiapo2(diapo3, diapo4));
            }
            if (diapoNbr == 5)
            {
                StartCoroutine(playDiapo("DiapoF", diapo4.GetComponent<Animator>()));
            }
            diapoNbr += 1;
        }

        if (Input.GetKey("space"))
        {
            timeE = Time.time;
        }

        if (timeE - timeS > .5f)
        {
            SceneManager.LoadScene("Level1");
        }

    }

    IEnumerator playDiapo(string animName, Animator anim)
    {
        anim.Play(animName, 0, 0);
        yield return new WaitForSeconds(1f);
        if (diapoNbr >= 5)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    IEnumerator playDiapo2(GameObject diapo1, GameObject diapo2)
    {
        diapo1.GetComponent<Animator>().Play("DiapoF", 0, 0);
        yield return new WaitForSeconds(1f);
        diapo1.SetActive(false);
        diapo2.SetActive(true);
        diapo2.GetComponent<Animator>().Play("DiapoS", 0, 0);
        yield return new WaitForSeconds(1f);
    }
}
