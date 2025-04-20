using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tCout : MonoBehaviour{

    public Text txt;
    public int toques;

    // Update is called once per frame
    void Update(){
        if(Input.touchCount > 0)
        {
            toques += Input.touchCount;
            txt.text = Input.touchCount.ToString();
        }
    }
}
