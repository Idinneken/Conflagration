using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseOfSingleton : MonoBehaviour
{
    public void Start()
    {
        Singletons.instance.player.AlterPointsLevel(500); //Adding points (this would normally be done through an event)
        Singletons.instance.playerGameObject.transform.localScale = new Vector3(10,10,10); //Setting then resetting the scale of the player
        Singletons.instance.playerGameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
