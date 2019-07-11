/// <summary>
/// 
/// SIMULATION CONTROLLER script controls the input from UI buttons and is responsible for:
/// 
/// - random generation of palnts on terrain
/// - clearing all plants from terrain
/// - setting random plants on fire
/// - starting and stopping simulation
/// - toggling between different modes (add, remove, toggle)
/// - adding new plant to the terrain on mouse click
/// - removing exisitng plant from the terrain on mouse click
/// - igniting selected plant on fire on mouse click
/// - quitting the game
/// 
/// written by Gleb Mirolyubov, 2019
///     
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimulationController : MonoBehaviour
{
    [SerializeField]
    private Terrain hillsTerrain;
    [SerializeField]
    private Text modeLabel;
    [SerializeField]
    private Text simulationText;

    int terrainWidth;
    int terrainLength;
    int terrainPosX;
    int terrainPosZ;
    int numberOfPlants;
    int currentPlants;

    Mode currentMode;

    void Awake()
    {
        // stop the time from the very beginning to not play the simulation
        Time.timeScale = 0;
    }

    void Start()
    {
        currentMode = Mode.Add;
        simulationText.text = "Play Simulation";

        terrainWidth = (int)hillsTerrain.terrainData.size.x;
        terrainLength = (int)hillsTerrain.terrainData.size.z;
        terrainPosX = (int)hillsTerrain.transform.position.x;
        terrainPosZ = (int)hillsTerrain.transform.position.z;
        numberOfPlants = PlantPooler.instance.AmountOfPlantsToPool;
        currentPlants = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentMode)
            {
                case Mode.Add:
                    AddPlant();
                    break;
                case Mode.Remove:
                    RemovePlant();
                    break;
                case Mode.Toggle:
                    TogglePlantFire();
                    break;
            }
        }
    }

    //Randomly place plants, that were pooled at the start of the scene, on the terrain while clearing already existing ones
    public void GeneratePlants()
    {
        // clear existing plants
        ClearPlants();

        while (currentPlants <= numberOfPlants)
        {
            int posX = Random.Range(terrainPosX, terrainPosX + terrainWidth);
            int posZ = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
            float posY = hillsTerrain.SampleHeight(new Vector3(posX, 0, posZ));

            GameObject plant = PlantPooler.instance.GetPooledPlant();
            if (plant != null)
            {
                plant.transform.position = new Vector3(posX, posY, posZ);
                plant.transform.rotation = Quaternion.identity;
                plant.SetActive(true);
            }
            currentPlants += 1;
        }
    }

    // Clear all plants from the terrain by moving them back to the plant pool
    public void ClearPlants()
    {
        foreach (GameObject plant in PlantPooler.instance.PooledPlants)
        {
            plant.SetActive(false);
        }

        currentPlants = 0;
    }

    // Ignite 10% of the active plants on fire
    public void SetRandomPlantsOnFire()
    {
        List<GameObject> plantsToIgnite = new List<GameObject>();

        // get all active and unburnt plants from the scene and add them to the list
        foreach (GameObject plant in PlantPooler.instance.PooledPlants)
        {
            if (plant.activeInHierarchy && plant.GetComponent<Plant>().PlantState == PlantState.Base)
            {
                plantsToIgnite.Add(plant);
            }
        }

        // ignite 10% of these plants using random method
        for (int i = 0; i < (int)(plantsToIgnite.Count * 0.1); i++)
        {
            plantsToIgnite[Random.Range(0, plantsToIgnite.Count - 1)].GetComponent<Plant>().SetPlantOnFire();
        }
    }

    // Turn simulation on and off
    public void ToggleSimulation()
    {
        switch (Time.timeScale)
        {
            case 1:
                Time.timeScale = 0;
                simulationText.text = "Play Simulation";
                break;
            case 0:
                Time.timeScale = 1;
                simulationText.text = "Stop Simulation";
                break;
        }
    }

    // Toggle between different modes
    public void ToggleMode()
    {
        // change current mode to the next in order
        switch (currentMode)
        {
            case Mode.Add:
                currentMode = Mode.Remove;
                break;
            case Mode.Remove:
                currentMode = Mode.Toggle;
                break;
            case Mode.Toggle:
                currentMode = Mode.Add;
                break;
            default:
                currentMode = Mode.Add;
                break;
        }

        // Update label text with current mode
        modeLabel.text = currentMode.ToString();
    }

    // Add plant on a click of a mouse
    public void AddPlant()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == hillsTerrain.gameObject.name)
            {
                GameObject plant = PlantPooler.instance.AddNewPlantToPool();
                if (plant != null)
                {
                    plant.transform.position = hit.point;
                }
            }
        }
    }

    // Remove plant under the click of the mouse
    public void RemovePlant()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Plant")
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    // Ignite plant under the mouse coursor on click
    public void TogglePlantFire()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Plant")
            {
                // if the plant is healthy, set it on fire
                if (hit.collider.gameObject.GetComponent<Plant>().PlantState == PlantState.Base)
                {
                    hit.collider.gameObject.GetComponent<Plant>().SetPlantOnFire();
                }
            }
        }
    }

    public void QuitSimulation()
    {
        Application.Quit(0);
    }
}