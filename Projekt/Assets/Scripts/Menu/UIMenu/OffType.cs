using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffType : MonoBehaviour {

    [SerializeField]
    Menu menu;
	public void OType()
    {
        menu.OffType = gameObject.name;
        PlayerPrefs.SetString("OffType", gameObject.name);
        PlayerPrefs.Save();
        menu.UpdateOffTree();
    }
}
