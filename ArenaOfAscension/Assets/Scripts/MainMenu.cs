using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject cardPanel;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject optionsPanel;
    private GameObject activePanel;

    public void PlayButton()
    {
        Loader.Load(Loader.Scene.Challenge_Troll);
    }
    public void MaiMenuButton()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CardsButton()
    {
        titlePanel.SetActive(false);
        cardPanel.SetActive(true);
        activePanel = cardPanel;
    }
    public void OptionsButton()
    {
        titlePanel.SetActive(false);
        optionsPanel.SetActive(true);
        activePanel = optionsPanel;
    }
    public void ReturnButton()
    {
        activePanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}
