using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject GameOverText;
    public GameObject GameClearText;
    public GameObject GameClearButton;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.GameOver += GameOver;
        player.GameClear += GameClear;
    }
    private void Update()
    {
        
    }

    public void GameOver()
    {
        GameOverText.SetActive(true);
    }

    public void GameClear()
    {
        GameClearText.SetActive(true);
        GameClearButton.SetActive(true);
    }
}