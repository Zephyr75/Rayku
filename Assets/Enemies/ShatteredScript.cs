using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredScript : MonoBehaviour {
    bool isDestroyed=false;
    public GameObject belly, wheel1, wheel2, wheel3, wheel4, arm1, arm2, special1, special2, hinge, head, leg1, leg2, foot2, torso;
    public GameObject collapseSfx;

    void Start () {
        if (gameObject.name != "EnemyPShattered" && gameObject.name != "EnemyTShattered")
        {
            collapseSfx.SetActive(true);
            belly.SetActive(true);
            wheel1.SetActive(true);
            wheel2.SetActive(true);
            wheel3.SetActive(true);
            wheel4.SetActive(true);
            arm1.SetActive(true);
            arm2.SetActive(true);
            hinge.SetActive(true);
            head.SetActive(true);
            leg1.SetActive(true);
            leg2.SetActive(true);
            foot2.SetActive(true);
            torso.SetActive(true);
            special1.SetActive(true);
            special2.SetActive(true);
            StartCoroutine(Destroy());

        }
    }
	
	void FixedUpdate () {
		if (isDestroyed==true)
        {
            Destroy(gameObject);
        }
	}


    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        isDestroyed = true;

    }
}
