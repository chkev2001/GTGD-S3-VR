using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPC_TurnOffStatePattern : MonoBehaviour {

        private NPC_Master npcMaster;
        private NPC_StatePattern npcPattern;

        void OnEnable()
		{
            SetInitialReferences();
            npcMaster.EventNpcDie += TurnOffStatePattern;
        }

        void OnDisable()
		{
            npcMaster.EventNpcDie -= TurnOffStatePattern;
        }

		void SetInitialReferences()
		{
            npcMaster = GetComponent<NPC_Master>();

            if (GetComponent<NPC_StatePattern>() != null)
            {
                npcPattern = GetComponent<NPC_StatePattern>();
            }
        }

        void TurnOffStatePattern()
        {
            if (npcPattern != null)
            {
                npcPattern.enabled = false;
            }
        }

    }
}


