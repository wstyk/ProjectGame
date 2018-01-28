using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffensiveSpell : MonoBehaviour {

    public void Deffensive()
    {
        PlayerPrefs.SetString("ChoosingDeff", gameObject.name);
    }
}
