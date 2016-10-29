/*
 * This fancy script is so that when something is put into the box, it will not jump out when you start driving.
 * To accomplish this, we need to have a duplicate box, which is under the terrain and doesn't move. 
 * Whenever something enters the box, we actually duplicate and put it in the unmoving box under the terrain.
 * The object the user can see is then being controller by it's duplicate under the terrain, and isn't affected by sudden stops, and physics glitches.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class fancyBoxScript : MonoBehaviour
{
    private GameObject movingBox; //this is itself.
    public GameObject nonMovingBox; //Needs to be the exact same size as movingBox
    public NewtonVR.NVRHand leftHand;
    public NewtonVR.NVRHand rightHand;
    public GameObject rover;
    public Skateboard roverScript;
    List<GameObject> toolsInBox = new List<GameObject>();
    List<GameObject> spawnedTools = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root != null)
        {
            if (col.transform.root.GetComponent<NewtonVR.NVRInteractableItem>() != null)
            {

            }
        }
        Debug.Log("just entered: " + col.gameObject.name);
        toolsInBox.Add(col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        if (toolsInBox.Contains(col.gameObject))
        {
            toolsInBox.Remove(col.gameObject);
        }
    }
    void Awake()
    {
        movingBox = gameObject;
    }
    void Start()
    {
    }

    void Update()
    {

    }
}