/// <summary>
/// 
/// SimulationController script controls the input from UI buttons and is responsible for:
/// - random generation of palnts on terrain
/// - clearing all plants from terrain
/// - toggling between different modes (add, remove, toggle)
/// - adding new plant to the terrain on mouse click
/// - removing exisitng plant from the terrain on mouse click
/// - igniting selected plant on fire on mouse click
///     
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [SerializeField]
    private Terrain hillsTerrain;
    [SerializeField]
    private Text modeLabel;

    int terrainWidth;
    int terrainLength;
    int terrainPosX;
    int terrainPosZ;
    int numberOfPlants;
    int currentPlants;

    Mode currentMode;

    void Start()
    {
        currentMode = Mode.Add;

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

    public void TogglePlantFire()
    {

    }
}