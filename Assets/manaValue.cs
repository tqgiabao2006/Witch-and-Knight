using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manaValue : MonoBehaviour
{
    public Slider slider;
    public void Current(float mana)
    {
        slider.value = mana;
    }
    public void SetMana(float mana)
    {
        slider.value = mana;
    }

}
