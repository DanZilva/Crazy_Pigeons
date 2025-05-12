using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ONDEESTOU : MonoBehaviour
{

    public static ONDEESTOU instance;

    public int fase = -1;
    public string faseN;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad (this.gameObject);
        }

        else
        {
            Destroy (gameObject);
        }


        SceneManager.sceneLoaded += VerificaFase;

    }

    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene ().buildIndex;
        faseN = SceneManager.GetActiveScene ().name;
    }

  
}
