using UnityEngine;
using System.Collections;

namespace S3
{
	public class VehicleCamera_FreeLook : MonoBehaviour {

        public Vector2 rotationRange = new Vector3(70, 70);
        public float rotationSpeed = 10;
        private VehicleCamera_Master vehicleCamMaster;
        private Transform targetTransform;
        private Vector3 targetAngles;
        private Quaternion capturedRotation;
        private bool isInFreeLook;
        private float inputH;
        private float inputV;

        
        void OnEnable()
		{
            SetInitialReferences();
            vehicleCamMaster.EventAssignCameraTarget += AssignTarget;
		}

		void OnDisable()
		{
            vehicleCamMaster.EventAssignCameraTarget -= AssignTarget;
        }

        void Update()
        {
            FreeLookRotation();
        }
	
		void FixedUpdate () 
		{
            FollowTargetRotation();
		}

		void SetInitialReferences()
		{
            vehicleCamMaster = GetComponent<VehicleCamera_Master>();
        }

        void FreeLookRotation()
        {
            if (Input.GetMouseButton(0) && Time.timeScale > 0)
            {
                if (targetTransform == null)
                {
                    return;
                }

                isInFreeLook = true;
                transform.rotation = capturedRotation;
               
                inputH = Input.GetAxis("Mouse X");
                inputV = Input.GetAxis("Mouse Y");           

                if (targetAngles.y > 180)
                {
                    targetAngles.y -= 360;
                }
                if (targetAngles.x > 180)
                {
                    targetAngles.x -= 360;
                }
                if (targetAngles.y < -180)
                {
                    targetAngles.y += 360;
                }
                if (targetAngles.x < -180)
                {
                    targetAngles.x += 360;
                }
                
                targetAngles.y += inputH * rotationSpeed;
                targetAngles.x += inputV * rotationSpeed;

                targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
                targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);
                
                transform.rotation = Quaternion.Euler(-targetAngles.x, targetAngles.y, 0);
            }

            else
            {
                isInFreeLook = false;
            }
        }

        void AssignTarget(Transform targ)
        {
            targetTransform = targ;
        }

        void FollowTargetRotation()
        {
            if (targetTransform != null && !isInFreeLook)
            {
                capturedRotation = Quaternion.LookRotation(targetTransform.forward, Vector3.up);

                transform.rotation = Quaternion.Lerp(transform.rotation, capturedRotation, 5 * Time.deltaTime);
                targetAngles = capturedRotation.eulerAngles;
            }
        }
	}
}


