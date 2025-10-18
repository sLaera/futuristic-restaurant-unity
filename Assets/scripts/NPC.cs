using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BurgerDomain;
using Data;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

enum ButtonStatus
{
    ShowingNext,
    ShowingSell
}

public class Npc : MonoBehaviour
{
    private DialoguePanel _dialoguePanel;
    private ButtonStatus _buttonStatus;
    public  AudioSource audio;
    public List<AudioClip> lista_dialoghi;
    void Start()
    {
        _dialoguePanel = FindObjectOfType<DialoguePanel>(true);
        _dialoguePanel.NextButton.onClick.AddListener(OnButtonClicked);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        ShowCursor();
        TipsReader.Tip tip = GameManager.Instance.Tips.GetRandomTip();
        
        //_dialoguePanel.Title.text = tip.Title;
        _dialoguePanel.Title.text = "Questo è un consiglio per evitare gli sprechi alimentari";
        audio.clip = lista_dialoghi[15];
        audio.Play();
        _dialoguePanel.Text.text = "\n" + tip.Description;
         audio.clip = lista_dialoghi[tip.Id-1];
        audio.Play();
        InitNextButton();
        _dialoguePanel.gameObject.SetActive(true);
    }

    void OnButtonClicked()
    {
        var totalProfit = FindObjectOfType<Player>().LeftoverIngredients.Aggregate(0f, (sum, i) => sum += i.GetComponent<IngredientInfo>().Prize * i.GetComponent<IngredientInfo>().Quantity);
        GameManager.Instance.Player.LeftoverIngredients.Clear();
        GameManager.Instance.ListaprezziLeft.Clear();
        GameManager.Instance.ListaStringheLeft.Clear();
        GameManager.Instance.ListaqtaLeft.Clear();
        totalProfit -= totalProfit * 0.3f;
        switch (_buttonStatus)
        {
            case ButtonStatus.ShowingNext:
                //passare al pulsante sell
                InitSellButton();
                _dialoguePanel.Title.text = "Vendi i tuoi burger";
                _dialoguePanel.Text.text = $"non sprecare gli ordini sbagliati,\n" +
                                            $"sono ancora buoni.\n\n" +
                                            $"Profitto totale: {totalProfit}";
                audio.clip = lista_dialoghi[16];
                audio.Play();
                break;
            case ButtonStatus.ShowingSell:
                FindObjectOfType<Player>().Money += totalProfit;
                _dialoguePanel.NextButton.gameObject.SetActive(false);
                _dialoguePanel.Title.text = "";
                _dialoguePanel.Text.text = "A Presto!";
                Invoke(nameof(HidePanel), 1);
                //audio.clip = lista_dialoghi[2];
                //audio.Play();
                break;
        }
    }

    void HidePanel()
    {
        _dialoguePanel.gameObject.SetActive(false);
        HideCursor();
    }

    /// <summary>
    ///settta i colori e il testo del bottone per essere il pulsante di next
    /// </summary>
    void InitNextButton()
    {
        _dialoguePanel.NextButton.gameObject.SetActive(true);
        _buttonStatus = ButtonStatus.ShowingNext;
        _dialoguePanel.NextButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
        _dialoguePanel.NextButton.GetComponent<Image>().color = new Color32(249,170,51,255);
    }
    
    void InitSellButton()
    {
        _dialoguePanel.NextButton.gameObject.SetActive(true);
        _buttonStatus = ButtonStatus.ShowingSell;
        _dialoguePanel.NextButton.GetComponentInChildren<TextMeshProUGUI>().text = "SELL";
        _dialoguePanel.NextButton.GetComponent<Image>().color = new Color32(116,234,136,255);
    }
    
    void ShowCursor()
    {
        GameManager.Instance.Player = FindObjectOfType<Player>();
        GameManager.Instance.Player.GetComponent<FirstPersonController>().cameraCanMove = false;
        GameManager.Instance.Player.GetComponent<FirstPersonController>().playerCanMove = false;
        GameManager.Instance.Player.GetComponent<FirstPersonController>().enableHeadBob = false;
        if (! Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    void HideCursor()
    {
        GameManager.Instance.Player.GetComponent<FirstPersonController>().cameraCanMove = true;
        GameManager.Instance.Player.GetComponent<FirstPersonController>().playerCanMove = true;
        GameManager.Instance.Player.GetComponent<FirstPersonController>().enableHeadBob = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
