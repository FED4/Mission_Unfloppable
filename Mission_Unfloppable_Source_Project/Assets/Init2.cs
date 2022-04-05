using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init2 : MonoBehaviour
{
    public GameObject asteroid;
    public float respawnTime;
    public float minRadius;
    public float maxRadius;
    private Vector2 destroyBounds;
    private Vector2 screenBounds;
    private Controller controller;

    public GameObject starbase;
    private bool created_starbase;

    // Start is called before the first frame update
    void Start()
    {
        destroyBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1.2f, Screen.height * 1.2f));
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        StartCoroutine(asteroidWave());

        GameObject Controller1 = GameObject.Find("Controller");
        controller = Controller1.GetComponent<Controller>();

        created_starbase = false;
    }

    private void spawnAsteroid()
    {
        GameObject a = Instantiate(asteroid) as GameObject;
        float r = Random.Range(minRadius, maxRadius);
        float angle = Random.Range(0.0f, 2.0f*Mathf.PI);
        float x = r * Mathf.Sin(angle);
        float y = r * Mathf.Cos(angle);
        a.transform.position = new Vector3(x,y,100.0f);
    }

    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnAsteroid();
        }

    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("transitScene", LoadSceneMode.Single);

    }

    // Update is called once per frame
    void Update()
    {
        if(controller != null && created_starbase == false && controller.navi_distance < 50)
        {
            GameObject s = Instantiate(starbase) as GameObject;
            created_starbase = true;
            s.transform.position = new Vector3(controller.navi_pos.x/10.0f, controller.navi_pos.y/10.0f, 101.0f);

            //Scene
            StartCoroutine(countDown());
        }
    }
}

