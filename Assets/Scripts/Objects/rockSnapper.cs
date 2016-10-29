using UnityEngine;
using System.Collections;

public class rockSnapper : MonoBehaviour {
    private NewtonVR.NVRInteractableItem nvritem;
    private Vector3 origin;
    public float maxRockDistance = 0.1f;
	// Use this for initialization
	void Awake () {
        origin = transform.position;
        nvritem = gameObject.GetComponent<NewtonVR.NVRInteractableItem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (nvritem.IsAttached)
        {
            if (Vector3.Distance(origin, nvritem.AttachedHand.transform.position) > maxRockDistance)
            {
                nvritem.AttachedHand = null;
            }
        }
	
	}
}