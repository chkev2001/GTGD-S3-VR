using UnityEngine;
using System.Collections;

namespace S3
{
	public class Destructible_CollisionDetection : MonoBehaviour {

        private Destructible_Master destructibleMaster;
        private Collider[] hitColliders;
        private Rigidbody myRigidbody;
        public float thresholdMass = 50;
        public float thresholdSpeed = 6;

        void Start () 
		{
            SetInitialReferences();
		}

        void OnCollisionEnter(Collision col)
        {
            if (col.contacts.Length > 0)
            {
                //Debug.Log("struck");
                if (col.contacts[0].otherCollider.GetComponent<Rigidbody>() != null)
                {
                    CollisionCheck(col.contacts[0].otherCollider.GetComponent<Rigidbody>());
                }
                else
                {
                    SelfSpeedCheck();
                }
            }
        }
	
		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();

            if (GetComponent<Rigidbody>() != null)
            {
                myRigidbody = GetComponent<Rigidbody>();
            }
		}

        void CollisionCheck(Rigidbody otherRigidbody)
        {
            if (otherRigidbody.mass > thresholdMass &&
                otherRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
            {
                //Debug.Log("struck2");
                int damage = (int)otherRigidbody.mass;
                //Debug.Log(transform.name + " " + damage.ToString());
                destructibleMaster.CallEventDeductHealth(damage);
            }

            else
            {
                SelfSpeedCheck();
            }
        }

        void SelfSpeedCheck()
        {
            if (myRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
            {
                int damage = (int)myRigidbody.mass;
               // Debug.Log(transform.name + " " + damage.ToString());
                destructibleMaster.CallEventDeductHealth(damage);
            }
        }
	}
}


