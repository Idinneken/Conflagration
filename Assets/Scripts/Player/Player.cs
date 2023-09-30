using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float initialPoints = 0;
    public float points;
    [Space]
    public PlayerData playerData;
    private int initialPlayerDataIndex = 0;
    public int playerDataIndex = 0;
    [Space]
    public int playerFlameIntensity;
    public float playerScale;
    public float playerCameraDistance;
    public float playerJumpForce;
    public float playerSpeed;
    public float playerButtonMashForce;
    [Space]
    public ThirdPersonFreeCam thirdPersonFreeCam;
    public PlayerMovement playerMovement;

    private static List<PlayerData> intensityScalePlayerData = new()
    {
        { new PlayerData { flameIntensity = 0, scale = 1f, jumpForce = 5f, cameraDistance = 10f, buttonMashForce = 1f, speed = 10f } },
        { new PlayerData { flameIntensity = 1, scale = 2f, jumpForce = 10f, cameraDistance = 15f, buttonMashForce = 1.5f, speed = 15f } },
        { new PlayerData { flameIntensity = 2, scale = 5f, jumpForce = 15f, cameraDistance = 20f, buttonMashForce = 2f, speed = 20f } },
    };

    public void Start()
    {
        points = initialPoints;
        playerDataIndex = initialPlayerDataIndex;
        UpdatePlayerData(intensityScalePlayerData[initialPlayerDataIndex]);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        }
    }

    public void UpdatePlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
        playerFlameIntensity = playerData.flameIntensity;
        playerScale = playerData.scale; gameObject.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        playerSpeed = playerData.speed; playerMovement.moveSpeed = playerData.speed;
        playerJumpForce = playerData.jumpForce; playerMovement.jumpForce = playerData.jumpForce;
        playerCameraDistance = playerData.cameraDistance; thirdPersonFreeCam.distance = playerData.cameraDistance;
        playerButtonMashForce = playerData.buttonMashForce;
    }
}

public struct PlayerData
{
    public int flameIntensity;
    public float scale;
    public float jumpForce;
    public float speed;
    public float cameraDistance;
    public float buttonMashForce;
}
