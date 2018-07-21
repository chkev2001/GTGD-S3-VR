using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

namespace S3
{
	public class Vehicle_Enter : MonoBehaviour {

        private Vehicle_Master vehicleMaster;

		void OnEnable()
		{
            SetInitialReferences();
            vehicleMaster.EventEnterVehicle += EnterVehicle;
		}

		void OnDisable()
		{
            vehicleMaster.EventEnterVehicle -= EnterVehicle;
        }

		void SetInitialReferences()
		{
            vehicleMaster = GetComponent<Vehicle_Master>();
		}

        void EnterVehicle(GameObject driver, string driverTag, LayerMask driverLayer)
        {
            SetCameraTarget();            
            PlaceDriverInVehicle(driver);
            ActivateVehicleControlScript();
            TurnOffNavMeshObstacle();
            ApplyTagAndLayerToVehicle(driverTag, driverLayer);
            EnableVehicleExitScript();
        }

        void SetCameraTarget()
        {
            if (vehicleMaster.vehicleCamera == null || vehicleMaster.cameraTarget == null)
            {
                return;
            }

            vehicleMaster.vehicleCamera.transform.rotation = transform.rotation;
            vehicleMaster.vehicleCamera.SetActive(true);

            if (vehicleMaster.vehicleCamera.GetComponent<VehicleCamera_Master>() != null)
            {
                vehicleMaster.vehicleCamera.GetComponent<VehicleCamera_Master>().CallEventAssignCameraTarget(vehicleMaster.cameraTarget);
            }
        }

        void PlaceDriverInVehicle(GameObject drvr)
        {
            drvr.SetActive(false);

            if (vehicleMaster.cabin == null)
            {
                Debug.LogWarning("Must assign cabin transform to Vehicle Master script");
                return;
            }

            drvr.transform.SetParent(vehicleMaster.cabin);
            vehicleMaster.driver = drvr;
            vehicleMaster.isVehicleOccupied = true;
        }

        void ActivateVehicleControlScript()
        {
            if (GetComponent<CarUserControl>() != null)
            {
                GetComponent<CarUserControl>().enabled = true;
            }

            if (GetComponent<CarController>() != null)
            {
                GetComponent<CarController>().enabled = true;
            }
        }

        void TurnOffNavMeshObstacle()
        {
            if (GetComponent<UnityEngine.AI.NavMeshObstacle>() != null)
            {
                GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
            }
        }

        void ApplyTagAndLayerToVehicle(string drvTag, LayerMask drvLayer)
        {
            gameObject.tag = drvTag;
            gameObject.layer = drvLayer;
        }

        void EnableVehicleExitScript()
        {
            if (GetComponent<Vehicle_Exit>() != null)
            {
                GetComponent<Vehicle_Exit>().enabled = true;
            }
        }
    }
}


