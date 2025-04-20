using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{
    public Text txt;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    txt.text = "Began";
                    break;
                case TouchPhase.Ended:
                    txt.text = "Ended";
                    break;
                case TouchPhase.Moved:
                    txt.text = "Moved";
                    break;
                case TouchPhase.Stationary:
                    txt.text = "Stationary";    
                    break;
                case TouchPhase.Canceled:
                    txt.text = "Canceled";
                    break;
            }
        }
    }
}
