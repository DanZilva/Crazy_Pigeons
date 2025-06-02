using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAnimaPorco : MonoBehaviour
{

    private Animator animacoes;
    private int limite = -1;
    public string[] clips;
    [SerializeField]
    private GameObject bomb, pontos1000;

    // Start is called before the first frame update
    void Start()
    {
        animacoes = GetComponent<Animator> ();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.magnitude > 4 && col.relativeVelocity.magnitude < 10)
        {
            if (limite < clips.Length - 1)
            {
                limite ++;
                animacoes.Play(clips[limite]);
            }
            else if (limite == clips.Length -1)
            {
                Instantiate (pontos1000 , new UnityEngine.Vector2 (transform.position.x, transform.position.y), Quaternion.identity);
                Instantiate (bomb , new UnityEngine.Vector2 (transform.position.x, transform.position.y), Quaternion.identity);
                GAMEMANAGER.instance.numPorcosCena -= 1;
                Destroy (gameObject);
            }
        }
        else if(col.relativeVelocity.magnitude > 12 && col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("clone"))
        {
            Instantiate (pontos1000 , new UnityEngine.Vector2 (transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate (bomb , new UnityEngine.Vector2 (transform.position.x, transform.position.y), Quaternion.identity);
            GAMEMANAGER.instance.numPorcosCena -= 1;
            Destroy (gameObject);
        }
    }
}
