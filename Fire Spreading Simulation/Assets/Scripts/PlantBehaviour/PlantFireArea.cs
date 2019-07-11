/// <summary>
/// 
/// PlantFireArea controls the trigger collider around the plant to imitate the wind forces and to allow fire to propagate:
/// - as the strength of the wind increases, so does the length of the capsule collider
/// - capsule collider is pivoted at the origin of the plant
/// - capsule collider rotates with wind direction to imitate the correct propagation
///     
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class PlantFireArea : MonoBehaviour
{
    private Slider windStrengthSlider;
    private Slider windDirectionSlider;

    CapsuleCollider fireAreaCollider;

    [Range(4f, 10f)]
    float colliderSize;
    [Range(0f, 3f)]
    float colliderPosition;

    void Start()
    {
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
}