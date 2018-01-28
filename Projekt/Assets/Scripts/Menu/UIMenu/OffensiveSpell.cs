using UnityEngine;
using UnityEngine.UI;

public class OffensiveSpell : MonoBehaviour {

    [SerializeField]
    Menu menu;
    //Funkcja do przycisków wyboru slotu
	public void Offensive()
    {
        PlayerPrefs.SetString("ChoosingOff", gameObject.name);
    }
    
    //Funkcja do przycisków wyboru spella
    public void Spell()
    {
        menu.Offensive(gameObject.GetComponent<Button>());
    }
}
