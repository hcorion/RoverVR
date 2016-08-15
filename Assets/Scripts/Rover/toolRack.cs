using UnityEngine;
using System.Collections;

public class toolRack : MonoBehaviour
{
    public NewtonVR.NVRHand leftHand;
    public NewtonVR.NVRHand rightHand;

    private GameObject curObject;
    private ToolProperties curObjectToolProps;
    
    //Use for lerping the tool to the attach point.
    public Transform toolLerpPoint;
    private bool isLerpingTool;
    private float realLerpTime;
    public float maxLerpTime = 0.1f;
    private Vector3 initialLerpPos;
    private Quaternion initialLerpRot;
    private Vector3 toolOffset;

    //Used for snapping the rotation;
    private Quaternion snapRotation;

    void Awake()
    {
        toolLerpPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (curObject != null)
        {
            if (leftHand.CurrentlyInteracting == curObject || rightHand.CurrentlyInteracting == curObject)
            {
                curObject.GetComponent<Rigidbody>().isKinematic = false;
                curObjectToolProps = null;
                curObject = null;

            }
            else
            {
                lerpTool();
            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (curObject == null)
        {
            GameObject newObj = other.transform.root.gameObject;
            if (newObj.GetComponent<ToolProperties>() != null)
            {
                if (leftHand.CurrentlyInteracting != newObj && rightHand.CurrentlyInteracting != newObj)
                {
                    //Used to check if it is already in another tool Rack.
                    if (newObj.GetComponent<Rigidbody>().isKinematic == true)
                    {
                        return;
                    }
                    Rigidbody newRigidbody = newObj.GetComponent<Rigidbody>();
                    newRigidbody.isKinematic = true;
                    curObject = newObj;
                    isLerpingTool = true;
                    initialLerpPos = curObject.transform.position;
                    initialLerpRot = curObject.transform.rotation;

                    //Figuring out the Rotation
                    curObjectToolProps = newObj.GetComponent<ToolProperties>();
                    if (curObjectToolProps.getTool() == "SelfieStick")
                    {
                        snapRotation = Quaternion.Euler(0, 0, 0);
                        toolOffset = new Vector3(0, -0.4f, 0);
                    }
                    else if (curObjectToolProps.getTool() == "NeutronDetector")
                    {
                        snapRotation = Quaternion.Euler(0, 0, -30);
                        toolOffset = new Vector3(-0.17f, .08f, -.02f);
                    }
                    else if (curObjectToolProps.getTool() == "ChemCam")
                    {
                        snapRotation = Quaternion.Euler(0, 0, -30);
                        toolOffset = new Vector3(-0.06f, .10f, 0);
                    }
                    else
                    {
                        Debug.Log("The object " + curObjectToolProps.getTool() + " has just been attached to the tool rack and isn't supported.");
                    }
                         
                }
            }
        }
    }
    void lerpTool()
    {
        if (isLerpingTool)
        {
            realLerpTime += Time.deltaTime;
            if (realLerpTime / maxLerpTime < 1)
            {
                curObject.transform.position = Vector3.Lerp(initialLerpPos, toolLerpPoint.position + toolOffset, realLerpTime / maxLerpTime);
                curObject.transform.rotation = Quaternion.Lerp(initialLerpRot, snapRotation, realLerpTime / maxLerpTime);
            }
            else
            {
                isLerpingTool = false;
                //curObject.transform.parent = transform.parent;
            }
        }
        else
        {
            curObject.transform.position = toolLerpPoint.position + toolOffset;
        }
    }
}