using UnityEngine;
using System.Collections;

namespace S3
{
	public class Item_MakeNoise : MonoBehaviour {

        public float noiseRange = 30;
        public float noiseRate = 10;
        public float speedThreshold = 5;
        private float nextNoiseTime;
        public LayerMask applicableNPCLayer;
        private Collider[] colliders;

        public string playerTag = "Player";
        private GameManager_NPCRelationsMaster npcRelationsMaster;

        void OnEnable()
        {
            SetInitialReferences();
            if (npcRelationsMaster != null)
            {
                npcRelationsMaster.EventUpdateNPCRelationsEverywhere += UpdateLayersToDistract;
            }
        }

        void OnDisable()
        {
            if (npcRelationsMaster != null)
            {
                npcRelationsMaster.EventUpdateNPCRelationsEverywhere -= UpdateLayersToDistract;
            }
        }

        void OnCollisionEnter()
        {
            if (Time.time > nextNoiseTime)
            {
                nextNoiseTime = Time.time + noiseRate;

                if (GetComponent<Rigidbody>().velocity.magnitude > speedThreshold)
                {
                    Distraction();
                }
            }
        }

        void SetInitialReferences()
        {
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


