using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public void OnClick(int nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("end Game");
    }
}
