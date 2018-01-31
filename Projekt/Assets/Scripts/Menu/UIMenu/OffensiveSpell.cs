using UnityEngine;
using UnityEngine.UI;

public class OffensiveSpell : MonoBehaviour {

    [SerializeField]
    Menu menu;
    //Funkcja do przycisków wyboru slotu
	public void Offensive()
    {
        menu.ChoosingOff = gameObject.name;
    }
    
    //Funkcja do przycisków wyboru spella
    public void Spell()
    {
        menu.Offensive(gameObject.GetComponent<Button>());
    }
}
