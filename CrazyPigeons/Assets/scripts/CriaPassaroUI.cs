using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriaPassaroUI : MonoBehaviour
{
    public GameObject[] passaros;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TiroPassaro",2F,2F);
    }


    void TiroPassaro()
    {
        Instantiate(passaros[Random.Range(0,3)],transform.position,Quaternion.identity);
    }
    


}
