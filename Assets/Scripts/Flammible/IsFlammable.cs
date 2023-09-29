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
    [Space]
    private bool isFlammible = true;
    private bool isAblaze;

    public void Start()
    {
        health = startingHealth;
    }

    public void Update()
    {
        if (useFlammibleOverride) { isFlammible = flammibleOverride; }
        if (useAblazeOverride) { isAblaze = ablazeOverride; } 


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleAblaze();
        }

        if (isAblaze)
        {
            Burn();
        }
    }

    public bool TrySetAblazeReturn(int intensity)
    {
        if (intensity >= flameIntensityRequirement)
        {
            SetAblaze(); return true;
        }
        return false;
    }

    public void TrySetAblaze()
    {
        if (Singletons.instance.player.flameIntensity >= flameIntensityRequirement)
        {
            SetAblaze();
        }
    }

    #region Setting ablaze/Extinguishing

    void SetAblaze()
    {
        isAblaze = true;
        GetComponent<Renderer>().material.color = Color.red;
    }

    void Extinguish()
    {
        isAblaze = false;
        GetComponent<Renderer>().material.color = Color.white;
    }

    void ToggleAblaze()
    {
        if (isAblaze) { Extinguish(); }
        else { SetAblaze(); }
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
