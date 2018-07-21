using UnityEngine;
using System.Collections;

namespace S3
{
	public class Destructible_LowHealthEffect : MonoBehaviour {

        private Destructible_Master destructibleMaster;
        public GameObject[] lowHealthEffectGO;

		void OnEnable()
		{
            SetInitialReferences();
            destructibleMaster.EventHealthLow += TurnOnLowHealthEffect;
		}

		void OnDisable()
		{
            destructibleMaster.EventHealthLow -= TurnOnLowHealthEffect;
        }

		void SetInitialReferences()
		{
            destructibleMaster = GetComponent<Destructible_Master>();
		}

        void TurnOnLowHealthEffect()
        {
            if (lowHealthEffectGO.Length > 0)
            {
                for (int i = 0; i < lowHealthEffectGO.Length; i++)
                {
                    lowHealthEffectGO[i].SetActive(true);
                }
            }
        }
	}
}


