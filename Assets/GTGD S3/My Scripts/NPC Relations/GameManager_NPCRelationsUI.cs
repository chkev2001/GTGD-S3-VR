using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace S3
{
    public class GameManager_NPCRelationsUI : MonoBehaviour {

        private GameManager_NPCRelationsMaster npcRelationsMaster;
        private GameManager_Master gameManagerMaster;
        public Transform panelFactionRelations;

        public GameObject panelRelationsPrefab;
        public GameObject labelRelationsPrefab;
        
        void OnEnable()
        {
            SetInitialReferences();
            DrawUI();
            gameManagerMaster.MenuToggleEvent += DrawUI;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= DrawUI;
        }

        void SetInitialReferences()
        {
            npcRelationsMaster = GetComponent<GameManager_NPCRelationsMaster>();
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void DrawUI()
        {
            ClearUI();

            foreach (NPCRelationsArray npcArray in npcRelationsMaster.npcRelationsArray)
            {
                GameObject panelRelation = Instantiate(panelRelationsPrefab) as GameObject;
                panelRelation.transform.SetParent(panelFactionRelations, false);

                GameObject headerLabel = Instantiate(labelRelationsPrefab) as GameObject;
                headerLabel.transform.SetParent(panelRelation.transform, false);
                headerLabel.GetComponentInChildren<Text>().text = npcArray.npcFaction;

                foreach (NPCRelations npcRelation in npcArray.npcRelations)
                {
                    GameObject labelRelations = Instantiate(labelRelationsPrefab) as GameObject;
                    labelRelations.transform.SetParent(panelRelation.transform, false);
                    labelRelations.GetComponentInChildren<Text>().text = npcRelation.npcTag + " " + npcRelation.npcFactionRating.ToString();
                }
            }
        }

        void ClearUI()
        {
            if (panelFactionRelations.transform.childCount == 0)
            {
                return;
            }

            foreach (Transform panelRelation in panelFactionRelations)
            {
                Destroy(panelRelation.gameObject);
            }
        }
	}
}


