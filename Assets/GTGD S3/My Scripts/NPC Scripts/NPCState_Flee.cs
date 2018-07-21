using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPCState_Flee : NPCState_Interface {

        private Vector3 directionToEnemy;
        private UnityEngine.AI.NavMeshHit navHit;

        private readonly NPC_StatePattern npc;

        public NPCState_Flee(NPC_StatePattern npcStatePattern)
        {
            npc = npcStatePattern;
        }

        public void UpdateState()
        {
            CheckIfIShouldFlee();
            CheckIfIShouldFight();
        }

        public void ToPatrolState()
        {
            KeepWalking();
            npc.currentState = npc.patrolState;
        }
        public void ToAlertState() { }
        public void ToPursueState() { }
        public void ToMeleeAttackState()
        {
            KeepWalking();
            npc.currentState = npc.meleeAttackState;
        }
        public void ToRangeAttackState() { }

        void CheckIfIShouldFlee()
        {
            npc.meshRendererFlag.material.color = Color.gray;

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            if (colliders.Length == 0)
            {
                ToPatrolState();
                return;
            }

            directionToEnemy = npc.transform.position - colliders[0].transform.position;
            Vector3 checkPos = npc.transform.position + directionToEnemy;

            if (UnityEngine.AI.NavMesh.SamplePosition(checkPos, out navHit, 3.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                npc.myNavMeshAgent.destination = navHit.position;
                KeepWalking();
            }

            else
            {
                StopWalking();
            }
        }

        void CheckIfIShouldFight()
        {
            if (npc.pursueTarget == null)
            {
                return;
            }

            float distanceToTarget = Vector3.Distance(npc.transform.position, npc.pursueTarget.position);

            if (npc.hasMeleeAttack && distanceToTarget <= npc.meleeAttackRange)
            {
                ToMeleeAttackState();
            }
        }

        void KeepWalking()
        {
            npc.myNavMeshAgent.Resume();
            npc.npcMaster.CallEventNpcWalkAnim();
        }

        void StopWalking()
        {
            npc.myNavMeshAgent.Stop();
            npc.npcMaster.CallEventNpcIdleAnim();
        }

    }
}


