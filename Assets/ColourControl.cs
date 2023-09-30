using UnityEngine;

public class ColourControl : MonoBehaviour
{
    internal Color initialColor;
    internal Color baseColor;
    public Color startPulseColor;
    public Color endPulseColor;
    private float pulsateSpeed = 1.0f;

    internal Renderer meshRenderer;
    private Color currentColor;
    private bool isPulsating = false;

    private void Start()
    {
        // Assuming you have a Renderer component attached to the GameObject
        meshRenderer =  GetComponent<Renderer>();
        initialColor = meshRenderer.material.color;
        baseColor = initialColor;
    }

    private void Update()
    {
        if (isPulsating)
        {
            // Calculate the pulsating color
            currentColor = Color.Lerp(startPulseColor, endPulseColor, Mathf.PingPong(Time.time * pulsateSpeed, 1));
            meshRenderer.material.color = currentColor;
        }
    }

    // Start pulsating the color with specified target colors
    public void PulsateBetween(Color startPulseColor, Color endPulseColor, float pulsateSpeed)
    {
        this.startPulseColor = startPulseColor; // Update the start color
        this.endPulseColor = endPulseColor; // Update the end color
        this.pulsateSpeed = pulsateSpeed;
        isPulsating = true;
    }

    // Stop pulsating the color
    public void StopPulsate()
    {
        isPulsating = false;
        meshRenderer.material.color = baseColor; // Reset the color to the start color
    }

    public void SetColour(Color color, bool setAsBase)
    {
        isPulsating = false;
        meshRenderer.material.color = color;
        if (setAsBase) { baseColor = color; }
    }
}
