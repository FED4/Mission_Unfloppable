using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void playGame()
    {
        GlobalControl.Instance.level = 1;
        SceneManager.LoadScene("mainScene", LoadSceneMode.Single);
        Debug.Log("Play!");
    }
}
