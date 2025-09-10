using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject GamePanel;

    public void OnMouseDown()
    {
        mainPanel.SetActive(true);
        GamePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
