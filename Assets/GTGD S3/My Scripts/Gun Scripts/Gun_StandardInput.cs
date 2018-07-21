using UnityEngine;
using System.Collections;

namespace S3
{
	public class Gun_StandardInput : MonoBehaviour {

        private Gun_Master gunMaster;
        private float nextAttack;
        public float attackRate = 0.5f;
        private Transform myTransform;
        public bool isAutomatic;
        public bool hasBurstFire;
        private bool isBurstFireActive;
        public string attackButtonName;
        public string reloadButtonName;
        public string burstFireButtonName;
		Component[] components;
		SteamVR_TrackedObject trackedObj;
		SteamVR_Controller.Device device;

		void Start () 
		{
            SetInitialReferences();
		}
	
		void Update () 
		{
			if (myTransform.root.CompareTag(GameManager_References._playerTag))
				device = SteamVR_Controller.Input ((int)trackedObj.index);
			CheckIfWeaponShouldAttack();
			CheckForBurstFireToggle();
			CheckForReloadRequest();

		}

		void SetInitialReferences()
		{
			gunMaster = GetComponent<Gun_Master> ();
			myTransform = transform;

			gunMaster.isGunLoaded = true; //So the player can attempt shooting right away.

			if (myTransform.root.CompareTag (GameManager_References._playerTag)) {
				
				components = GetComponentsInParent (typeof(SteamVR_TrackedObject));
				if (components != null) {
					trackedObj = (SteamVR_TrackedObject)components [0];
				}
			}
		}

        void CheckIfWeaponShouldAttack()
        {
            if (Time.time > nextAttack && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {

                if (isAutomatic && !isBurstFireActive)
                {
                    if (Input.GetButton(attackButtonName))
                    {
                        //Debug.Log("Full Auto");
                        AttemptAttack();
                    }
					else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
					{
						//Debug.Log("Full Auto");
						AttemptAttack();
					}

                }

                else if (isAutomatic && isBurstFireActive)
                {
                    if (Input.GetButtonDown(attackButtonName))
                    {
                        //Debug.Log("Burst");
                        StartCoroutine(RunBurstFire());
                    }
					else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
					{
						//Debug.Log("Burst");
						StartCoroutine(RunBurstFire());

					}
                }

                else if (!isAutomatic)
                {
                    if (Input.GetButtonDown(attackButtonName))
                    {
                        AttemptAttack();
                    }
					else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
					{
						AttemptAttack();
					}
                }
            }
        }

        void AttemptAttack()
        {
            nextAttack = Time.time + attackRate;

            if (gunMaster.isGunLoaded)
            {
                //Debug.Log("Shooting");
                gunMaster.CallEventPlayerInput();
            }
            else
            {
                gunMaster.CallEventGunNotUsable();
            }

        }

        void CheckForReloadRequest()
        {
            if (Input.GetButtonDown(reloadButtonName) && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                gunMaster.CallEventRequestReload();
            }
			else if (myTransform.root.CompareTag(GameManager_References._playerTag) && device.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && Time.timeScale > 0
				)
			{
				gunMaster.CallEventRequestReload();
			}
        }

        void CheckForBurstFireToggle()
        {
            if (Input.GetButtonDown(burstFireButtonName) && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                //Debug.Log("Burst Fire Toggled");
                isBurstFireActive = !isBurstFireActive;
                gunMaster.CallEventToggleBurstFire();
            }
        }

        IEnumerator RunBurstFire()
        {
            AttemptAttack();
            yield return new WaitForSeconds(attackRate);
            AttemptAttack();
            yield return new WaitForSeconds(attackRate);
            AttemptAttack();
        }
	}
}


