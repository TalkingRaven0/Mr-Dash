using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    private WindowManager wm;

    private void Awake()
    {
        wm = GetComponent<WindowManager>();
    }

    public void startgame()
    {
        SceneManager.LoadScene("Level01",LoadSceneMode.Single);
    }

    public void exitgame()
    {
            Application.Quit();
    }

    public void LevelSelect()
    {
        wm.OpenWindowSolo("LevelSelect");
    }
}
