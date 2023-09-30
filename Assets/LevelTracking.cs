using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelTracking : MonoBehaviour
{
    public List<IsFlammable> allFlammables;
    public List<List<IsFlammable>> flammablesTiered = new();

    public int tier0Requirement; 
    public int tier1Requirement;
    public int tier2Requirement;

    public int tier0destroyed;
    public int tier1destroyed;
    public int tier2destroyed;

    void Start()
    {
        allFlammables = GetAllFlammibleObjects();
        flammablesTiered.Add(new());
        flammablesTiered.Add(new());
        flammablesTiered.Add(new());

        foreach (IsFlammable isFlammable in allFlammables) 
        {
            flammablesTiered[isFlammable.flameIntensityRequirement].Add(isFlammable);
        }

        Debug.Log($"flammablesTiered[0].Count {flammablesTiered[0]?.Count}");
        Debug.Log($"flammablesTiered[1].Count {flammablesTiered[1]?.Count}");
        Debug.Log($"flammablesTiered[2].Count {flammablesTiered[2]?.Count}");
    }

    public List<IsFlammable> GetAllFlammibleObjects() => FindObjectsByType<IsFlammable>(FindObjectsSortMode.None).ToList();

    public void FlammibleObjectDestroyed(int tier)
    {
        if (tier == 0) { tier0destroyed++; }
        if (tier == 1) { tier1destroyed++; }
        if (tier == 2) { tier2destroyed++; }

        CheckIfUpdatePlayerTier();
    }

    private void CheckIfUpdatePlayerTier()
    {
        Player player = Singletons.instance.player;
        if (player.playerFlameIntensity == 0 && tier0destroyed >= tier0Requirement)
        {
            player.IncreaseIntensity();
        }

        if (player.playerFlameIntensity == 1 && tier1destroyed >= tier1Requirement)
        {
            player.IncreaseIntensity();
        }

        if (player.playerFlameIntensity == 2 && tier2destroyed >= tier2Requirement)
        {
            player.IncreaseIntensity();
        }
    }
}
