using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailedToScript : MonoBehaviour {

    float t;
	void Start()
    {
        t = 1;
    }

    void Update () {
        t -= 0.01f;
        gameObject.GetComponent<Text>().color = new Color(1, 0.2f, 0, t);
        if (t <= 0) Destroy(gameObject);
	}
}
