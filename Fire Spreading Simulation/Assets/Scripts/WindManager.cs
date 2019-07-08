using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    [SerializeField]
    private WindZone wind;
    [SerializeField]
    private Text windStrengthText;
    [SerializeField]
    private Text windDirectionText;

    float minWindStrength = 0f;
    float maxWindStrength = 100f;

    float minWindDirection = 0f;
    float maxWindDirection = 360f;

    public void ChangeWindStrength (float strength)
    {
        wind.windMain = strength;

        windStrengthText.text = strength.ToString();
    }

    public void ChangeWindDirection (float direction)
    {
        wind.gameObject.transform.rotation = Quaternion.Euler(0, direction, 0);

        windDirectionText.text = direction.ToString();
    }
}
