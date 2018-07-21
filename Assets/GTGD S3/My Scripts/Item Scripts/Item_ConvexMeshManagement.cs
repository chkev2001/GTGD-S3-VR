using UnityEngine;
using System.Collections;

namespace S3
{
	public class Item_ConvexMeshManagement : MonoBehaviour {

        private Item_Master itemMaster;
        public MeshCollider[] meshColliders;
        public Rigidbody myRigidbody;
        public bool isSettled = true;
        private float checkRate = 0.2f;
        private float nextCheck;

		void OnEnable()
		{
            SetInitialReferences();
            itemMaster.EventObjectPickup += EnableMeshConvex;
		}

		void OnDisable()
		{
            itemMaster.EventObjectPickup -= EnableMeshConvex;
        }

		void Update () 
		{
            CheckIfIHaveSettled();
		}

		void SetInitialReferences()
		{
            itemMaster = GetComponent<Item_Master>();
		}

        void CheckIfIHaveSettled()
        {
            if (Time.time > nextCheck && !isSettled)
            {
                nextCheck = Time.time + checkRate;

                if (Mathf.Approximately(myRigidbody.velocity.magnitude, 0) &&
                    !myRigidbody.isKinematic)
                {
                    isSettled = true;
                    DisableMeshConvexAndEnableIsKinematic();
                }
            }
        }

        void EnableMeshConvex()
        {
            isSettled = false;

            if (meshColliders.Length > 0)
            {
                foreach (MeshCollider subMeshCollider in meshColliders)
                {
                    subMeshCollider.convex = true;
                }
            }
        }

        void DisableMeshConvexAndEnableIsKinematic()
        {
            myRigidbody.isKinematic = true;

            if (meshColliders.Length > 0)
            {
                foreach (MeshCollider subMeshCollider in meshColliders)
                {
                    subMeshCollider.convex = false;
                }
            }            
        }
	}
}


