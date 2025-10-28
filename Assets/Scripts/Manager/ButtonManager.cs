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
    public Button nextSceneButton;

    void Start()
    {
        
    }


    void Update()
    {
        playButton.onClick.AddListener(PlayButton);
        exitButton.onClick.AddListener(ExitButton);
        nextSceneButton.onClick.AddListener(NextSceneButton);
    }
    void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    void ExitButton()
    {
        Application.Quit();
    }
    void NextSceneButton()
    {
        SceneManager.LoadScene(+1);
    }
}
