using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReynaAftur : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Aflæsa músa bendi
        Cursor.visible = true; // Gera músa bendir sýnilegan aftur
    }
    public void Retry()
    {
        SceneManager.LoadScene(0); // Skipta yfir í main menu senu
    }
}
