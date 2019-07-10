using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    PlantState plantState;

    void OnEnable()
    {
        plantState = PlantState.Base;
    }
}
