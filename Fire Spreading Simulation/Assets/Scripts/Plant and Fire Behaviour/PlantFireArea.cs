/// <summary>
/// 
/// PLANT FIRE AREA controls the trigger collider around the plant to imitate the wind forces and to allow fire to propagate:
/// 
/// - as the strength of the wind increases, so does the length of the capsule collider
/// - capsule collider is pivoted at the origin of the plant
/// - capsule collider rotates with wind direction to imitate the correct propagation
///     
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlantFireArea : MonoBehaviour
{
    private Slider windStrengthSlider;
    private Slider windDirectionSlider;

    private CapsuleCollider fireAreaCollider;

    [Range(4f, 10f)]
    float colliderSize;
    [Range(0f, 3f)]
    float colliderPosition;

    private List<GameObject> nearbyPlants;

    public List<GameObject> NearbyPlants
    {
        get
        {
            return nearbyPlants;
        }
    }

    void OnDisable()
    {
        // clear the list since this plant is pooled back
        nearbyPlants.Clear();
    }

    void Awake()
    {
        nearbyPlants = new List<GameObject>();
        fireAreaCollider = GetComponent<CapsuleCollider>();
        windStrengthSlider = WindController.instance.WindStrengthSlider;
        windDirectionSlider = WindController.instance.WindDirectionSlider;

        windStrengthSlider.onValueChanged.AddListener(delegate { WindStrengthChange(); });
        windDirectionSlider.onValueChanged.AddListener(delegate { WindDirectionChange(); });
    }

    void WindDirectionChange()
    {
        fireAreaCollider.transform.rotation = Quaternion.Euler(0, windDirectionSlider.value, 0);
    }

    void WindStrengthChange()
    {
        colliderSize = Mathf.Lerp(4, 10, Mathf.InverseLerp(0, 100, windStrengthSlider.value));
        colliderPosition = Mathf.Lerp(0, 3, Mathf.InverseLerp(0, 100, windStrengthSlider.value));
        fireAreaCollider.height = colliderSize;
        fireAreaCollider.center = new Vector3(0, 0, colliderPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FireArea")
        {
            GameObject plant = other.gameObject.transform.parent.gameObject;
            nearbyPlants.Add(plant);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FireArea")
        {
            if (nearbyPlants.Contains(other.gameObject.transform.parent.gameObject))
            {
                nearbyPlants.Remove(other.gameObject.transform.parent.gameObject);
            }
        }
    }
}