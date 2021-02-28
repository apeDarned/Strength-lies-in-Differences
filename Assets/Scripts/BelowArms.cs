using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelowArms : MonoBehaviour
{
    [SerializeField] private Transform abovePosition;
    [SerializeField] private float size;
    [SerializeField] private LayerMask layerMask;
    public bool belowArms;



    private void Update()
    {
        if (Physics.CheckSphere(abovePosition.position, size, layerMask)) {
            belowArms = true;
        } else {
            belowArms = false;
        }
    }
}
