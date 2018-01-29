using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
#if UNITY_EDITOR
    [String("Player speed:")]
#endif
    [SerializeField]
    float ForwardSpeed;
    [SerializeField]
    float SideSpeed;
#if UNITY_EDITOR
    [String("Camera references:")]
#endif
    [SerializeField]
    GameObject cam;
	
    void Start()
    {
        ForwardSpeed = 40;
        SideSpeed = 40;
    }

	void Update () {
        Movement();

    }

    void Movement()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
        //ruch ciała
        if(Input.GetAxis("Horizontal") != 0)
        {
            gameObject.transform.position += gameObject.transform.right * SideSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        }
        if(Input.GetAxis("Vertical") != 0)
        {
            gameObject.transform.position += gameObject.transform.forward * ForwardSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
        }
         
    }  
}
