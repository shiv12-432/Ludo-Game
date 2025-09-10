using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject GamePannel;

    public void Game1()
    {
        GameManager.gm.totalPlayerCanPlay = 2;
        MainPanel.SetActive(false);
        GamePannel.SetActive(true);
        Game1Setting();
    }

    public void Game2()
    {
        GameManager.gm.totalPlayerCanPlay = 3;
        MainPanel.SetActive(false);
        GamePannel.SetActive(true);
        Game2Setting();
    }

    public void Game3()
    {
        GameManager.gm.totalPlayerCanPlay = 4;
        MainPanel.SetActive(false);
        GamePannel.SetActive(true);
    }

    public void Game4()
    {
        GameManager.gm.totalPlayerCanPlay = 1;
        MainPanel.SetActive(false);
        GamePannel.SetActive(true);
        Game1Setting();
    }

    void Game1Setting()
    {
        HidePlayers(GameManager.gm.redPlayerPice);
        HidePlayers(GameManager.gm.yellowPlayerPice);

    }

    void Game2Setting()
    {
        
        HidePlayers(GameManager.gm.yellowPlayerPice);

    }

    public void HidePlayers(PlayerPiece [] PlayerPieces_)
    {
        for (int i = 0; i < PlayerPieces_.Length; i++)
        {
            PlayerPieces_[i].gameObject.SetActive(false);
        }
    }
}
