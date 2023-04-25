using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Aflæsa músa bendi
        Cursor.visible = true; // Gera músa bendir aftur sýnilegan
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0); // Skipta yfir á Main menu senu
    }
}
