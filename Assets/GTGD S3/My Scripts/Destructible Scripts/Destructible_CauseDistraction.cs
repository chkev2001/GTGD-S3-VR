using UnityEngine;
using System.Collections;

namespace S3
{
	public class Destructible_CauseDistraction : MonoBehaviour {

        private Destructible_Master destructibleMaster;
        public float noiseRange = 60;
        public LayerMask applicableNPCLayer;
        private Collider[] colliders;

        public string playerTag = "Player";
        private GameManager_NPCRelationsMaster npcRelationsMaster;

        void OnEnable()
		{
            SetInitialReferences();
            destructibleMaster.EventDestroyMe += Distraction;

            if (npcRelationsMaster != null)
            {
                npcRelationsMaster.EventUpdateNPCRelationsEverywhere += UpdateLayersToDistract;
            }
        }

        void OnDisable()
		{
            destructibleMaster.EventDestroyMe -= Distraction;

            if (npcRelationsMaster != null)
            {
                npcRelationsMaster.EventUpdateNPCRelationsEverywhere -= UpdateLayersToDistract;
            }
        }

		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();

            if (GameObject.Find("GameManager").GetComponent<GameManager_NPCRelationsMaster>() != null)
            {
                npcRelationsMaster = GameObject.Find("GameManager").GetComponent<GameManager_NPCRelationsMaster>();
            }

            if (playerTag == "")
            {
                playerTag = "Player";
            }
        }

        void Distraction()
        {
            colliders = Physics.OverlapSphere(transform.position, noiseRange, applicableNPCLayer);

            if (colliders.Length == 0)
            {
                return;
            }

            foreach (Collider col in colliders)
            {
                col.transform.root.SendMessage("Distract", transform.position, SendMessageOptions.DontRequireReceiver);
            }
        }

        void UpdateLayersToDistract()
        {
            if (npcRelationsMaster == null)
            {
                return;
            }

            foreach (NPCRelationsArray npcArray in npcRelationsMaster.npcRelationsArray)
            {
                if (npcArray.npcFaction == playerTag)
                {
                    applicableNPCLayer = npcArray.myEnemyLayers;
                    break;
                }
            }
        }
    }
}


