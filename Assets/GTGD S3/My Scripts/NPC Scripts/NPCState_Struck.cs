using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPCState_Struck : NPCState_Interface {

        private readonly NPC_StatePattern npc;
        private float informRate = 0.5f;
        private float nextInform;
        private Collider[] colliders;
        private Collider[] friendlyColliders;

        public NPCState_Struck(NPC_StatePattern npcStatePattern)
        {
            npc = npcStatePattern;
        }


        public void UpdateState()
        {
            InformNearbyAlliesThatIHaveBeenHurt();
        }
        public void ToPatrolState() { }

        public void ToAlertState()
        {
            //npc.currentState = npc.alertState;
        }
        public void ToPursueState() { }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        void InformNearbyAlliesThatIHaveBeenHurt()
        {
            if (Time.time > nextInform)
            {
                nextInform = Time.time + informRate;
            }
            else
            {
                return;
            }

            if (npc.myAttacker != null)
            {
                friendlyColliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myFriendlyLayers);

                if (IsAttackerClose())
                {
                    AlertNearbyAllies();
                    SetMyselfToInvestigate();
                }
            }
        }

        bool IsAttackerClose()
        {
            if (Vector3.Distance(npc.transform.position, npc.myAttacker.position) <= npc.sightRange * 2)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        void AlertNearbyAllies()
        {
            foreach (Collider ally in friendlyColliders)
            {
                if (ally.transform.root.GetComponent<NPC_StatePattern>() != null)
                {
                    NPC_StatePattern allyPattern = ally.transform.root.GetComponent<NPC_StatePattern>();

                    if (allyPattern.currentState == allyPattern.patrolState)
                    {
                        allyPattern.pursueTarget = npc.myAttacker;
                        allyPattern.locationOfInterest = npc.myAttacker.position;
                        allyPattern.currentState = allyPattern.investigateHarmState;
                        allyPattern.npcMaster.CallEventNpcWalkAnim();
                    }
                }
            }
        }


        void SetMyselfToInvestigate()
        {
            npc.pursueTarget = npc.myAttacker;
            npc.locationOfInterest = npc.myAttacker.position;

            if (npc.capturedState == npc.patrolState)
            {
                npc.capturedState = npc.investigateHarmState;
            }
        }
    }
}


