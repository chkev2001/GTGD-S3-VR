using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPC_DropItems : MonoBehaviour {

        private NPC_Master npcMaster;
        public GameObject[] itemsToDrop;

        void OnEnable()
		{
            SetInitialReferences();
            npcMaster.EventNpcDie += DropItems;
        }

        void OnDisable()
		{
            npcMaster.EventNpcDie -= DropItems;
        }

        void SetInitialReferences()
		{
            npcMaster = GetComponent<NPC_Master>();
        }

        void DropItems()
        {
            if (itemsToDrop.Length > 0)
            {
                foreach (GameObject item in itemsToDrop)
                {
                    StartCoroutine(PauseBeforeDrop(item)); //Otherwise the event gets fired off before the Start method on Item Master can run.
                }
            }
        }

        IEnumerator PauseBeforeDrop(GameObject itemToDrop)
        {
            yield return new WaitForSeconds(0.05f);
            itemToDrop.SetActive(true);
            itemToDrop.transform.parent = null;
            yield return new WaitForSeconds(0.05f);
            if (itemToDrop.GetComponent<Item_Master>() != null)
            {
                itemToDrop.GetComponent<Item_Master>().CallEventObjectThrow();
            }
        }
    }
}


