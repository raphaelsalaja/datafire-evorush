using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SelectScene(int x)
    {
        SceneManager.LoadScene(x);
    }
    public void ReturnToShootingRange()
    {
        SceneManager.LoadScene("_MenuArea");
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
