using UnityEngine;
using System.Collections;

namespace NewtonVR
{
	public class Steering : NVRInteractable
	{
		public GameObject wheel;
		public float maximumSteeringAngle = 200f;
		public float wheelReleasedSpeed = 200f;
		public float wheelAngle = 0f;
		public float wheelPrevAngle = 0f;
		public bool wheelHeld = false;

		protected virtual float DeltaMagic { get { return 2f; } }

		protected Transform InitialAttachPoint;

		// Use this for initialization
		protected override void Start ()
		{
	
		}

		public override void OnNewPosesApplied ()
		{
			base.OnNewPosesApplied ();

			if (IsAttached == true) {
				Vector3 PositionDelta = (AttachedHand.transform.position - InitialAttachPoint.position) * DeltaMagic;

				this.Rigidbody.AddForceAtPosition (PositionDelta, InitialAttachPoint.position, ForceMode.VelocityChange);
			}
		}
	
		// Update is called once per frame
		protected override void Update ()
		{
			//reset rotation after wheel is released
			if (!wheelHeld) {
				float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
				if (Mathf.Abs (deltaAngle) > Mathf.Abs (wheelAngle)) {
					wheelAngle = 0f;
				} else if (wheelAngle > 0f) {
					wheelAngle -= deltaAngle;
				} else {
					wheelAngle += deltaAngle;
				}
			}
		}

		public override void BeginInteraction (NVRHand hand)
		{
			base.BeginInteraction (hand);
			InitialAttachPoint = new GameObject (string.Format ("[{0}] InitialAttachPoint", this.gameObject.name)).transform;
			InitialAttachPoint.position = hand.transform.position;
			InitialAttachPoint.rotation = hand.transform.rotation;
			InitialAttachPoint.localScale = Vector3.one * 0.25f;
			InitialAttachPoint.parent = this.transform;
			wheelHeld = true;
		}

		public override void EndInteraction ()
		{
			base.EndInteraction ();
			if (InitialAttachPoint != null)
				Destroy (InitialAttachPoint.gameObject);
		}
	}
}
