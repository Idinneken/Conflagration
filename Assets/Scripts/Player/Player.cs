using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float initialPoints = 0;
    public float points;

    public PlayerData playerData;
    private int initialPlayerDataIndex = 0;
    public int playerDataIndex = 0;

    public int flameIntensity;
    public float playerScale;
    public float cameraDistance;
    public float buttonMashForce;

    private static List<PlayerData> intensityScalePlayerData = new()
    {
        { new PlayerData { flameIntensity = 0, playerScale = 1f, cameraDistance = 10f, buttonMashForce = 1f } },
        { new PlayerData { flameIntensity = 1, playerScale = 2f, cameraDistance = 15f, buttonMashForce = 1.5f } },
        { new PlayerData { flameIntensity = 2, playerScale = 5f, cameraDistance = 20f, buttonMashForce = 2f } },
        { new PlayerData { flameIntensity = 3, playerScale = 10f, cameraDistance = 30f, buttonMashForce = 3f } },
    };

    public ThirdPersonFreeCam thirdPersonFreeCam;

    public void Start()
    {
        points = initialPoints;
        playerDataIndex = initialPlayerDataIndex;
        UpdatePlayerData(intensityScalePlayerData[initialPlayerDataIndex]);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseIntensity();
        }
    }

    public void AlterPointsLevel(float quantity)
    {
        points += quantity;
    }

    public void IncreaseIntensity()
    {
        playerDataIndex++;
        if (playerDataIndex <= intensityScalePlayerData.Count)
        {
            UpdatePlayerData(intensityScalePlayerData[playerDataIndex]);

            gameObject.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
            thirdPersonFreeCam.distance = cameraDistance;
        }
    }

    public void UpdatePlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
        playerScale = playerData.playerScale;
        cameraDistance = playerData.cameraDistance;
        buttonMashForce = playerData.buttonMashForce;
    }
}

public struct PlayerData
{
    public int flameIntensity;
    public float playerScale;
    public float cameraDistance;
    public float buttonMashForce;
}
