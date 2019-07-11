/// <summary>
/// 
/// PLANT is the base script for plant object that tracks the current state of the plant
///     
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{
    private PlantState plantState;
    private Renderer plantRenderer;

    public PlantState PlantState
    {
        get
        {
            return plantState;
        }
    }

    void Awake()
    {
        plantRenderer = transform.GetComponent<Renderer>();
        plantState = PlantState.Base;
    }

    void OnDisable()
    {
        plantState = PlantState.Base;
        plantRenderer.material.color = Color.green;
    }

    public void SetPlantOnFire()
    {
        plantState = PlantState.Burning;
        plantRenderer.material.color = Color.red;
        StartCoroutine("BurningPlant");
    }

    public void SetPlantToBurnt()
    {
        plantState = PlantState.Burnt;
        plantRenderer.material.color = Color.black;
    }

    void CheckNearbyPlants()
    {
        // get the list of nearby plants from the child object
        List<GameObject> nearbyPlants = transform.GetChild(0).GetComponent<PlantFireArea>().NearbyPlants;

        if (nearbyPlants != null)
        {
            foreach (GameObject plant in nearbyPlants)
            {
                // check if the plant nearby is not burning already
                if (plant.GetComponent<Plant>().plantState == PlantState.Base)
                {
                    LightUpNearbyPlant(plant);
                }
            }
        }
    }

    void LightUpNearbyPlant(GameObject plant)
    {
        plant.GetComponent<Plant>().SetPlantOnFire();
    }

    IEnumerator BurningPlant()
    {
        yield return new WaitForSeconds(1f);
        CheckNearbyPlants();
        yield return new WaitForSeconds(2f);
        SetPlantToBurnt();
    }
}