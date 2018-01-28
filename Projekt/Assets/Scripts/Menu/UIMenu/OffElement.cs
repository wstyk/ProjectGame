using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffElement : MonoBehaviour {

    [SerializeField]
    Menu menu;
    public void OElement()
    {
        menu.OffElement = gameObject.name;
        PlayerPrefs.SetString("OffElement", gameObject.name);
        menu.UpdateOffTree();
    }
}
