using UnityEngine;
using System.Collections;

namespace S3
{
	public class Item_Name : MonoBehaviour {

        public string itemName;

		void Start()
		{
            SetItemName();
		}

        void SetItemName()
        {
            transform.name = itemName;
        }
	}
}


