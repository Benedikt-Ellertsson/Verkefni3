using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReynaAftur : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Afl�sa m�sa bendi
        Cursor.visible = true; // Gera m�sa bendir s�nilegan aftur
    }
    public void Retry()
    {
        SceneManager.LoadScene(0); // Skipta yfir � main menu senu
    }
}
