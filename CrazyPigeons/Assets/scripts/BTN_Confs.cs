using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Confs : MonoBehaviour
{
public static BTN_Confs instance;
public bool liga = false;
public Animator animaConf,animaEngre;

public void ClickBTN()
{
    liga = !liga;

    if (liga)
    {
        animaConf.Play ("MOVE_UI");
        animaEngre.Play ("AnimaEngrenagem");
    }

    else
    {
        animaConf.Play ("MOVE_UI_invers");
        animaEngre.Play ("AnimaEngrenagemInvers");
    }


}


}
