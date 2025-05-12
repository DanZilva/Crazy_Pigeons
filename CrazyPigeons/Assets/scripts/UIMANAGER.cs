using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMANAGER : MonoBehaviour
{
    public static UIMANAGER instance;

    public Animator painelGameOver, painelWin, painelPause;
    [SerializeField] private Button winBtnMenu, winBtnNovamente, winBtnProximo;
    public Animator estrela1, estrela2, estrela3;
    [SerializeField] private Button loseBtnMenu, loseBtnNovamaente;
    [SerializeField] private Button pauseBtn, pauseBtnPlay, pauseBtnNovamente, pauseBtnMenu, pauseBtnLoja;
    public AudioSource winSom;
    public AudioSource loseSom;
    public Text pontosTxt, bestPontoTxt;
    public Text moedasTxt;

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

        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        //Paineis
        painelGameOver = GameObject.Find("Menu_Lose")?.GetComponent<Animator>();
        painelWin = GameObject.Find("Menu_Win")?.GetComponent<Animator>();
        painelPause = GameObject.Find("Painel_Pause")?.GetComponent<Animator>();
        //BTN WIN
        winBtnMenu = GameObject.Find("Button_Menu")?.GetComponent<Button>();
        winBtnNovamente = GameObject.Find("Button_Novamente")?.GetComponent<Button>();
        winBtnProximo = GameObject.Find("Button_Avancar")?.GetComponent<Button>();
        //Estrelas
        estrela1 = GameObject.Find("Estrela1_win")?.GetComponent<Animator>();
        estrela2 = GameObject.Find("Estrela2_win")?.GetComponent<Animator>();
        estrela3 = GameObject.Find("Estrela3_win")?.GetComponent<Animator>();
        //BTN Lose
        loseBtnMenu = GameObject.Find("Button_Menul")?.GetComponent<Button>();
        loseBtnNovamaente = GameObject.Find("Button_Novamentel")?.GetComponent<Button>();
        //BTN Pause
        pauseBtn = GameObject.Find("Pause")?.GetComponent<Button>();
        pauseBtnPlay = GameObject.Find("play")?.GetComponent<Button>();
        pauseBtnNovamente = GameObject.Find("again")?.GetComponent<Button>();
        pauseBtnMenu = GameObject.Find("scene")?.GetComponent<Button>();
        pauseBtnLoja = GameObject.Find("shop")?.GetComponent<Button>();
        //Audio
        winSom = painelWin?.GetComponent<AudioSource>();
        loseSom = painelGameOver?.GetComponent<AudioSource>();
        //Pontos
        pontosTxt = GameObject.FindWithTag("pointVal")?.GetComponent<Text>();
        bestPontoTxt = GameObject.FindWithTag("ptBest")?.GetComponent<Text>();
        //Text Score
        moedasTxt = GameObject.FindWithTag("moedatxt").GetComponent<Text>();
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
