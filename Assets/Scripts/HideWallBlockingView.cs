using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWallBlockingView : MonoBehaviour
{
    public GameObject currentBlockingWall;
    public GameObject previousBlockingWall;


    
    //update function that checks if a gameobject tagged 'Wall' is in the way of the camera using a raycast, if so then hide its mesh renderer. Only Re-enable the mesh renderer if a new object is in the way, or there is no longer an object in the way.
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                currentBlockingWall = hit.collider.gameObject;
                if (currentBlockingWall != previousBlockingWall)
                {
                    currentBlockingWall.GetComponent<MeshRenderer>().enabled = false;
                    if (previousBlockingWall != null)
                    {
                        previousBlockingWall.GetComponent<MeshRenderer>().enabled = true;
                    }
                    previousBlockingWall = currentBlockingWall;
                }
            }
            else
            {
                if (previousBlockingWall != null)
                {
                    previousBlockingWall.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        else
        {
            if (previousBlockingWall != null)
            {
                previousBlockingWall.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }






}
