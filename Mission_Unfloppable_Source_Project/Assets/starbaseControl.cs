using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starbaseControl : MonoBehaviour
{
    [SerializeField]
    private Controller controller;
    private Rigidbody2D rb;
    private Vector2 centerPoint = new Vector2(0.0f, 0.0f);
    public float velocity_ratio;
    public float baseRadius = 200.0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        GameObject Controller1 = GameObject.Find("Controller");
        controller = Controller1.GetComponent<Controller>();
        velocity_ratio = 1.0f;
    }

    public void UpdateVelocity(Vector2 velocity)
    {
        rb.velocity = velocity / velocity_ratio;
    }

    void Update()
    {
        if (controller != null)
        {
            UpdateVelocity(controller.velocity);
        }

    }
}
