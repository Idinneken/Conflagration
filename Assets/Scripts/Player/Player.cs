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
        { new PlayerData { flameIntensity = 0, scale = 1f, jumpForce = 5f, cameraDistance = 10f, buttonMashForce = 0.1f } },
        { new PlayerData { flameIntensity = 1, scale = 2f, jumpForce = 10f, cameraDistance = 15f, buttonMashForce = 0.15f } },
        { new PlayerData { flameIntensity = 2, scale = 5f, jumpForce = 15f, cameraDistance = 20f, buttonMashForce = 0.2f } },
    };

    public void Start()
    {
        points = initialPoints;
        playerDataIndex = initialPlayerDataIndex;
        UpdatePlayerData(intensityScalePlayerData[initialPlayerDataIndex]);

        string audioStart = (0).ToString() + "FlameIntensity";
        FindFirstObjectByType<AudioManager>().PlayAudio(audioStart);
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
            
            PlayAudio(playerFlameIntensity);
            
        }
    }

    public void UpdatePlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
        playerFlameIntensity = playerData.flameIntensity;
        playerScale = playerData.scale; gameObject.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
        playerJumpForce = playerData.jumpForce; playerMovement.jumpForce = playerData.jumpForce;
        playerCameraDistance = playerData.cameraDistance; thirdPersonFreeCam.distance = playerData.cameraDistance;
        playerButtonMashForce = playerData.buttonMashForce;
    }

    public void PlayAudio(int playerIntensity)
    {
        string fireAlarm = "FireAlarm";
        FindFirstObjectByType<AudioManager>().PlayAudio(fireAlarm);

        string previousIntensity = (playerIntensity - 1).ToString() + "FlameIntensity";
        FindFirstObjectByType<AudioManager>().StopAudio(previousIntensity);

        string audioIntensity = playerIntensity + "FlameIntensity" ;
        FindFirstObjectByType<AudioManager>().PlayAudio(audioIntensity);
    }
}

public struct PlayerData
{
    public int flameIntensity;
    public float scale;
    public float jumpForce;
    public float cameraDistance;
    public float buttonMashForce;
}
