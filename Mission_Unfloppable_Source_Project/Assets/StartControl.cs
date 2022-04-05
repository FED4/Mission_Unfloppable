using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{

    //public Button next;
    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.Instance.level = 1;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject startButton = GameObject.Find("Canvas/StartButton");
        //next = startButton.GetComponent<Button>();
        //next.onClick.AddListener(playGame);
    }

    public void playGame()
    {
        GlobalControl.Instance.level = GlobalControl.Instance.level + 1;
        SceneManager.LoadScene("mainScene", LoadSceneMode.Single);
    }
}
