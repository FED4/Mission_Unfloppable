using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigatorController : MonoBehaviour
{
    private Controller controller;
    public GameObject navigate_starbase;
    public Vector3 lp;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Controller1 = GameObject.Find("Controller");
        controller = Controller1.GetComponent<Controller>();

        navigate_starbase = GameObject.Find("Canvas/navigator/navigate_starbase");
        navigate_starbase.GetComponent<Transform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        lp = navigate_starbase.GetComponent<Transform>().localPosition;
        navigate_starbase.GetComponent<Transform>().localPosition = controller.navi_pos;
    }
}
