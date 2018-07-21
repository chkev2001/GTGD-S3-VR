using UnityEngine;
using System.Collections;

namespace S3
{
	public class StayWithTransform : MonoBehaviour {

        public Transform targetTransform;
	
		void FixedUpdate () 
		{
            StayWithTarget();
		}

        void StayWithTarget()
        {
            if (targetTransform == null)
            {
                return;
            }

            transform.position = targetTransform.position;
        }
	}
}


