using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMEMANAGER : MonoBehaviour
{

    public static GAMEMANAGER instance;
    public GameObject[] passaro;
    public int passarosNum;
    public int passarosEmCena = 0;
    public Transform pos;
    public bool win;
    public bool jogoComecou;
    public string nomePassaro;

    public bool passaroLancado = false;
    public Transform objE, objD;

    public int numPorcosCena;
    private bool tocaWin = false, tocaLose = false;

    public bool estrela1fim, estrela2fim,estrela3fim;

    public int aux;

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

        SceneManager.sceneLoaded += Carrega;


    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        pos = GameObject.FindWithTag("pos").GetComponent<Transform>();
        objE = GameObject.FindWithTag("PE").GetComponent<Transform>();
        objD = GameObject.FindWithTag("PD").GetComponent<Transform>();
        StartGame();

        //Passaro Pos
        
        passarosNum = GameObject.FindGameObjectsWithTag("Player").Length;
        passaro = new GameObject[passarosNum];

        for (int x = 0; x < GameObject.FindGameObjectsWithTag("Player").Length; x++)
        {
            passaro[x] = GameObject.Find("Bird" + x);
        }

        //

        numPorcosCena = GameObject.FindGameObjectsWithTag ("porco").Length;
        aux = passarosNum;
    }

    void NascPassaro()
    {

        if (passarosEmCena == 0 && passarosNum > 0)
        {
            
            for(int x = 0; x < passaro.Length; x++)
            {
                if (passaro[x] != null)
                {
                    if (passaro[x].transform.position != pos.position && passarosEmCena == 0)
                    {
                        nomePassaro = passaro[x].name;
                        passaro[x].transform.position = pos.position;
                        passarosEmCena = 1;
                    }
                }
            }
        }
    }

    void GameOver()
    {
        jogoComecou = false;
    }

    void WinGame()
    {
        jogoComecou = false;
        UIMANAGER.instance.painelWin.Play ("MenuWinAnimado");

        if (!UIMANAGER.instance.winSom.isPlaying && tocaWin == false)
        {
            UIMANAGER.instance.winSom.Play ();
            tocaWin = true;
        }

        if (tocaWin && !UIMANAGER.instance.winSom.isPlaying)
        {
           if (passarosNum == aux - 1)
           {    
                print("Libera 3 Estrelas!");
                UIMANAGER.instance.estrela1.Play ("Estrela1_animada");
                

                if (estrela1fim)
                {
                    UIMANAGER.instance.estrela2.Play ("Estrela2_animada");

                    if (estrela2fim)
                    {
                        UIMANAGER.instance.estrela3.Play ("Estrela3_animada");
                    }
                }
           } 

            else if (passarosNum == aux - 2)
            {
                print("Libera 2 Estrelas!");
                UIMANAGER.instance.estrela1.Play("Estrela1_animada");

                    if (estrela1fim)
                    {
                        UIMANAGER.instance.estrela2.Play ("Estrela2_animada");
                    }              
            }

            else if(passarosNum <= aux -3)
            {
                print("Libera 1 Estrela!");
                UIMANAGER.instance.estrela1.Play("Estrela1_animada");
            }
        }

    }

    void StartGame()
    {
        jogoComecou = true;
        passarosEmCena = 0;
        win = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numPorcosCena <= 0 && passarosNum > 0)
        {
            win = true;
        }
        if(win)
        {
            WinGame ();
        }
        else
        {
            NascPassaro ();
        }
    }
}
