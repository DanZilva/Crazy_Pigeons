using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIMANAGER : MonoBehaviour
{
    // Start is called before the first frame update

    public static UIMANAGER instance;

    public Animator painelGameOver,painelWin,painelPause;
    [SerializeField]
    private Button winBtnMenu,winBtnNovamente,winBtnProximo;
    public Animator estrela1,estrela2,estrela3;
    [SerializeField]
    private Button loseBtnMenu,loseBtnNovamaente;
    [SerializeField]
    private Button pauseBtn,pauseBtnPlay,pauseBtnNovamente,pauseBtnMenu,pauseBtnLoja;
    public AudioSource winSom;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        //Painel
        painelGameOver = GameObject.Find("Menu_Lose").GetComponent<Animator>();
        painelWin = GameObject.Find("Menu_Win").GetComponent<Animator>();
        painelPause = GameObject.Find("Painel_Pause").GetComponent<Animator>();
        //Btn Win
        winBtnMenu = GameObject.Find("Button_Menu").GetComponent<Button>();
        winBtnNovamente = GameObject.Find("Button_Novamente").GetComponent<Button>();
        winBtnProximo = GameObject.Find("Button_Avancar").GetComponent<Button>();
        //Estrelas
        estrela1 = GameObject.Find("Estrela1_win").GetComponent<Animator>();
        estrela2 = GameObject.Find("Estrela2_win").GetComponent<Animator>();
        estrela3 = GameObject.Find("Estrela3_win").GetComponent<Animator>();
        //Btn Lose
        loseBtnMenu = GameObject.Find("Button_Menul").GetComponent<Button>();
        loseBtnNovamaente = GameObject.Find("Button_Novamentel").GetComponent<Button>();
        //Btn Pause
        pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
        pauseBtnPlay = GameObject.Find("play").GetComponent<Button>();
        pauseBtnNovamente = GameObject.Find("again").GetComponent<Button>();
        pauseBtnMenu = GameObject.Find("scene").GetComponent<Button>();
        pauseBtnLoja = GameObject.Find("shop").GetComponent<Button>();
        //audio
        winSom = painelWin.GetComponent<AudioSource>();
        
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
