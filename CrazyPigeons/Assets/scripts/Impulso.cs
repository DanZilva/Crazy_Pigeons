using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{

    private Rigidbody2D passaro;



    // Start is called before the first frame update
    void Start()
    {
       passaro = GetComponent<Rigidbody2D> (); 
       passaro.AddForce (new Vector2(Random.Range(5,11),Random.Range(5,11)),ForceMode2D.Impulse);
    }

   
}
