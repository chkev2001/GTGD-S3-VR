using UnityEngine;
using System.Collections;

namespace S3
{
	public class Screenshot : MonoBehaviour {

        private int counter;

	
		void Update () 
		{
            if (Input.GetKeyUp(KeyCode.RightBracket))
            {
                TakeScreenShot();
            }
		}

        void TakeScreenShot()
        {
            ScreenCapture.CaptureScreenshot("Screenshot " + counter.ToString()+ ".png");
            counter++;
        }

	}
}


