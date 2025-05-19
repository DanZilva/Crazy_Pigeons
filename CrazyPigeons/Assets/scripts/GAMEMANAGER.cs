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
    public bool lose;

    public bool estrela1fim, estrela2fim, estrela3fim;

    public int estrelasNum;
    public bool trava = false;

    public int aux;

    public int pontosGame, bestPontoGame;
    public int moedasGame;

    public bool pausado = false;

    void Awake()
    {
        ZPlayerPrefs.Initialize("12345678", "crazypigeongame");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        pos = GameObject.FindWithTag("pos").GetComponent<Transform>();
        objE = GameObject.FindWithTag("PE").GetComponent<Transform>();
        objD = GameObject.FindWithTag("PD").GetComponent<Transform>();

        passarosNum = GameObject.FindGameObjectsWithTag("Player").Length;
        passaro = new GameObject[passarosNum];

        for (int x = 0; x < passarosNum; x++)
        {
            passaro[x] = GameObject.Find("Bird" + x);
        }

        numPorcosCena = GameObject.FindGameObjectsWithTag("porco").Length;
        aux = passarosNum;

        StartGame();
    }

    void NascPassaro()
    {
        if (passarosEmCena == 0 && passarosNum > 0)
        {
            for (int x = 0; x < passaro.Length; x++)
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
        UIMANAGER.instance.painelGameOver.Play("MenuLoseAnimado");
        if (!UIMANAGER.instance.loseSom.isPlaying && tocaLose == false)
        {
            UIMANAGER.instance.loseSom.Play();
            tocaLose = true;
        }
    }

    void WinGame()
    {
        SCOREMANAGER.instance.SalvarDados(moedasGame);


        int tempOnde = ONDEESTOU.instance.fase + 1;
        ZPlayerPrefs.SetInt("Level" + tempOnde + "_" + ONDEESTOU.instance.faseMestra, 1);

        if (jogoComecou != false)
        {
            jogoComecou = false;
            UIMANAGER.instance.painelWin.Play("MenuWinAnimado");

            if (!UIMANAGER.instance.winSom.isPlaying && tocaWin == false)
            {
                UIMANAGER.instance.winSom.Play();
                tocaWin = true;
            }

            //Pontos

            POINTMANAGER.instance.MelhorPontuacaoSave(ONDEESTOU.instance.faseN, pontosGame);

            //
        }

        if (!UIMANAGER.instance.winSom.isPlaying && tocaWin == false)
        {
            UIMANAGER.instance.winSom.Play();
            tocaWin = true;
        }

        if (tocaWin && !UIMANAGER.instance.winSom.isPlaying && trava == false)
        {
            if (passarosNum == aux - 1)
            {
                UIMANAGER.instance.estrela1.Play("Estrela1_animada");

                if (estrela1fim)
                {
                    UIMANAGER.instance.estrela2.Play("Estrela2_animada");

                    if (estrela2fim)
                    {
                        UIMANAGER.instance.estrela3.Play("Estrela3_animada");
                        trava = true;

                        UIMANAGER.instance.winBtnMenu.interactable = true;
                        UIMANAGER.instance.winBtnNovamente.interactable = true;
                        UIMANAGER.instance.winBtnProximo.interactable = true;


                    }
                }

                estrelasNum = 3;
            }
            else if (passarosNum == aux - 2)
            {
                UIMANAGER.instance.estrela1.Play("Estrela1_animada");

                if (estrela1fim)
                {
                    UIMANAGER.instance.estrela2.Play("Estrela2_animada");
                    trava = true;

                    UIMANAGER.instance.winBtnMenu.interactable = true;
                    UIMANAGER.instance.winBtnNovamente.interactable = true;
                    UIMANAGER.instance.winBtnProximo.interactable = true;



                }

                estrelasNum = 2;
            }
            else if (passarosNum <= aux - 3)
            {
                UIMANAGER.instance.estrela1.Play("Estrela1_animada");
                estrelasNum = 1;
                trava = true;

                UIMANAGER.instance.winBtnMenu.interactable = true;
                UIMANAGER.instance.winBtnNovamente.interactable = true;
                UIMANAGER.instance.winBtnProximo.interactable = true;
            }
            else
            {
                estrelasNum = 0;
                trava = true;
            }

            string chave = ONDEESTOU.instance.faseN + "estrelas";

            if (!ZPlayerPrefs.HasKey(chave))
            {
                ZPlayerPrefs.SetInt(chave, estrelasNum);
            }
            else
            {
                if (ZPlayerPrefs.GetInt(chave) < estrelasNum)
                {
                    ZPlayerPrefs.SetInt(chave, estrelasNum);
                }
            }

            ZPlayerPrefs.Save();
        }
    }

    void StartGame()
    {
        jogoComecou = true;
        passarosEmCena = 0;
        lose = false;
        win = false;
        trava = false;
        passaroLancado = false;
        tocaLose = false;
        tocaWin = false;
        pontosGame = 0;
        bestPontoGame = POINTMANAGER.instance.MelhorPontuacaoLoad(ONDEESTOU.instance.faseN);
        UIMANAGER.instance.pontosTxt.text = pontosGame.ToString();
        UIMANAGER.instance.bestPontoTxt.text = bestPontoGame.ToString();

        moedasGame = SCOREMANAGER.instance.LoadDados();
        UIMANAGER.instance.moedasTxt.text = SCOREMANAGER.instance.LoadDados().ToString();
        
        UIMANAGER.instance.winBtnMenu.interactable = false;
        UIMANAGER.instance.winBtnNovamente.interactable = false;
        UIMANAGER.instance.winBtnProximo.interactable = false;
    }

    void Start()
    {
        StartGame();

        
    }

    void Update()
    {
        if (numPorcosCena <= 0 && passarosNum > 0)
        {
            win = true;
        }
        else if (numPorcosCena > 0 && passarosNum <= 0)
        {
            lose = true;
        }

        if (win)
        {
            WinGame();
        }
        else if (lose)
        {
            GameOver();
        }

        if (jogoComecou)
        {
            NascPassaro();
        }
    }
}
