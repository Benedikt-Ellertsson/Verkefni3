using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Afl�sa m�sa bendi
        Cursor.visible = true; // Gera m�sa bendir aftur s�nilegan
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0); // Skipta yfir � Main menu senu
    }
}
