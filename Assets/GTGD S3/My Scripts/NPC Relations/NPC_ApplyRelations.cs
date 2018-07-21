using UnityEngine;
using System.Collections;

namespace S3
{
    public class NPC_ApplyRelations : MonoBehaviour
    {
        private GameManager_NPCRelationsMaster npcRelationsMaster;
        private NPC_StatePattern npcStatePattern;
        private NPC_Master npcMaster;        

        void OnEnable()
        {
            SetInitialReferences();
            npcRelationsMaster.EventUpdateNPCRelationsEverywhere += SetMyRelations;
            Invoke("SetMyRelations", 0.1f);
        }

        void OnDisable()
        {
            npcRelationsMaster.EventUpdateNPCRelationsEverywhere -= SetMyRelations;
        }

        void SetInitialReferences()
        {
            npcStatePattern = GetComponent<NPC_StatePattern>();
            npcMaster = GetComponent<NPC_Master>();

            GameObject gameManager = GameObject.Find("GameManager");
            npcRelationsMaster = gameManager.GetComponent<GameManager_NPCRelationsMaster>();
        }

        void SetMyRelations()
        {
            if (npcRelationsMaster.npcRelationsArray == null)
            {
                return;
            }

            foreach (NPCRelationsArray npcArray in npcRelationsMaster.npcRelationsArray)
            {
                if (transform.CompareTag(npcArray.npcFaction))
                {
                    npcStatePattern.myFriendlyLayers = npcArray.myFriendlyLayers;
                    npcStatePattern.myEnemyLayers = npcArray.myEnemyLayers;
                    npcStatePattern.myFriendlyTags = npcArray.myFriendlyTags;
                    npcStatePattern.myEnemyTags = npcArray.myEnemyTags;

                    ApplySightLayers(npcStatePattern.myFriendlyTags);
                    CheckThatMyFollowTargetIsStillAnAlly(npcStatePattern.myEnemyTags);

                    npcMaster.CallEventNPCRelationsChange();

                    break;
                }
            }
        }


        //So that allies of the NPC will not block their vision.
        void ApplySightLayers(string[] friendlyTags)
        {
            npcStatePattern.sightLayers = LayerMask.NameToLayer("Everything");

            if (friendlyTags.Length > 0)
            {
                foreach (string fTag in friendlyTags)
                {
                    int tempINT = LayerMask.NameToLayer(fTag);
                    npcStatePattern.sightLayers = ~(1 << tempINT | 1<< LayerMask.NameToLayer("Ignore Raycast"));
                }
            }
        }

        //For example, if the player becomes an enemy to this NPCs faction
        //then they should no longer follow the player as their leader.
        void CheckThatMyFollowTargetIsStillAnAlly(string[] enemyTags)
        {
            if (npcStatePattern.myFollowTarget == null)
            {
                return;
            }

            if (enemyTags.Length > 0)
            {
                foreach (string eTag in enemyTags)
                {
                    if (npcStatePattern.myFollowTarget.CompareTag(eTag))
                    {
                        npcStatePattern.myFollowTarget = null;
                        break;
                    }
                }
            }
        }
    }
}

