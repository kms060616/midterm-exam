using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void GameStartButtonAction()
    {
        SceneManager.LoadScene("Level_1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
