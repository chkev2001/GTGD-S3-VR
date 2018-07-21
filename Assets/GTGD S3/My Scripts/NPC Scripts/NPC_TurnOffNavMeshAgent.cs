using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPC_TurnOffNavMeshAgent : MonoBehaviour {

        private NPC_Master npcMaster;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;

        void OnEnable()
		{
            SetInitialReferences();
            npcMaster.EventNpcDie += TurnOffNavMeshAgent;
        }

        void OnDisable()
		{
            npcMaster.EventNpcDie -= TurnOffNavMeshAgent;
        }

		void SetInitialReferences()
		{
            npcMaster = GetComponent<NPC_Master>();

            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }
        }

        void TurnOffNavMeshAgent()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }
        }

    }
}


