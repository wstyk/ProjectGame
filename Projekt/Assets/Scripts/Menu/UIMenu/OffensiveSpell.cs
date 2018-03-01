using UnityEngine;
using UnityEngine.UI;

public class OffensiveSpell : MonoBehaviour {

    [SerializeField]
    Menu menu;
    [SerializeField]
    SpellList spellList;
    //Funkcja do przycisków wyboru slotu
	public void Offensive()
    {
        if (menu.ChoosingOff != null) menu.ChoosingOff.transform.localScale = new Vector3(1, 1, 1);
        if (menu.ChoosingDeff != null) menu.ChoosingDeff.transform.localScale = new Vector3(1, 1, 1);
        menu.ChoosingOff = gameObject;
        menu.ChoosingOff.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    
    //Funkcja do przycisków wyboru spella
    public void Spell()
    {
        int index = 0;
        for (int i = 0; i < spellList.Names.Count; i++)
        {
            if (gameObject.name == spellList.Names[i])
            {
                index = i;
                i = spellList.Names.Count;
            }
        }
        menu.Offensive(gameObject.GetComponent<Button>(), index);
    }
}
