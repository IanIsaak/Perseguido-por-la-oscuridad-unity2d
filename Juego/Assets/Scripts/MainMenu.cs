using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Nos permite manejar el flujo de pantallas

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
