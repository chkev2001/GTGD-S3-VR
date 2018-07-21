using UnityEngine;
using System.Collections;

namespace S3
{
	public class VehicleCamera_Follow : MonoBehaviour {

        private VehicleCamera_Master vehicleCamMaster;
        private Transform targetTransform;

		void OnEnable()
		{
            SetInitialReferences();
            vehicleCamMaster.EventAssignCameraTarget += AssignTarget;
		}

		void OnDisable()
		{
            vehicleCamMaster.EventAssignCameraTarget -= AssignTarget;
        }
	
		void FixedUpdate () 
		{
            StayWithTarget();
		}

		void SetInitialReferences()
		{
            vehicleCamMaster = GetComponent<VehicleCamera_Master>();
		}

        void AssignTarget(Transform targ)
        {
            targetTransform = targ;
        }

        void StayWithTarget()
        {
            if (targetTransform == null)
            {
                return;
            }

            transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * 5);
        }
    }
}


