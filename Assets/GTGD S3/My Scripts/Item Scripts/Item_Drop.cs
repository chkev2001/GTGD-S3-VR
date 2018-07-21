using UnityEngine;
using System.Collections;

namespace S3
{
	public class Item_Drop : MonoBehaviour {

        private Item_Master itemMaster;
        public string dropButtonName;
        private Transform myTransform;

		void Start () 
		{
            SetInitialReferences();
		}
	
		void Update () 
		{
            CheckForDropInput();
		}

		void SetInitialReferences()
		{
            itemMaster = GetComponent<Item_Master>();
            myTransform = transform;
		}

        void CheckForDropInput()
        {
            if (Input.GetButtonDown(dropButtonName) && Time.timeScale > 0 &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                myTransform.parent = null;
                itemMaster.CallEventObjectThrow();
            }
        }
	}
}


