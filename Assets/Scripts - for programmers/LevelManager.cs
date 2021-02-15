using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region parameters
    [SerializeField] LoadingPanel loadingpanel;
    private string _curentLevelName;
    #endregion

    #region SceneLoading
    public void LoadLevel(string levelName)
    {
        _curentLevelName = levelName;

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        ao.completed += OnLoadOperationComplete;
    }

    private void OnLoadOperationComplete(AsyncOperation obj)
    {
        Debug.Log("Loding " + _curentLevelName + "completed");
        loadingpanel.DoOutAnimation();
    }
    #endregion

    #region button functions
    public void StartGame(string levelname)
    {
        loadingpanel.DoInAnimation(levelname);
    }
    public void ReStart()
    {
        loadingpanel.DoInAnimation(SceneManager.GetActiveScene().name);
    }
    public void GOHome()
    {
        loadingpanel.DoInAnimation("Boot");
    }

    public void StartSceneLoading(string sceneName)
    {
        loadingpanel.DoInAnimation(sceneName);
    }
    #endregion
}
