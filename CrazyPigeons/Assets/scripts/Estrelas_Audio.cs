using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrelas_Audio : MonoBehaviour
{

    public AudioSource aSource;
    public AudioClip clip;
    public string nomeEstrela;

    public void TocaAudioEstrela()
    {
        if (!aSource.isPlaying)
        {
            aSource.clip = clip;
            aSource.Play();
        }
    }

    public void Verifica_Star()
    {

        switch(nomeEstrela)
        {
            case "Estrela1_win":
            GAMEMANAGER.instance.estrela1fim = true;
            break;
        

    
            case "Estrela2_win":
            GAMEMANAGER.instance.estrela2fim = true;
            break;
        
        }

    }

}
