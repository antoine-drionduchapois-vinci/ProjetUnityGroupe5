using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Material skybox;
    public Light directionalLight; // Assign your directional light in the Inspector
    public Light playerLight;      // Assign the player's light in the Inspector
    public float dayDuration = 60f; // Duration of a full day in seconds

    private float rotationSpeed;

    void Start()
    {
        // Calculate the rotation speed for the sun
        rotationSpeed = 360f / dayDuration;
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
        AdjustAmbientLight();
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
