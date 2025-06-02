using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingInfo : MonoBehaviour
{

    public Text txtCarregando;

    public void BtnClick(string s)
    {
        StartCoroutine (LoadGameProg(s));
    }

    IEnumerator LoadGameProg(string val)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(val);
        while (!async.isDone)
        {
            txtCarregando.enabled = true;
            yield return null;
        }
    }



 
}
