/// <summary>
/// 
/// PLANT POOLER uses pooling technique to optimise performance:
/// 
/// - instead of creating and destroying many plants at runtime, they are all created at the start and stored in the "pool" of objects
/// - if new plants need to be added to the terrain, new objects are instantiated and stored in the pool
/// - "destroyed plants" are disabled, not destroyed completely
/// 
/// written by Gleb Mirolyubov, 2019
///     
/// </summary>
using System.Collections.Generic;
using UnityEngine;

public class PlantPooler : MonoBehaviour
{
    public static PlantPooler instance;

    [SerializeField]
    private List<GameObject> pooledPlants;
    [SerializeField]
    private int amountOfPlantsToPool;
    [SerializeField]
    private GameObject plantToPool;

    // Properties for these variables allow for better access control
    public List<GameObject> PooledPlants
    {
        get
        {
            return pooledPlants;
        }
    }

    public int AmountOfPlantsToPool
    {
        get
        {
            return amountOfPlantsToPool;
        }
    }

    void Awake()
    {
        instance = this;
    }

    // Creates the specified number of plants and adds them to the pool list
    void Start()
    {
        pooledPlants = new List<GameObject>();

        for (int i = 0; i < amountOfPlantsToPool; i++)
        {
            GameObject plant = Instantiate(plantToPool);
            plant.SetActive(false);
            pooledPlants.Add(plant);
        }
    }

    // Gets disabled plant
    public GameObject GetPooledPlant()
    {
        for (int i = 0; i < pooledPlants.Count; i++)
        {
            if (!pooledPlants[i].activeInHierarchy)
            {
                return pooledPlants[i];
            }
        }

        return null;
    }

    // Adds a new plant game object to the pool
    public GameObject AddNewPlantToPool()
    {
        GameObject plant = Instantiate(plantToPool);
        plant.SetActive(true);
        pooledPlants.Add(plant);
        return plant;
    }
}