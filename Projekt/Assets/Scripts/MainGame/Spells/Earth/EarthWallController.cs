using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallController : MonoBehaviour {
    [HideInInspector]
    public float destroyTime, speed;
    float TimePassed, time;
    [HideInInspector]
    public EarthSpells scritp;
    [HideInInspector]
    public GameObject player;
    public GameObject lookAt;
    bool rotateWall, moveWall, truefalse1;
    Transform StartRotation, EndRotation;
    Quaternion EndR;

	void Start () {
        destroyTime = 4;
        TimePassed = 0;
        rotateWall = false;
        moveWall = false;
        speed = 70;
        time = 0;
	}

	void FixedUpdate () {

        destroyTime -= Time.deltaTime;
        TimePassed += Time.deltaTime;
        
        if (rotateWall) 
        {
            Rotate();
            //time += 0.01f;
        }
        if (moveWall) Move();
        
        if (destroyTime <= 0) 
        {
            scritp.EarthWallCount--;
            scritp.EarthWalls.Remove(gameObject);
            Destroy(gameObject);
        }
        else if (destroyTime > 0 && gameObject.transform.position.y < 7)
        {
            gameObject.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
	}

    public void MoveWall()
    {
        if(TimePassed >= 2)
        {
            
        }
    }
    void Rotate()
    {
        
    }
    void Move()
    {
        
    }
}
