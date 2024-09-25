using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logoMovement : MonoBehaviour {
    
	
	void Update () {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}
