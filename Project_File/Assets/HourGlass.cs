using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HourGlass : MonoBehaviour
{
    public static event Action onHourglassPickupCall;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            onHourglassPickupCall?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
