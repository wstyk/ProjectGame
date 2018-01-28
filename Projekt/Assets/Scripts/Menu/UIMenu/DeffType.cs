using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffType : MonoBehaviour {

    [SerializeField]
    Menu menu;
    public void DType()
    {
        menu.DeffType = gameObject.name;
        PlayerPrefs.SetString("DeffType", gameObject.name);
        menu.UpdateDeffTree();
    }
}
