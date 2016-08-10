using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class rockDetector : MonoBehaviour {
	private List<GameObject> rocks = new List<GameObject>();
	private bool isLazyLoop = false;
	public float detectionRadius = 3.4f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLazyLoop == false)
		{
			isLazyLoop = true;
			StartCoroutine(lazyUpdate());
		}
	}
	IEnumerator lazyUpdate() {
		//Debug.Log("Entering lazyUpdate");
        yield return new WaitForSeconds(.5f);
		List<GameObject> addedColliders = new List<GameObject>();
		foreach(Collider col in Physics.OverlapSphere(transform.position, detectionRadius))
		{
			
			GameObject obj = col.transform.root.gameObject;
			if (obj.tag == "Rock")
			{
				if (obj.GetComponent<ObjectProperties>() == null)
				{
					//Debug.Log("ObjectProperties is equal to null");
				}
				else
				{
					obj.GetComponent<ObjectProperties>().enabled = true;
				}
				
				///.enabled = true;
				//obj.AddComponent<ObjectProperties>();
				//obj.GetComponent<ObjectProperties>();
				//Debug.Log("We have the object " + obj.name + " in the sphere");
				//We've got a rock.
				if (rocks.Contains(obj) == false)
				{
					addedColliders.Add(obj);
                    //If we haven't added that rock.
                    //obj.GetComponent<Rigidbody>().WakeUp();
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    //obj.GetComponent<Rigidbody>().detectCollisions = true;
                }
			}
			
			//if(obj.GetComponent<ObjectProperties>() != null)
			//{
				
			//}
		}
		var firstNotSecond = rocks.Except(addedColliders).ToList();
		//var secondNotFirst = addedColliders.Except(rocks).ToList();
		foreach(GameObject obj in firstNotSecond)
		{
			Rigidbody rb = obj.GetComponent<Rigidbody>();
			//rb.detectCollisions = false;
			rb.Sleep();
		}
		isLazyLoop = false;
    }

}
