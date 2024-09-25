using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public Transform enemyT;
    public float speed;
    public bool isAttacking = true;

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
