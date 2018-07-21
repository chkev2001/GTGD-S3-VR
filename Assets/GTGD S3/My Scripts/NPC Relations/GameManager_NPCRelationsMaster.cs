using UnityEngine;
using System.Collections;

namespace S3
{
    public class GameManager_NPCRelationsMaster : MonoBehaviour
    {

        public delegate void NPCRelationChangeEventHandler(string factAffected, string factCausing, int adjustment, bool chain);
        public event NPCRelationChangeEventHandler EventNPCRelationChange;

        public delegate void UpdateNPCRelationsEventHandler();
        public event UpdateNPCRelationsEventHandler EventUpdateNPCRelationsEverywhere;

        public int hostileThreshold = 40;
        public NPCRelationsArray[] npcRelationsArray;

        public void CallEventNPCRelationChange(string factionAffected, string factionCausingChange, int relationChangeAmount, bool applyChainEffect)
        {
            if (EventNPCRelationChange != null)
            {
                EventNPCRelationChange(factionAffected, factionCausingChange, relationChangeAmount, applyChainEffect);
            }
        }

        public void CallEventUpdateNPCRelationsEverywhere()
        {
            if (EventUpdateNPCRelationsEverywhere != null)
            {
                EventUpdateNPCRelationsEverywhere();
            }
        }
    }
}

