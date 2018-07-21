﻿using UnityEngine;
using System.Collections;

namespace S3
{
	public class Enemy_NavDestinationReached : MonoBehaviour {

        private Enemy_Master enemyMaster;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;

        void OnEnable()
		{
            SetInitialReferences();
            enemyMaster.EventEnemyDie += DisableThis;
		}

		void OnDisable()
		{
            enemyMaster.EventEnemyDie -= DisableThis;
        }

		void Update () 
		{
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CheckIfDestinationReached();
            }
        }

		void SetInitialReferences()
		{
            enemyMaster = GetComponent<Enemy_Master>();
            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }
            checkRate = Random.Range(0.3f, 0.4f);
        }

        void CheckIfDestinationReached()
        {
            if (enemyMaster.isOnRoute)
            {
                if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
                {
                    enemyMaster.isOnRoute = false;
                    enemyMaster.CallEventEnemyReachedNavTarget();
                }
            }
        }

        void DisableThis()
        {
            this.enabled = false;
        }
	}
}


