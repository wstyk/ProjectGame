using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffElement : MonoBehaviour {

    [SerializeField]
    Menu menu;
    public void DElement()
    {
        menu.DeffElement = gameObject.name;
        PlayerPrefs.SetString("DeffElement", gameObject.name);
        PlayerPrefs.Save();
        menu.UpdateDeffTree();
    }
}
