using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class hostbuttonanim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    

    public Animator button;
    public string s1, s2;


    public void OnPointerEnter(PointerEventData eventData)
    {
        button.Play(s1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.Play(s2);
    }
}
