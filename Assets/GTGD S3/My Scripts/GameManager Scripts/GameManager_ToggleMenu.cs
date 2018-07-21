using UnityEngine;
using System.Collections;

namespace S3
{
    public class GameManager_ToggleMenu : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;
        public GameObject menu;
		Component component;
		SteamVR_TrackedObject trackedObj;
		SteamVR_Controller.Device device;
		bool deviceExist = false;



        // Use this for initialization
        void Start()
        {
            ToggleMenu();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForMenuToggleRequest();
        }

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.GameOverEvent += ToggleMenu;
        }

        void OnDisable()
        {
            gameManagerMaster.GameOverEvent -= ToggleMenu;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void CheckForMenuToggleRequest()
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

            if (Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn)
            {
                ToggleMenu();
            }
			else if (deviceExist) {

				if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn)
				{
					ToggleMenu();
				}
			}
        }

        void ToggleMenu()
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
                gameManagerMaster.CallEventMenuToggle();
				ToggleLasserPointer ();

            }
            else
            {
                Debug.LogWarning("You need to assign a UI GameObject to the Toggle Menu script in the inspector.");
            }
        }

		void ToggleLasserPointer() 
		{
			GameObject playerObj = GameObject.FindWithTag("Player");

			if (playerObj != null) {
				/*
				GameObject inputObj = playerObj.transform.Find ("VRInputModule").gameObject;

				if (inputObj != null) {

					if (inputObj.activeSelf)
						inputObj.SetActive (false);
					else
						inputObj.SetActive (true);
				}
				*/

				GameObject inputObj = playerObj.transform.Find ("VivePointers").gameObject;

				if (inputObj != null) {
					Debug.Log ("found!!!");

					GameObject LeftObj = inputObj.transform.Find ("Left").gameObject;
					if (LeftObj != null) {


						if (!gameManagerMaster.isMenuOn)
							LeftObj.SetActive (false);
						else
							LeftObj.SetActive (true);
					}	
				}

			}
		}
    }
}

