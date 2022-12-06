using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentscene;
    public bool dead = false;

    private void Start()
    {
        currentscene = SceneManager.GetActiveScene().buildIndex;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single);
        if (dead && Input.GetKeyDown(KeyCode.R))
            Reload();
    }
    public void FinishLevel()
    {
        if (currentscene+2 > SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        else
            SceneManager.LoadScene(currentscene+1, LoadSceneMode.Single);
    }

    public void Reload ()
    {
        SceneManager.LoadScene(currentscene, LoadSceneMode.Single);
    }
}
