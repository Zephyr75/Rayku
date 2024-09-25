using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour {
    public GameObject image;
    public void Activate()
    {
        image.SetActive(true);
    }
    public void Deactivate()
    {
        image.SetActive(false);
    }
}
