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

    void Start()
    {
        // Calculate the rotation speed for the sun
        rotationSpeed = 360f / dayDuration;
       
        
           skyboxMaterial = RenderSettings.skybox; // Automatically use the active skybox
        
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

        // Adjust ambient light for smooth transitions between day and night
        // AdjustAmbientLight();
        AdjustSkyboxExposure();

    }
    private void AdjustSkyboxExposure()
    {
        // Get the sun's angle (y-axis determines horizontal rotation)
        float sunAngle = directionalLight.transform.eulerAngles.y;

        // Calculate exposure: 
        // - Sun rising: 0 to 180 degrees -> minExposure to maxExposure
        // - Sun setting: 180 to 360 degrees -> maxExposure to minExposure
        float exposure = sunAngle <= 180f
            ? Mathf.Lerp(minExposure, maxExposure, Mathf.InverseLerp(0f, 180f, sunAngle)) // Daytime
            : Mathf.Lerp(maxExposure, minExposure, Mathf.InverseLerp(180f, 360f, sunAngle)); // Nighttime

        // Apply the calculated exposure to the skybox
        if (skyboxMaterial.HasProperty("_Exposure"))
        {
            skyboxMaterial.SetFloat("_Exposure", exposure);
        }
    }

    // Determine if it's currently night
    private bool IsNight()
    {
        // Night occurs when the sun is below the horizon (angle > 180 degrees)
        float sunAngle = directionalLight.transform.eulerAngles.x;
        return sunAngle > 180f && sunAngle < 360f;
    }

    // Smoothly adjust the ambient light based on the sun's angle
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
