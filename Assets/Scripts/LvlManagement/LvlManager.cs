using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{

    public int menuSceneIndex, endSceneIndex, firstLvlSceneIndex;


    public void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }
    public void LoadStartMenu()
    {

        SceneManager.LoadScene(menuSceneIndex);
    }
    public void LoadFirstLvl()
    {
        SceneManager.LoadScene(firstLvlSceneIndex);
    }
    public int NextLvlIndex()
    {
        int index=SceneManager.GetActiveScene().buildIndex+1;
        int sceneCountSum = SceneManager.sceneCountInBuildSettings;

        if (index < sceneCountSum)
        {
            SceneManager.LoadScene(index);
            return index;
        }
        else
        {
            SceneManager.LoadScene(endSceneIndex);
            return endSceneIndex;
        }
    }
    public void GoToNextLvl()
    {
        SceneManager.LoadScene(NextLvlIndex());
    }
    public void GoToNextLvl(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
#else
        Application.Quit();
#endif
    }
}
