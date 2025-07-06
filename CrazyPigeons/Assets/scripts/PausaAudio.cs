using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausaAudio : MonoBehaviour
{

    public UnityEngine.UI.Image btn;

    // Update is called once per frame
    void Update()
    {
        if (AUDIOMANAGER.instance.pause == 1)
        {
            btn.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
        else
        {
            btn.color = new Color(1, 1, 1, 1);
        }

    }

    public void PauseSom()
    {
        AUDIOMANAGER.instance.pause *= -1;
    }
}
