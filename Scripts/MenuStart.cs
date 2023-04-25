using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Skipta yfir ? leikinn sj?lfann.
    }
}
