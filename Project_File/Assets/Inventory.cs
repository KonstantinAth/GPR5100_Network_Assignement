using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI quantityText;
    private int hourglasses;
    public int Hourglasess
    {
        get { return hourglasses;}
        set { hourglasses = value;}
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        HourGlass.onHourglassPickupCall += AddHourglassCallBack;
    }
    private void OnDisable()
    {
        HourGlass.onHourglassPickupCall -= AddHourglassCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddHourglassCallBack()
    {
        Hourglasess++;
        quantityText.text= "X " +Hourglasess;
    }
    public void RemoveHourglass()
    {
        // time logic // death logic
    }
}
