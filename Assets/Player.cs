using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float startingPoints = 0;
    public float points;

    public void Start()
    {
        points = startingPoints; 
    }

    public void AlterPointsLevel(float quantity)
    {
        points += quantity;
    }
}
