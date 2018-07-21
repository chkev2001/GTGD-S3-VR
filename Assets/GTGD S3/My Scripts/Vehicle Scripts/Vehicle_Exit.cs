using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

namespace S3
{
	public class Vehicle_Exit : MonoBehaviour {

        private Vehicle_Master vehicleMaster;
        private Destructible_Master destructibleMaster;

		void OnEnable()
		{
            SetInitialReferences();
            vehicleMaster.EventExitVehicle += ExitVehicle;

            if (destructibleMaster != null)
            {
                destructibleMaster.EventDestroyMe += ExitVehicle;
            }
		}

		void OnDisable()
		{
            vehicleMaster.EventExitVehicle -= ExitVehicle;

            if (destructibleMaster != null)
            {
                destructibleMaster.EventDestroyMe -= ExitVehicle;
            }
        }

		void Start () 
		{
            this.enabled = false;
		}
	
		void Update () 
		{
            ExitVehicleRequest();
		}

		void SetInitialReferences()
		{
            vehicleMaster = GetComponent<Vehicle_Master>();

            if (GetComponent<Destructible_Master>() != null)
            {
                destructibleMaster = GetComponent<Destructible_Master>();
            }
        }

        void ExitVehicle()
        {
            RemoveDriverFromVehicle();
            TurnOffCamera();
            DisableVehicleControlScript();
            TurnOnNavMeshObstacle();
            ApplyTagAndLayerToVehicle();
            ReleasePassengers();

            this.enabled = false;
        }

        void ExitVehicleRequest()
        {
            if (Input.GetButtonDown(vehicleMaster.exitButton) && Time.timeScale > 0)
            {
                vehicleMaster.CallEventExitVehicle();
            }
        }

        void TurnOffCamera()
        {
            if (vehicleMaster.vehicleCamera == null)
            {
                return;
            }

            vehicleMaster.vehicleCamera.SetActive(false);
        }

        void RemoveDriverFromVehicle()
        {
            vehicleMaster.driver.transform.parent = null;
            vehicleMaster.driver.SetActive(true);
            vehicleMaster.driver = null;
            vehicleMaster.isVehicleOccupied = false;
        }

        void DisableVehicleControlScript()
        {
            if (GetComponent<CarUserControl>() != null)
            {
                GetComponent<CarUserControl>().enabled = false;
            }

            if (GetComponent<CarController>() != null)
            {
                GetComponent<CarController>().Move(0, 0, 0, 1);
                GetComponent<CarController>().enabled = false;
            }
        }

        void TurnOnNavMeshObstacle()
        {
            if (GetComponent<UnityEngine.AI.NavMeshObstacle>() != null)
            {
                GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
            }            
        }

        void ApplyTagAndLayerToVehicle()
        {
            gameObject.tag = vehicleMaster.defaultTag;
            gameObject.layer = vehicleMaster.defaultLayerNumber;
        }

        void ReleasePassengers()
        {
            if (vehicleMaster.cabin.childCount > 0)
            {
                vehicleMaster.cabin.DetachChildren();
            }
        }
    }
}


