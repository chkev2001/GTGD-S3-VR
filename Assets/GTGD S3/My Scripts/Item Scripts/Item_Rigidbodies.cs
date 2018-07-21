﻿using UnityEngine;
using System.Collections;

namespace S3
{
	public class Item_Rigidbodies : MonoBehaviour {

        private Item_Master itemMaster;
        public Rigidbody[] rigidBodies;

		void OnEnable()
		{
            SetInitialReferences();            
            itemMaster.EventObjectThrow += SetIsKinematicToFalse;
            itemMaster.EventObjectPickup += SetIsKinematicToTrue;
        }

		void OnDisable()
		{
            itemMaster.EventObjectThrow -= SetIsKinematicToFalse;
            itemMaster.EventObjectPickup -= SetIsKinematicToTrue;
        }

        void Start()
        {
            CheckIfStartsInInventory();
        }

		void SetInitialReferences()
		{
            itemMaster = GetComponent<Item_Master>();
            rigidBodies = GetComponentsInChildren<Rigidbody>();
		}

        void CheckIfStartsInInventory()
        {
            if (transform.root.CompareTag(GameManager_References._playerTag))
            {
                SetIsKinematicToTrue();
            }
        }

        void SetIsKinematicToTrue()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                {
                    rBody.isKinematic = true;
                }
            }
        }

        void SetIsKinematicToFalse()
        {
            if (rigidBodies.Length > 0)
            {
                foreach (Rigidbody rBody in rigidBodies)
                {
                    rBody.isKinematic = false;
                }
            }
        }
	}
}


