using UnityEngine;
using System.Collections;

namespace S3
{
	public class Player_VehicleInteraction : MonoBehaviour {

        public Transform rayTransformPivot;
        public string buttonInteract = "PickUpItem";
        private RaycastHit hit;
        private float detectRadius = 0.7f;
        private float detectRange = 3;


		void Update () 
		{
            TryToEnterVehicle();
		}

        void TryToEnterVehicle()
        {
            if (Input.GetButtonDown(buttonInteract) && Time.timeScale > 0)
            {
                if (Physics.SphereCast(rayTransformPivot.position, detectRadius, rayTransformPivot.forward, out hit, detectRange))
                {
                    if (hit.transform.GetComponent<Vehicle_Master>() != null)
                    {
                        if (!hit.transform.GetComponent<Vehicle_Master>().isVehicleOccupied)
                        {
                            hit.transform.GetComponent<Vehicle_Master>().CallEventEnterVehicle(gameObject, gameObject.tag, gameObject.layer);
                        }                        
                    }
                }
            }
        }
	}
}


