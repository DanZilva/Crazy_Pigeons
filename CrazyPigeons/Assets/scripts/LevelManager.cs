using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
        public bool txtAtivo;
        public string levelReal;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

  void ListaAdd()
    {
        foreach (Level level in levelList)
        {
            GameObject btnNovo = Instantiate(botao) as GameObject;
            botaoLevel btnNew = btnNovo.GetComponent<botaoLevel>(); // Correção aqui
            Button buttonComponent = btnNovo.GetComponent<Button>();
            Image buttonImage = btnNovo.GetComponent<Image>();
            btnNew.levelTxtBTN.text = level.levelText;

            btnNew.realLevel = level.levelReal;

            // Verificar progresso salvo em PlayerPrefs

            if (ZPlayerPrefs.GetInt("Level" + btnNew.realLevel+"_"+ONDEESTOU.instance.faseMestra) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
                level.txtAtivo = true;
            }

            // Atualizar propriedades do botão
            btnNew.desbloqueadoBTN = level.desbloqueado;
            buttonComponent.interactable = level.habilitado;
            btnNew.GetComponentInChildren<Text>().enabled = level.txtAtivo;

            // Se o nível está bloqueado, mudar a cor do botão
            if (level.desbloqueado == 0)
            {
                buttonComponent.interactable = false;
                if (buttonImage != null)
                {
                    buttonImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Deixa o botão mais escuro
                }
            }

            // Adiciona a função de clique apenas se o nível estiver desbloqueado
            if (level.desbloqueado == 1)
            {
                buttonComponent.onClick.AddListener(() => ClickLevel("Level" +level.levelReal + "_" + ONDEESTOU.instance.faseMestra));

               if (ZPlayerPrefs.GetInt("Level" + btnNew.realLevel + "_" + ONDEESTOU.instance.faseMestra + "estrelas") == 1)
               {
                btnNew.estrela1.enabled = true;
               }
                else if (ZPlayerPrefs.GetInt("Level" + btnNew.realLevel + "_" + ONDEESTOU.instance.faseMestra + "estrelas") == 2)
                {
                    btnNew.estrela1.enabled = true;
                    btnNew.estrela2.enabled = true;

                }
                else if (ZPlayerPrefs.GetInt("Level" + btnNew.realLevel + "_" + ONDEESTOU.instance.faseMestra + "estrelas") == 3)
                {
                    btnNew.estrela1.enabled = true;
                    btnNew.estrela2.enabled = true; 
                    btnNew.estrela3.enabled = true;
                }
                else if (ZPlayerPrefs.GetInt("Level" + btnNew.realLevel + "_" + ONDEESTOU.instance.faseMestra + "estrelas") == 0)
                {
                    btnNew.estrela1.enabled = false;
                    btnNew.estrela2.enabled = false; 
                    btnNew.estrela3.enabled = false; 
                }



            }

            // Adicionar botão à hierarquia
            btnNovo.transform.SetParent(localBtn, false);
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    void Start()
    {
        
        ListaAdd();
    }

    void Awake()
    {
        ZPlayerPrefs.Initialize("12345678","crazypigeongame");
    }
}
