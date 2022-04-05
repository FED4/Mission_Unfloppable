using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField]
    private Controller controller;
    private Rigidbody2D rb;
    private Vector2 centerPoint = new Vector2(0.0f, 0.0f);
    private Animator main_engine_animator;
    private Animator left_engine_animator;
    private Animator right_engine_animator;

    // Start is called before the first frame update
    void Start()
    {
        //Controller
        rb = this.GetComponent<Rigidbody2D>();
        GameObject Controller1 = GameObject.Find("Controller");
        controller = Controller1.GetComponent<Controller>();

        //children animators
        GameObject main_engine = GameObject.Find("rocket/main_engine");
        main_engine_animator = main_engine.GetComponent<Animator>();

        GameObject left_engine = GameObject.Find("rocket/left_engine");
        left_engine_animator = left_engine.GetComponent<Animator>();

        GameObject right_engine = GameObject.Find("rocket/right_engine");
        right_engine_animator = right_engine.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            UpdateAngle(controller.angularVelocity);
            main_engine_animator.SetBool("Start_Engine", controller.start_engine);
            left_engine_animator.SetBool("Start_Engine", controller.start_left_engine);
            right_engine_animator.SetBool("Start_Engine", controller.start_right_engine);
        }
    }

    void UpdateAngle(float angle)
    {
        transform.Rotate(0, 0, angle*180/Mathf.PI, Space.Self);
    }
}
