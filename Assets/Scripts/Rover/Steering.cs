using UnityEngine;
using System.Collections;

namespace NewtonVR
{
	public class Steering : NVRInteractableRotator
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
		protected override void Awake ()
		{
			base.Awake ();
		}

		public override void OnNewPosesApplied ()
		{
			base.OnNewPosesApplied ();
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
			this.Rigidbody.isKinematic = false;
		}
	}
}
