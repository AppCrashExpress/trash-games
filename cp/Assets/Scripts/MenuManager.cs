using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Load(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
