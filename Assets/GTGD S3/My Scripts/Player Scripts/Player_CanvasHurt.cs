using UnityEngine;
using System.Collections;

namespace S3
{
	public class Player_CanvasHurt : MonoBehaviour {

        public GameObject hurtCanvas;
        private Player_Master playerMaster;
        private float secondsTillHide = 2;

		void OnEnable()
		{
            SetInitialReferences();
            HideHurtCanvas();
            playerMaster.EventPlayerHealthDeduction += TurnOnHurtEffect;
		}

		void OnDisable()
		{
            playerMaster.EventPlayerHealthDeduction -= TurnOnHurtEffect;
        }

		void SetInitialReferences()
		{
            playerMaster = GetComponent<Player_Master>();
		}

        void TurnOnHurtEffect(int damage)
        {
            if (hurtCanvas != null)
            {

                if (damage >= 1)
                {
                    StopAllCoroutines();
                    hurtCanvas.SetActive(true);
                    StartCoroutine(ResetHurtCanvas());
                }
            }
        }

        IEnumerator ResetHurtCanvas()
        {
            yield return new WaitForSeconds(secondsTillHide);
            HideHurtCanvas();
        }

        void HideHurtCanvas()
        {
            hurtCanvas.SetActive(false);
        }
	}
}


