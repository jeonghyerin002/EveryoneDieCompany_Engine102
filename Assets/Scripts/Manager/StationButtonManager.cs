using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StationButtonManager : MonoBehaviour
{
    public Button deletePanelButton;

    public GameObject ShopPanel;





    void Start()
    {
        
    }
    void Update()
    {
        deletePanelButton.onClick.AddListener(DeletePanel);
        NextScene();
    }
    void NextScene()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = i + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    void DeletePanel()
    {
        GameObject.Destroy(ShopPanel);
    }
}
