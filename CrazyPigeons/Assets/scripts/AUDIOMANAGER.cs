using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AUDIOMANAGER : MonoBehaviour
{
    public static AUDIOMANAGER instance;

    public AudioClip [] clip;
    public AudioSource audioS;

    public int pause = -1;

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

        audioS = GetComponent<AudioSource> ();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pause == 1)
        {
            audioS.Pause();
        }
        else if (!audioS.isPlaying)
        {
            audioS.Play();
        }
    }


    public void GetSom(int clips)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene.StartsWith("Level") && currentScene.Contains("Mestra"))
        {
            audioS.Stop();
            audioS.clip = null;
            return;
        }



        if (clips == 0)
            {
                audioS.clip = clip[0];
                audioS.loop = true;
                audioS.Play();
            }
            else if (clips == 1)
            {
                audioS.clip = clip[1];
                audioS.loop = true;
                audioS.Play();
            }







    }

}
