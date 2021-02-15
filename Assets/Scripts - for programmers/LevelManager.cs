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
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region parameters
    [SerializeField] LoadingPanel loadingpanel;
    [SerializeField] GameObject titlepanel;
    [SerializeField] GameObject pausepanel;
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
        Debug.Log("Loading " + _curentLevelName + "completed");
        loadingpanel.DoOutAnimation();
    }
    #endregion

    #region button functions
    public void StartGame(string levelname)
    {
        titlepanel.SetActive(false);

        loadingpanel.DoInAnimation(levelname);
    }
    public void ReStart()
    {
        loadingpanel.DoInAnimation(SceneManager.GetActiveScene().name);
    }
    public void GOHome()
    {
        titlepanel.SetActive(true);

        loadingpanel.DoInAnimation("Boot");
    }

    public void StartSceneLoading(string sceneName)
    {
        loadingpanel.DoInAnimation(sceneName);
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pausepanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausepanel.SetActive(false);
        }
    }
}
