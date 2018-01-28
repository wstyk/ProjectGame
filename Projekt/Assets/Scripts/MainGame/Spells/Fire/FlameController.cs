using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlameController : NetworkBehaviour {

    [SerializeField]
    float dmg, destroyTime;

    void Start()
    {
        dmg = 1;
    }

    void OnTriggerStay(Collider hit)
    {
        if(hit.tag == "Player")
        {
            hit.GetComponent<PlayerHP>().TakeDamage(dmg);
        }
    }
}
