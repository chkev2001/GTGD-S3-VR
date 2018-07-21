using UnityEngine;
using System.Collections;

namespace S3
{
	public class Player_CameraZoom : MonoBehaviour {

        private float minFOV = 7;
        private float maxFOV = 60;
        private float fieldOfView;
        public Camera firstPersonCharacter;
        public Camera weaponCamera;
        public float zoomSensitivity = 10;
        public bool isCameraZoomEnabled = true;

		void Start () 
		{
            SetInitialReferences();
		}
	
		void Update () 
		{
            CheckForMouseWheel();
        }

		void SetInitialReferences()
		{
            fieldOfView = maxFOV;

            if (firstPersonCharacter == null || weaponCamera == null)
            {
                Debug.LogWarning("Cameras not attached to Player_CameraZoom script.");
                return;
            }

            firstPersonCharacter.fieldOfView = maxFOV;
            weaponCamera.fieldOfView = maxFOV;
		}

        void CheckForMouseWheel()
        {
            if (firstPersonCharacter == null || weaponCamera == null || Time.timeScale == 0)
            {             
                return;
            }

            fieldOfView += Input.GetAxis("Mouse ScrollWheel") * -zoomSensitivity;
            fieldOfView = Mathf.Clamp(fieldOfView, minFOV, maxFOV);
            firstPersonCharacter.fieldOfView = fieldOfView;
            weaponCamera.fieldOfView = fieldOfView;
        }
	}
}


