using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPC_CollisionField : MonoBehaviour {

        private NPC_Master npcMaster;
        private Rigidbody rigidBodyStrikingMe;
        private int damageToApply;
        public float massRequirement = 50;
        public float speedRequirement = 5;
        private float damageFactor = 0.1f;

        void OnEnable()
		{
            SetInitialReferences();
            npcMaster.EventNpcDie += DisableThis;
        }

        void OnDisable()
		{
            npcMaster.EventNpcDie -= DisableThis;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                rigidBodyStrikingMe = other.GetComponent<Rigidbody>();

                if (rigidBodyStrikingMe.mass >= massRequirement &&
                    rigidBodyStrikingMe.velocity.sqrMagnitude >= speedRequirement * speedRequirement)
                {
                    damageToApply = (int)(rigidBodyStrikingMe.mass * rigidBodyStrikingMe.velocity.magnitude * damageFactor);
                    npcMaster.CallEventNpcDeductHealth(damageToApply);
                }
            }
        }

        void SetInitialReferences()
		{
            npcMaster = transform.root.GetComponent<NPC_Master>();
        }

        void DisableThis()
        {
            gameObject.SetActive(false);
        }

    }
}


