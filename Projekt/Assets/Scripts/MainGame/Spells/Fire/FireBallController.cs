using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBallController : NetworkBehaviour {

    float destroyTime, ballspeed, dmg;
	void Start ()
    {
        dmg = 20;
        destroyTime = 0;
        ballspeed = 200;
	}

	void FixedUpdate ()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime > 5) Destroy(gameObject);
        gameObject.transform.position += gameObject.transform.forward * ballspeed * Time.deltaTime;
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            hit.GetComponent<PlayerHP>().TakeDamage(dmg);
        }
        if(hit.tag != "Spell") Destroy(gameObject);
    }
}
