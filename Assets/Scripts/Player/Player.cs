using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float startingPoints = 0;
    public float points;
    public int flameIntensity;

    public float buttonMashForce;

    public void Start()
    {
        points = startingPoints; 
    }

    public void Update()
    {
        
    }

    public void AlterPointsLevel(float quantity)
    {
        points += quantity;
    }
}
