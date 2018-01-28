using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailedToScript : MonoBehaviour {

    [SerializeField]
    Text text;
    float t;
	void Start()
    {
        t = 1;
    }

    void Update () {
        t -= 0.01f;
        text.color = new Color(0, 0, 0, t);
        if (t <= 0) Destroy(gameObject);
	}
}
