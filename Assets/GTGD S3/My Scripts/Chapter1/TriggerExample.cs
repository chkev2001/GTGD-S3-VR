using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class TriggerExample : MonoBehaviour
    {
        private GameManager_EventMaster eventMasterScript;


        void Start()
        {
            SetInitialReferences();
        }

        void OnTriggerEnter(Collider other)
        {
            eventMasterScript.CallMyGeneralEvent();
            Destroy(gameObject);
        }


        void SetInitialReferences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }
    }
}


