using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    //ship
    public float thrust;
    public float sideThrust;
    public float drag;
    public float gravity;
    public float inverseInertia;
    public float disturb;
    public float sideDisturb;
    public float angularDisturb;
    public float thrustDisturb;
    public Vector2 velocity;
    public float angularVelocity;
    public bool start_engine;
    public bool start_left_engine;
    public bool start_right_engine;
    public Vector2 direction;
    public float angle;
    public Vector2 position;
    private Vector2 gravityV;
    private Vector2 positive_direction;

    //txt
    public GameObject instruction1;
    public int level = 1;

    //nav
    public GameObject altitude;
    public Text altitude_txt;
    private Vector2 starbase_position;
    public float random_range;
    public float base_spacing;
    public float navigate_ratio;
    public GameObject navigate_arrow;
    public GameObject navigate_starbase;
    public Vector3 navi_pos;
    public float navi_distance;
    public Vector2 relative_vector;
    public float condition;
    public bool set = false;
    public GameObject instruction2;

    void Start()
    {
        velocity = new Vector2(0.0f, 0.0f);
        direction = new Vector2(0.0f, -1.0f);
        positive_direction = new Vector2(0.0f, -1.0f);
        gravityV = new Vector2(0.0f, gravity);
        angularVelocity = 0.0f;
        position = new Vector2(0.0f, 0.0f);//inheratage

        //UI
        //instruction
        instruction1 = GameObject.Find("Canvas/instruction");
        instruction1.SetActive(true);
        StartCoroutine(waitForSec(10.0f, instruction1));

        //altitude
        altitude = GameObject.Find("Canvas/altitude");
        altitude_txt = altitude.GetComponent<Text>();
        GameObject minal = GameObject.Find("Canvas/minal");
        Text minal1 = minal.GetComponent<Text>();
        

        //navigator
        navigate_arrow = GameObject.Find("Canvas/navigator/navigate_arrow");
        navigate_starbase = GameObject.Find("Canvas/navigator/navigate_starbase");

        //under
        level = GlobalControl.Instance.level;
        GameObject level1 = GameObject.Find("Canvas/level");
        Text level_txt = level1.GetComponent<Text>();
        level_txt.text = level.ToString();

        position.y = level * (-base_spacing) + base_spacing;
        starbase_position = new Vector2(position.x + Random.Range(-random_range, random_range)*(float)level, position.y * (float)level - base_spacing);
        condition = position.y;
        minal1.text = Mathf.Floor(-position.y / 1024).ToString(); ;
    }

    void Update()
    {

        if (position.y-2.0f > condition && set == false)
        {
            instruction2 = GameObject.Find("Canvas/instruction2");
            instruction2.GetComponent<Text>().text = "YOU CRUSHED\nRESPAWN IN 3 SECS";
            GameObject rocket = GameObject.Find("rocket");
            rocket.SetActive(false);
            set = true;
            StartCoroutine(waitForSec(5.0f, instruction2));
            SceneManager.LoadScene("failScene", LoadSceneMode.Single);
        }

        //thrust
        if(Input.GetKey(KeyCode.Space))
        {
            velocity = velocity + thrust * direction;
            start_engine = true;
        }
        else
        {
            start_engine = false;
        }

        //left
        if (Input.GetKey(KeyCode.F))
        {
            velocity = velocity + sideThrust * direction;
            velocity = velocity + (sideThrust + getRand() * sideDisturb) * Rotate(direction, -Mathf.PI/2);
            angularVelocity = angularVelocity - inverseInertia * sideThrust + getRand()*angularDisturb;
            start_left_engine = true;
        }
        else
        {
            start_left_engine = false;
        }

        //right
        if (Input.GetKey(KeyCode.J))
        {
            velocity = velocity + sideThrust * direction;
            velocity = velocity + (sideThrust + getRand() * sideDisturb) * Rotate(direction, Mathf.PI / 2);
            angularVelocity = angularVelocity + inverseInertia * sideThrust + getRand() * angularDisturb;
            start_right_engine = true;
        }
        else
        {
            start_right_engine = false;
        }


        //drag
        if (velocity.sqrMagnitude > 0.0f)
        { 
            Vector2 zeroV = new Vector2(0.0f, 0.0f);
            velocity = velocity - drag * velocity.sqrMagnitude * velocity.normalized / Mathf.Clamp(-position.y/1024.0f, 1.0f, 10000000.0f) + gravityV;
        }
        
        
        //angle
        if(direction.x > 0.0f)
        {
            angle = Vector2.Angle(direction, positive_direction) * Mathf.PI / 180;
        }
        else
        {
            angle = -Vector2.Angle(direction, positive_direction) * Mathf.PI / 180;
        }
        angle = Wrap(angle + angularVelocity);
        direction.x = Mathf.Sin(angle);
        direction.y = -Mathf.Cos(angle);

        //position
        position = position + velocity;
        altitude_txt.text = Mathf.Floor(-position.y / 1024).ToString();

        //navigate

        navigate_arrow.GetComponent<Transform>().Rotate(0, 0, angularVelocity * 180 / Mathf.PI, Space.Self);
        relative_vector = starbase_position - position;
        float distance = relative_vector.magnitude;
        float nav_x = Mathf.Clamp(-relative_vector.x * navigate_ratio, -150.0f, 150.0f);
        float nav_y = Mathf.Clamp(-relative_vector.y * navigate_ratio, -100.0f, 150.0f);
        if (distance < 4.0f*navigate_ratio)
        {
            Vector2 norm_rela_v = relative_vector.normalized * 4.0f;
            nav_x = norm_rela_v.x;
            nav_y = norm_rela_v.y;
        }
        navi_pos = new Vector3(nav_x, nav_y, 0.0f);
        navigate_starbase.GetComponent<Transform>().localPosition = navi_pos;
        navi_distance = navi_pos.magnitude;

    }

    Vector2 Rotate(Vector2 v, float theta)
    {
        Vector2 outv = new Vector2(0.0f, 0.0f);
        outv.x = Mathf.Cos(theta)* v.x - Mathf.Sin(theta)*v.y;
        outv.y = Mathf.Sin(theta)*v.x + Mathf.Cos(theta)*v.y;
        return outv;
    }

    float getRand()
    {
        return Random.Range(-1.0f, 1.0f)* disturb;
    }

    float Wrap(float angle)
    {
        if(angle > Mathf.PI)
        {
            angle = angle - 2*Mathf.PI;
        }
        if (angle < -Mathf.PI)
        {
            angle = angle + 2*Mathf.PI;
        }
        return angle;
    }

    IEnumerator waitForSec(float s, GameObject ob)
    {
        yield return new WaitForSeconds(s);
        Destroy(ob);
        //SceneManager.LoadScene("startScene", LoadSceneMode.Single);
        Debug.Log("Play!");
    }



}
