using UnityEngine;
using System.Collections;

namespace S3
{
	public class Melee_Strike : MonoBehaviour {

        private Melee_Master meleeMaster;
        //private float nextSwingTime;
        public int damage = 25;

		void Start () 
		{
            SetInitialReferences();
		}

        void OnCollisionEnter(Collision collision)
        {
            //if (collision.gameObject != GameManager_References._player &&
            //    meleeMaster.isInUse &&
            //    Time.time > nextSwingTime) //I commented this out so that the melee weapon can strike multiple objects in quick succession.
            if (collision.gameObject != GameManager_References._player &&
                meleeMaster.isInUse)
            {
                //nextSwingTime = Time.time + meleeMaster.swingRate;
                collision.transform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
                collision.transform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
                meleeMaster.CallEventHit(collision, collision.transform);
            }
        }

		void SetInitialReferences()
		{
            meleeMaster = GetComponent<Melee_Master>();
		}
	}
}


