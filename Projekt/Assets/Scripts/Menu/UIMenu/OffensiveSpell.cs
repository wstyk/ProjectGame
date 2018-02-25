using UnityEngine;
using UnityEngine.UI;

public class OffensiveSpell : MonoBehaviour {

    [SerializeField]
    Menu menu;
    //Funkcja do przycisków wyboru slotu
	public void Offensive()
    {
        if (menu.ChoosingOff != null) menu.ChoosingOff.transform.localScale = new Vector3(1, 1, 1);
        if (menu.ChoosingDeff != null) menu.ChoosingDeff.transform.localScale = new Vector3(1, 1, 1);
        menu.ChoosingOff = gameObject;
        menu.ChoosingOff.transform.localScale = new Vector3(2, 2, 2);
    }
    
    //Funkcja do przycisków wyboru spella
    public void Spell()
    {
        menu.Offensive(gameObject.GetComponent<Button>());
    }
}
