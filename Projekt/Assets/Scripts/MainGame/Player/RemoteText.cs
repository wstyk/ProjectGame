using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteText : MonoBehaviour {

    public GameObject Player;
    void LateUpdate()
    {
        if (Player == null) Destroy(gameObject);
        else
        {
            gameObject.transform.position = Player.transform.position + new Vector3(0, 18, 0);
            gameObject.transform.LookAt(Camera.main.gameObject.transform.position);
        }
    }
}
