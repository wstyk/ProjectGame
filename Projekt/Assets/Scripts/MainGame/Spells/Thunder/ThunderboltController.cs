using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderboltController : MonoBehaviour {
    float destroyTime, speed, dmg;
    //wszystko tutaj jest do zmiany jak cos jest srednie (moja inwencja tworcza jest karygodna)
    void Start () {
        dmg = 30;
        destroyTime = 0;
        speed =250;
        
	}
	
	
	void FixedUpdate () {
        destroyTime += Time.deltaTime;
        if (destroyTime > 3) Destroy(gameObject);
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            PlayerHP enemy = hit.GetComponent<PlayerHP>();
            PlayerNet stun=hit.GetComponent<PlayerNet>(); //dodanie stuna
            enemy.TakeDamage(dmg);
            stun.DisableCC();
        }
        Destroy(gameObject);
    }
}
