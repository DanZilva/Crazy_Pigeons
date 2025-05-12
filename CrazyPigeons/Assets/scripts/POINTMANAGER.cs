using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POINTMANAGER : MonoBehaviour
{
    public static POINTMANAGER instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MelhorPontuacaoSave(string level, int pt)
    {
        if (!ZPlayerPrefs.HasKey(level + "best"))
        {
            ZPlayerPrefs.SetInt(level + "best", pt);
        }
        else
        {
            if (pt > ZPlayerPrefs.GetInt(level + "best"))
            {
                ZPlayerPrefs.SetInt(level + "best", pt);
            }
        }

        ZPlayerPrefs.Save();
    }

    public int MelhorPontuacaoLoad(string level)
    {
        if (ZPlayerPrefs.HasKey(level + "best"))
        {
            return ZPlayerPrefs.GetInt(level + "best");
        }
        else
        {
            return 0;
        }
    }
}
