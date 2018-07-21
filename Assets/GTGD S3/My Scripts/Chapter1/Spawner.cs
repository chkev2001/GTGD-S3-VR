using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class Spawner : MonoBehaviour
    {
        public GameObject objectToSpawn;
        public int numberOfEnemies;
        private float spawnRadius = 5;
        private Vector3 spawnPosition;
        private GameManager_EventMaster eventMasterScript;

        void OnEnable()
        {
            SetInitialReferences();
            eventMasterScript.myGeneralEvent += SpawnObject;
        }

        void OnDisable()
        {
            eventMasterScript.myGeneralEvent -= SpawnObject;
        }


        void SpawnObject()
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }

        void SetInitialReferences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
        }
    }
}


