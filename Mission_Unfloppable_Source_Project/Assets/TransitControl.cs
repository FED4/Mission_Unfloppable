using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitControl : MonoBehaviour
{

    //public Button next;
    // Start is called before the first frame update
    public GameObject ob;
    void Start()
    {
        //next = this.GetComponent<Button>();
        //next.onClick.AddListener(nextLevel);
        int level = GlobalControl.Instance.level;
        GameObject level1 = GameObject.Find("Canvas/level");
        Text level_txt = level1.GetComponent<Text>();
        level_txt.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        GlobalControl.Instance.level = GlobalControl.Instance.level + 1;
        SceneManager.LoadScene("mainScene", LoadSceneMode.Single);
    }

}
