using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;    // Assign your directional light in the Inspector
    public Light playerLight;         // Assign the player's light in the Inspector
    public float dayDuration = 60f;   // Duration of a full day in seconds
    public Material skyboxMaterial;

    private float rotationSpeed;
    private float maxExposure = 1f;   // Maximum exposure during the day
    private float minExposure = 0f;   // Minimum exposure at night

    void Start()
    {
        // Calculate the rotation speed for the sun
        rotationSpeed = 360f / dayDuration;

        // Initialize the skybox material
        skyboxMaterial = RenderSettings.skybox;
        if (skyboxMaterial != null && skyboxMaterial.HasProperty("_Exposure"))
        {
            skyboxMaterial.SetFloat("_Exposure", maxExposure);
        }
    }

    void Update()
    {
        // Rotate the directional light to simulate the sun moving across the sky
        directionalLight.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        // Check if it's night and toggle the player's light
        if (IsNight())
        {
            playerLight.enabled = true;
        }
        else
        {
            playerLight.enabled = false;
        }

        // Gradually adjust the skybox exposure
        AdjustSkyboxExposure();
    }

    private void AdjustSkyboxExposure()
    {
        // Get the angle of the directional light relative to the horizon
        float sunAngle = Vector3.Dot(directionalLight.transform.forward, Vector3.down);

        // Map the sun angle to a value between minExposure and maxExposure
        float exposure = Mathf.Lerp(minExposure, maxExposure, (sunAngle + 1f) / 2f);

        // Apply the calculated exposure to the skybox
        if (skyboxMaterial != null && skyboxMaterial.HasProperty("_Exposure"))
        {
            skyboxMaterial.SetFloat("_Exposure", exposure);
        }
    }

    private bool IsNight()
    {
        // Use the sun's forward direction to determine if it's night
        return Vector3.Dot(directionalLight.transform.forward, Vector3.down) < 0f;
    }
}
