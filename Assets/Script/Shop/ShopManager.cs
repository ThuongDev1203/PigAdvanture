using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject uiDetail;
    public GameObject exchangeUI;
    public GameObject checkYesNo;
    public GameObject messageUI;
    AudioManager audioManager;


    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnpenExChange()
    {
        exchangeUI.SetActive(true);
    }

    public void CloseExchange()
    {
        exchangeUI.SetActive(false);
    }

    public void OpnenMessage()
    {
        messageUI.SetActive(true);
    }

    public void CloseMessage()
    {
        SceneManager.LoadScene("ShopManager");
    }

    public void CloseCheckYesNo()
    {
        checkYesNo.SetActive(false);
    }
    public void OpenCheckYesNo()
    {
        checkYesNo.SetActive(true);
    }


    public void comBackBtn()
    {
        audioManager.PlaySFX(audioManager.click);
        SceneManager.LoadScene("MainMenu");
    }

    public void openDetailDep()
    {
        audioManager.PlaySFX(audioManager.click);
        uiDetail.SetActive(true);
    }

}
