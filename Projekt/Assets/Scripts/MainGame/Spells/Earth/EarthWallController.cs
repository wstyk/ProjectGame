using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallController : MonoBehaviour {
    float destroy, speed;

	void Start () {
        destroy = 4;
        speed = 70;
	}

	void Update () {
        destroy -= Time.deltaTime;
        if (destroy <= 0) Destroy(gameObject);
        if (destroy > 0 && gameObject.transform.position.y < 7) gameObject.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
	}
}
