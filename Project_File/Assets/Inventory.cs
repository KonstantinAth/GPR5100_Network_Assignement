using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Inventory : MonoBehaviour {
    //event
    public static event Action<int> onRemoveCall;
    [SerializeField]
    TextMeshProUGUI quantityText;
    private int hourglasses;
    public int Hourglasess {
        get { return hourglasses; }
        set { hourglasses = value; }
    }
    // Start is called before the first frame update
    private void OnEnable() {
        Trap.onTrapHitCall += RemoveHourglass;
        HourGlass.onHourglassPickupCall += AddHourglassCallBack;
    }
    private void OnDisable() {
        Trap.onTrapHitCall -= RemoveHourglass;
        HourGlass.onHourglassPickupCall -= AddHourglassCallBack;
    }
    public void AddHourglassCallBack() {
        Hourglasess++;
        quantityText.text = "X " + Hourglasess;
    }
    public void RemoveHourglass() {
        onRemoveCall?.Invoke(hourglasses);
        hourglasses = 0;
        quantityText.text = "X " + Hourglasess;
    }
}