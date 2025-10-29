using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StationButtonManager : MonoBehaviour
{
    public Button nextSceneButton;
    public Button deletePanelButton;


    void Start()
    {
        
    }
    void Update()
    {
        nextSceneButton.onClick.AddListener(NextSceneButton);
        deletePanelButton.onClick.AddListener(DeletePanel);
    }
    void NextSceneButton()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = i + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void DeletePanel()
    {
        GameObject.Destroy(gameObject);
    }
}
