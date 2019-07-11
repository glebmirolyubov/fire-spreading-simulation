/// <summary>
/// 
/// Plant is the base script for plant object that tracks the current state of the plant
///     
/// </summary>
using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    PlantState plantState;
    Material plantMaterial;

    void OnEnable()
    {
        plantState = PlantState.Base;
        plantMaterial.color = Color.green;
    }

    public void SetPlantOnFire()
    {
        plantState = PlantState.Burning;
    }

    public void SetPlantToBurnt()
    {
        plantState = PlantState.Burnt;
    }
}