using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MOSTRA_ESTRELAS_PONTOS : MonoBehaviour
{


    private Text estrelas, estrelas2;
    private Text pontos, pontos2;

    private int[] estrelasVal;
    private int[] pontosVal;


    // Start is called before the first frame update
    void Awake()
    {
        ZPlayerPrefs.Initialize("12345678", "crazypigeongame");

        estrelasVal = new int[2];
        pontosVal = new int[2];

        for (int a = 0; a < 2; a++)
        {
            for (int x = 0; x <= ZPlayerPrefs.GetInt("FasesNumMestra" + (a + 1)); x++)
            {

                estrelasVal[a] += ZPlayerPrefs.GetInt("Level" + x + "_Mestra" + (a + 1) + "estrelas");
                ZPlayerPrefs.SetInt("Mestra" + (a + 1) + "Star", estrelasVal[a]);

                pontosVal[a] += ZPlayerPrefs.GetInt("Level" + x + "_Mestra" + (a + 1) + "bestMestra" + (a + 1));
                ZPlayerPrefs.SetInt("Mestra" + (a + 1) + "p", pontosVal[a]);


            }
        }

        estrelas = GameObject.FindWithTag("textstar").GetComponent<Text>();
        estrelas2 = GameObject.FindWithTag("textstar2").GetComponent<Text>();

        estrelas.text = (ZPlayerPrefs.GetInt("Mestra1Star").ToString());
        estrelas2.text = (ZPlayerPrefs.GetInt("Mestra2Star").ToString());

        pontos = GameObject.FindWithTag("textPontos").GetComponent<Text>();
        pontos2 = GameObject.FindWithTag("textPontos2").GetComponent<Text>();

        pontos.text = (ZPlayerPrefs.GetInt("Mestra1p").ToString());
        pontos2.text = (ZPlayerPrefs.GetInt("Mestra2p").ToString());



    }
}
