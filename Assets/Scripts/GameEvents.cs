using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GameEvents : MonoBehaviour
{

    public static GameEvents instance;

    private void Awake(){
        instance = this;
    }

    public event Action<bool, bool> OnAcidBasePour;
    public event Action<bool, bool> OnColorChange;

    public event Action OnWearable;

    public void AcidBasePour(bool arg1, bool arg2) => OnAcidBasePour ?.Invoke(arg1 , arg2);
    public void ColorChange(bool arg1, bool arg2) => OnColorChange?.Invoke(arg1, arg2);

    public void Wearable() => OnWearable?.Invoke();
}
