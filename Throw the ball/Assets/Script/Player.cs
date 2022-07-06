using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    public static int[] gameCount = { 3, 3, 3 };
    public static int stage = 1;
    public event Action GameOver;
    public event Action GameClear;
    private float TotalSeconds;
    public int count=gameCount[stage-1];
    private Transform CameraTransform;
    private Rigidbody2D PlayerRigidbody;
    private int jumpCount = 0;
    private float xForce;
    private float yForce;
    private float Force;
    public GameObject Timer;
    public Text TimerText;
    public Text CountText;
    private int timer = 10;
    private int timeLimit = 1;
    private bool Check = false;
    Vector2 MousePosition1;
    Vector2 MousePosition2;
    Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        GameClear += NextStage;
    }



    // Update is called once per frame
    void Update()
    {
        if (count == 0 && !Check)
        {
            timer = StockwatchTimer();
            timeLimit = 5 - timer;
            TimerText.text = ""+timeLimit;
            Timer.SetActive(true);
            if (timeLimit <= 0.0f)
            {
                Timer.SetActive(false);
                GameOver();
                Check = true;
            }
        }
        else if(count>0 && !Check)
        {
            Jump();
        }
        
    }

    void Jump()
    {
        if (Input.GetMouseButtonDown(0) && jumpCount == 0)
        {
            MousePosition1 = Input.mousePosition;
            CameraTransform.Translate(((Vector2)Input.mousePosition) - MousePosition1);
            MousePosition1 = Camera.ScreenToWorldPoint(MousePosition1);
            if (MousePosition1.x >= transform.position.x - 0.5 && MousePosition1.x <= transform.position.x + 0.5 && MousePosition1.y >= transform.position.y - 0.5 && MousePosition1.y <= transform.position.y + 0.5)
            {
                jumpCount++;
                Debug.Log("확인");
                
            }
        }
        if (Input.GetMouseButtonUp(0) && jumpCount == 1)
        {
            MousePosition2 = Input.mousePosition;
            MousePosition2 = Camera.ScreenToWorldPoint(MousePosition2);
            xForce = MousePosition1.x - MousePosition2.x;
            yForce = MousePosition1.y - MousePosition2.y;
            Force = 300;
            PlayerRigidbody.AddForce(new Vector2(xForce * Force, yForce * Force));
            Debug.Log("점프");
            jumpCount = 0;
            count -= 1;
            CountText.text = "남은 기회 : " + count;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag is "Finish")
        {
            if (count > 0)
            {
                GameClear();
                Check = true;
            }
            else if (timeLimit > 0)
            {
                Timer.SetActive(false);
                GameClear();
                Check = true;
            }
            else
            {
                Timer.SetActive(false);
                GameOver();
                Check = true;
            }
        }
    }

    int StockwatchTimer()
    {
        TotalSeconds += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(TotalSeconds);

        return timeSpan.Seconds;
    }

    private void NextStage()
    {
        stage += 1;
    }
}