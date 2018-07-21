using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class GameManager_CursorToggle : MonoBehaviour
    {
        private bool isCursorLocked;

		Component component;
		SteamVR_TrackedObject trackedObj;
		SteamVR_Controller.Device device;
		bool deviceExist = false;


        void Start()
        {
            ToggleCursorState();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForInput();
            CheckIfCursorShouldBeLocked();
        }

        void ToggleCursorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        void CheckForInput()
        {

			GameObject controllerLft = GameObject.FindWithTag("controller_left");
			if (controllerLft != null){

				component = controllerLft.GetComponent(typeof(SteamVR_TrackedObject));
				if (component != null) {
					trackedObj = (SteamVR_TrackedObject)component;
					device = SteamVR_Controller.Input ((int)trackedObj.index);
					deviceExist = true;
				}
			}else
				deviceExist = false;
			
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleCursorState();
            }
			else if (deviceExist) {
				if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
				{
					ToggleCursorState();
				}
			}
        }

        void CheckIfCursorShouldBeLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}


