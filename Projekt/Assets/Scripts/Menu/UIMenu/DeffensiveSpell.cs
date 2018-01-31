using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeffensiveSpell : MonoBehaviour {

    [SerializeField]
    Menu menu;
    //Funkcja do przycisków wyboru slotu
    public void Deffensive()
    {
        menu.ChoosingDeff = gameObject.name;
    }
    //Funkcja do przycisków wyboru spella
    public void Spell()
    {
        menu.Deffensive(gameObject.GetComponent<Button>());
    }
}
