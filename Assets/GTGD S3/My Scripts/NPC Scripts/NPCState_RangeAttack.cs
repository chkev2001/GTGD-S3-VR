using UnityEngine;
using System.Collections;

namespace S3
{
	public class NPCState_RangeAttack : NPCState_Interface {

        private readonly NPC_StatePattern npc;
        private RaycastHit hit;

        public NPCState_RangeAttack(NPC_StatePattern npcStatePattern)
        {
            npc = npcStatePattern;
        }

        public void UpdateState()
        {
            Look();
            TryToAttack();
        }

        public void ToPatrolState()
        {
            KeepWalking();
            npc.pursueTarget = null;
            npc.currentState = npc.patrolState;
        }

        public void ToAlertState()
        {
            KeepWalking();
            npc.currentState = npc.alertState;
        }

        public void ToPursueState()
        {
            KeepWalking();
            npc.currentState = npc.pursueState;
        }

        public void ToMeleeAttackState()
        {
            npc.currentState = npc.meleeAttackState;
        }
        public void ToRangeAttackState() { }

        void Look()
        {
            if (npc.pursueTarget == null)
            {
                ToPatrolState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

            if (colliders.Length == 0)
            {
                ToPatrolState();
                return;
            }

            foreach (Collider col in colliders)
            {
                if (col.transform.root == npc.pursueTarget)
                {
                    TurnTowardsTarget();
                    return;
                }
            }

            ToPatrolState();
        }

        void TryToAttack()
        {
            if (npc.pursueTarget != null)
            {
                npc.meshRendererFlag.material.color = Color.cyan;

                if (!IsTargetInSight())
                {
                    ToPursueState();
                    return;
                }

                if (Time.time > npc.nextAttack)
                {
                    npc.nextAttack = Time.time + npc.attackRate;

                    float distanceToTarget = Vector3.Distance(npc.transform.position, npc.pursueTarget.position);

                    TurnTowardsTarget();

                    if (distanceToTarget <= npc.rangeAttackRange)
                    {
                        StopWalking();

                        if (npc.rangeWeapon.GetComponent<Gun_Master>() != null)
                        {
                            npc.rangeWeapon.GetComponent<Gun_Master>().CallEventNpcInput(npc.rangeAttackSpread);
                            return;
                        }
                    }

                    if (distanceToTarget <= npc.meleeAttackRange && npc.hasMeleeAttack)
                    {
                        ToMeleeAttackState();
                    }
                }
            }

            else
            {
                ToPatrolState();
            }
        }

        void TurnTowardsTarget()
        {
            Vector3 newPos = new Vector3(npc.pursueTarget.position.x, npc.transform.position.y, npc.pursueTarget.position.z);
            npc.transform.LookAt(newPos);
        }


        void KeepWalking()
        {
            if (npc.myNavMeshAgent.enabled)
            {
                npc.myNavMeshAgent.Resume();
                npc.npcMaster.CallEventNpcWalkAnim();
            }
        }

        void StopWalking()
        {
            if (npc.myNavMeshAgent.enabled)
            {
                npc.myNavMeshAgent.Stop();
                npc.npcMaster.CallEventNpcIdleAnim();
            }
        }

        bool IsTargetInSight()
        {
            RaycastHit hit;

            Vector3 weaponLookAtVector = new Vector3(npc.pursueTarget.position.x, npc.pursueTarget.position.y + npc.offset, npc.pursueTarget.position.z);
            npc.rangeWeapon.transform.LookAt(weaponLookAtVector);

            if (Physics.Raycast(npc.rangeWeapon.transform.position, npc.rangeWeapon.transform.forward, out hit))
            {
                foreach (string tag in npc.myEnemyTags)
                {
                    if (hit.transform.root.CompareTag(tag))
                    {
                        return true;
                    }
                }
                return false;
            }

            else
            {
                return false;
            }
        }
    }
}


