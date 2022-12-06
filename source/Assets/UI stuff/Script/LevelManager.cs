using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void back()
    {
        FindObjectOfType<WindowManager>().OpenWindowSolo("WithLevelSelect");
    }

    public void Level01()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Level02()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
