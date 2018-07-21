﻿using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class GrenadeExplosion : MonoBehaviour
    {
        private Collider[] hitColliders;
        private float destroyTime = 7;
        public float blastRadius;
        public float explosionPower;
        public LayerMask explosionLayers;

        void OnCollisionEnter(Collision col)
        {
            //Debug.Log(col.contacts[0].point.ToString());
            ExplosionWork(col.contacts[0].point);
            Destroy(gameObject);
        }

        void ExplosionWork(Vector3 explosionPoint)
        {
            hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);

            foreach (Collider hitCol in hitColliders)
            {
                if (hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    hitCol.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                }

                //Debug.Log(hitCol.gameObject.name);
                if (hitCol.GetComponent<Rigidbody>() != null)
                {
                    hitCol.GetComponent<Rigidbody>().isKinematic = false;
                    hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
                }

                if (hitCol.CompareTag("Enemy"))
                {
                    Destroy(hitCol.gameObject, destroyTime);
                }


            }
        }

    }
}

