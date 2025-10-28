using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        
    }


    void Update()
    {
        playButton.onClick.AddListener(PlayButton);
        exitButton.onClick.AddListener(ExitButton);
    }
    void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    void ExitButton()
    {
        Application.Quit();
    }
}
