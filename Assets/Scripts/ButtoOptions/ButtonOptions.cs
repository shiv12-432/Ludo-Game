using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonOptions : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
    public void Rules()
    {
        SceneManager.LoadScene(4);
    }
    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
   
}
