using UnityEngine;
using System.Collections;

namespace S3
{
	public class Destructible_NPCRelation : MonoBehaviour {

        private GameManager_NPCRelationsMaster npcRelationsMaster;
        public int relationChangeOnDestroy = -50;
        public string factionAffected;
        public bool applyRelationChainEffect = true;
        private string factionInstigating;

		void Start () 
		{
            SetInitialReferences();
		}

        void OnDestroy()
        {
            ApplyRelationChangeOnDestruction();
        }

		void SetInitialReferences()
		{
            if (GameObject.Find("GameManager").GetComponent<GameManager_NPCRelationsMaster>() != null)
            {
                npcRelationsMaster = GameObject.Find("GameManager").GetComponent<GameManager_NPCRelationsMaster>();
            }
        }

        public void SetMyAttacker(Transform attacker)
        {
            factionInstigating = attacker.tag;
        }

        void ApplyRelationChangeOnDestruction()
        {
            if (factionInstigating == null)
            {
                return;
            }

            npcRelationsMaster.CallEventNPCRelationChange(factionAffected, factionInstigating, relationChangeOnDestroy, applyRelationChainEffect);
        }
    }
}


