using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental;

public class UIMANAGER : MonoBehaviour
{
    public static UIMANAGER instance;

    public Animator painelGameOver, painelWin, painelPause;
    [SerializeField] public Button winBtnMenu, winBtnNovamente, winBtnProximo;
    public Animator estrela1, estrela2, estrela3;
    [SerializeField] private Button loseBtnMenu, loseBtnNovamente;
    [SerializeField] private Button pauseBtn, pauseBtnPlay, pauseBtnNovamente, pauseBtnMenu, pauseBtnLoja;
    public AudioSource winSom;
    public AudioSource loseSom;
    public Text pontosTxt, bestPontoTxt;
    public Text moedasTxt;
    public Image fundoPreto;

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
        DadosParaCarregamento();
    }

    void DadosParaCarregamento()
    {
        
        if (ONDEESTOU.instance.fase != 0 && ONDEESTOU.instance.fase != 5 &&
            ONDEESTOU.instance.fase != 6 && ONDEESTOU.instance.fase != 7 &&
            ONDEESTOU.instance.fase != 8 && ONDEESTOU.instance.fase != 9)
        {
            // Paineis
            painelGameOver = GameObject.Find("Menu_Lose")?.GetComponent<Animator>();
            painelWin = GameObject.Find("Menu_Win")?.GetComponent<Animator>();
            painelPause = GameObject.Find("Painel_Pause")?.GetComponent<Animator>();

            // Botões Win
            winBtnMenu = GameObject.Find("Button_Menu")?.GetComponent<Button>();
            winBtnNovamente = GameObject.Find("Button_Novamente")?.GetComponent<Button>();
            winBtnProximo = GameObject.Find("Button_Avancar")?.GetComponent<Button>();

            // Estrelas
            estrela1 = GameObject.Find("Estrela1_win")?.GetComponent<Animator>();
            estrela2 = GameObject.Find("Estrela2_win")?.GetComponent<Animator>();
            estrela3 = GameObject.Find("Estrela3_win")?.GetComponent<Animator>();

            // Botões Lose
            loseBtnMenu = GameObject.Find("Button_Menul")?.GetComponent<Button>();
            loseBtnNovamente = GameObject.Find("Button_Novamentel")?.GetComponent<Button>();

            // Botões Pause
            pauseBtn = GameObject.Find("Pause")?.GetComponent<Button>();
            pauseBtnPlay = GameObject.Find("play")?.GetComponent<Button>();
            pauseBtnNovamente = GameObject.Find("again")?.GetComponent<Button>();
            pauseBtnMenu = GameObject.Find("scene")?.GetComponent<Button>();
            pauseBtnLoja = GameObject.Find("shop")?.GetComponent<Button>();

            // Audio
            winSom = painelWin?.GetComponent<AudioSource>();
            loseSom = painelGameOver?.GetComponent<AudioSource>();

            // Textos
            pontosTxt = GameObject.FindWithTag("pointVal")?.GetComponent<Text>();
            bestPontoTxt = GameObject.FindWithTag("ptBest")?.GetComponent<Text>();
            moedasTxt = GameObject.FindWithTag("moedatxt")?.GetComponent<Text>();

            // Imagem fundo preto
            fundoPreto = GameObject.FindWithTag("fundoPreto")?.GetComponent<Image>();

            // Eventos

            // Pause
            pauseBtn?.onClick.AddListener(Pausar);
            pauseBtnPlay?.onClick.AddListener(PausarInvers);
            pauseBtnNovamente?.onClick.AddListener(Again);
            pauseBtnMenu?.onClick.AddListener(GoMenu);

            // Lose
            loseBtnMenu?.onClick.AddListener(GoMenu);
            loseBtnNovamente?.onClick.AddListener(Again);

            // Win
            winBtnMenu?.onClick.AddListener(GoMenu);
            winBtnNovamente?.onClick.AddListener(Again);
            winBtnProximo?.onClick.AddListener(ProximaFase);
        }
    }

    // Pause
    void Pausar()
    {
        GAMEMANAGER.instance.pausado = true;
        Time.timeScale = 0;
        if (fundoPreto != null) fundoPreto.enabled = true;
        painelPause?.Play("MenuPauseAnim");
    }

    void PausarInvers()
    {
        GAMEMANAGER.instance.pausado = false;
        Time.timeScale = 1;
        if (fundoPreto != null) fundoPreto.enabled = false;
        painelPause?.Play("MenuPauseAnimInvers");
    }

    // Jogar Novamente
    void Again()
    {
        SceneManager.LoadScene(ONDEESTOU.instance.fase);
        Time.timeScale = 1;
        GAMEMANAGER.instance.pausado = false;
    }

    // Ir para o Menu
    void GoMenu()
    {
        if (ONDEESTOU.instance.faseMestra == "Mestra1")
        {
            SceneManager.LoadScene("Mestra1");
        }
        else if (ONDEESTOU.instance.faseMestra == "Mestra2")
        {
            SceneManager.LoadScene("Mestra2");
        }
    }

    // Loja (reserva)
    void GoLoja()
    {
        Time.timeScale = 1;
        GAMEMANAGER.instance.pausado = false;
    }

    // Avançar fase
    void ProximaFase()
    {
        SceneManager.LoadScene(ONDEESTOU.instance.fase + 1);
    }

    void Start() { }
    void Update() { }
}
