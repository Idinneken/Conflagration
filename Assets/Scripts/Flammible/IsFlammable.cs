using JetBrains.Annotations;
using System.Collections; 
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class IsFlammable : MonoBehaviour
{
    [Header("Dev/Special cases")]
    public bool useFlammibleOverride;
    public bool flammibleOverride;
    public bool useAblazeOverride;
    public bool ablazeOverride;

    [Header("Health")]
    public float startingHealth = 100; 
    public float health;
    private float minHealth = 0;
    [Header("Intensity requirement")]
    public int flameIntensityRequirement;
    public float healthReductionAmount = 10;
    public float healthReductionRateInSecs = 1;
    private bool isFlammible = true;
    private bool isAblaze;

    [Header("Button Mashing")]
    public bool usesButtonMashing = true;
    public bool buttonMashingApplicable = true;
    private bool buttonMashingInProgress = false;
    public float buttonMashProgress = 0f;
    public float buttonMashThreshold = 1f;
    public float buttonMashEffortMultiplier = 1f; 
    public float buttonMashDecayAmount = 0.1f;
    public float buttonMashDecayRateInSecs = 1f;
    public float buttonMashingInProgressSmokeEmissionMultiplier = 10f;

    [Space]
    public ParticleSystem flameParticle;
    public ParticleSystem smokeParticle;
    public ColourControl colourControl;

    private ParticleSystem.EmissionModule flameParticleEmission;
    private ParticleSystem.ShapeModule flameParticleShape;
    private ParticleSystem.EmissionModule smokeParticleEmission;
    private ParticleSystem.ShapeModule smokeParticleShape;
    private MeshFilter modelMeshFilter;

    public void Start()
    {
        health = startingHealth;

        modelMeshFilter = GetComponent<MeshFilter>();
        flameParticleEmission = flameParticle.emission;
        flameParticleShape = flameParticle.shape;
        smokeParticleEmission = smokeParticle.emission;
        smokeParticleShape = smokeParticle.shape;
        
        flameParticleEmission.enabled = false;
        flameParticleShape.mesh = modelMeshFilter.mesh;
        smokeParticleEmission.enabled = false;
        smokeParticleShape.mesh = modelMeshFilter.mesh;
    }

    public void Update()
    {
        if (useFlammibleOverride) { isFlammible = flammibleOverride; } else { isFlammible = true;}
        if (useAblazeOverride) { isAblaze = ablazeOverride; } 

        if (isAblaze)
        {
            Burn();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !buttonMashingInProgress && flameIntensityRequirement <= Singletons.instance.player.playerFlameIntensity)
        {
            buttonMashingInProgress = true;
            colourControl.PulsateBetween(Color.white, new Color(1f, 0.5f, 0.3f, 1f), 4f);

            if(flameIntensityRequirement < Singletons.instance.player.playerFlameIntensity)
            {
                SetAblaze();
            }
        }

        if (buttonMashingApplicable && buttonMashingInProgress && usesButtonMashing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                buttonMashProgress += Singletons.instance.player.playerButtonMashForce * buttonMashEffortMultiplier;
            }

            if (buttonMashProgress >= buttonMashThreshold)
            {
                SetAblaze();
            }

            buttonMashProgress -= buttonMashDecayAmount * Time.deltaTime * buttonMashDecayRateInSecs;

            if (buttonMashProgress <= 0) { buttonMashProgress = 0; buttonMashingInProgress = false; StopSmokeParticles(); colourControl.StopPulsate(); }
        }

        if (buttonMashingInProgress)
        {
            EmitSmokeParticles();
            smokeParticleEmission.rateOverTime = buttonMashProgress * buttonMashingInProgressSmokeEmissionMultiplier;
        }
    }

    public bool TrySetAblazeReturn(int intensity)
    {
        if (intensity >= flameIntensityRequirement && isFlammible)
        {
            SetAblaze(); 
            return true;
        }
        return false;
    }

    public void TrySetAblaze()
    {
        if (Singletons.instance.player.playerFlameIntensity >= flameIntensityRequirement && isFlammible)
        {
            SetAblaze();
        }
    }

    #region Setting ablaze/Extinguishing

    void SetAblaze()
    {
        isAblaze = true;
        buttonMashingApplicable = false;
        
        colourControl.SetColour(Color.red, false);
        EmitParticles();
    }

    void Extinguish()
    {
        isAblaze = false;
        buttonMashingApplicable = true;
        buttonMashProgress = 0f;
        colourControl.SetColour(Color.white, true);
        StopParticles();
    }

    void ToggleAblaze()
    {
        if (isAblaze) { Extinguish(); }
        else { SetAblaze(); }
    }

    void EmitParticles()
    {
        EmitFireParticles();
        EmitSmokeParticles();
    } 

    void StopParticles()
    {
        StopFireParticles();
        StopSmokeParticles();
    }

    void EmitSmokeParticles()
    {
        smokeParticleEmission.enabled = true;
    }

    void StopSmokeParticles()
    {
        smokeParticleEmission.enabled = false;
    }

    void EmitFireParticles()
    {
        flameParticleEmission.enabled = true;
    }

    void StopFireParticles()
    {
        flameParticleEmission.enabled = false;
    }

    #endregion

    #region Health stuff

    void Burn()
    {
        health -= healthReductionAmount * Time.deltaTime * healthReductionRateInSecs;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (health <= minHealth) { DestroyThis(); }
    }

    #endregion

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

}
