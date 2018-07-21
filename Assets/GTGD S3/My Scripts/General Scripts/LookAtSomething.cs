using UnityEngine;
using System.Collections;

namespace S3
{
	public class LookAtSomething : MonoBehaviour {

        public Transform objectToLookAt;
	
		void FixedUpdate () 
		{
            LookAtContinuously();
		}

        void LookAtContinuously()
        {
            if (objectToLookAt == null)
            {
                Debug.LogWarning("Object to look at not set on LookAtSomething script.");
                return;
            }

            Vector3 target = new Vector3(objectToLookAt.position.x, transform.position.y, objectToLookAt.position.z);
            transform.LookAt(target);
        }
	}
}


