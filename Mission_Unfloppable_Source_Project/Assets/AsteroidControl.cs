using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    [SerializeField]
    private Controller controller;
    private Rigidbody2D rb;
    private Vector2 centerPoint = new Vector2(0.0f, 0.0f);
    private float BoundRadius = 2.0f;
    public float velocity_ratio;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        GameObject Controller1 = GameObject.Find("Controller");
        controller = Controller1.GetComponent<Controller>();
        BoundRadius = Mathf.Max(Screen.width, Screen.height) * BoundRadius / 100.0f;
        velocity_ratio = 1.0f;
    }

    public void UpdateVelocity(Vector2 velocity)
    {
        rb.velocity = velocity/ velocity_ratio;
    }

    void Update()
    {
        if (controller != null)
        {
            UpdateVelocity(controller.velocity);
        }

        if(Vector2.Distance(transform.position, centerPoint) > BoundRadius)
        {
            Destroy(this.gameObject);
        }
    }
}
