using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Exibe_e_Perde_Moedas : MonoBehaviour
{

    [SerializeField]
    private Text textMoeda;
    private int val;
    [SerializeField]
    private UnityEngine.UI.Button btnCompra;

    void Awake()
    {

        textMoeda = GetComponent<Text>();
        val = SCOREMANAGER.instance.LoadDados();
        textMoeda.text = val.ToString();


    }


    // Update is called once per frame
    void Update()
    {
        if (val >= 50)
        {
            btnCompra.interactable = true;
        }
        else
        {

            btnCompra.interactable = false;

        }

    }


    public void CompraSimula()
    {

        SCOREMANAGER.instance.PerdeMoedas(50);
        val = SCOREMANAGER.instance.LoadDados();
        textMoeda.text = val.ToString();


    }





}
