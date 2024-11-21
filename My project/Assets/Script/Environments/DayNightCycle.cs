using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // Assign your directional light in the Inspector
    public Light playerLight;      // Assign the player's light in the Inspector
    public float dayDuration = 60f; // Duration of a full day in seconds
    public Material skyboxMaterial;
    private float rotationSpeed;
    private float maxExposure = 1f;       // Maximum exposure during the day
    private float minExposure = 0f;

    [Range(0f, 360f)] public float initialSunAngle = 180f; // Initial angle of the sun

    void Start()
    {
        // Calculate the rotation speed for the sun
        rotationSpeed = 360f / dayDuration;

        // Set the initial rotation of the directional light
        directionalLight.transform.rotation = Quaternion.Euler(initialSunAngle, 0, 0);

        // Set the initial skybox exposure
        skyboxMaterial = RenderSettings.skybox; // Automatically use the active skybox
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

        // Adjust skybox exposure
        AdjustSkyboxExposure();
    }

    private void AdjustSkyboxExposure()
    {
        // Get the sun's angle
        float sunAngle = directionalLight.transform.eulerAngles.x;

        // Calculate the exposure based on the sun's angle
        float exposure = Mathf.Lerp(minExposure, maxExposure, Mathf.InverseLerp(180f, 0f, sunAngle));

        // Apply the calculated exposure to the skybox
        if (skyboxMaterial != null && skyboxMaterial.HasProperty("_Exposure"))
        {
            skyboxMaterial.SetFloat("_Exposure", exposure);
        }
    }

    private bool IsNight()
    {
        // Night occurs when the sun is below the horizon (angle > 180 degrees)
        float sunAngle = directionalLight.transform.eulerAngles.x;
        return sunAngle > 180f && sunAngle < 360f;
    }

    private void AdjustAmbientLight()
    {
        float sunAngle = directionalLight.transform.eulerAngles.y;
        if (sunAngle > 180f) // Night
        {
            RenderSettings.ambientLight = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(180f, 360f, sunAngle));
        }
        else // Day
        {
            RenderSettings.ambientLight = Color.Lerp(Color.black, Color.white, Mathf.InverseLerp(0f, 180f, sunAngle));
        }
    }
}
