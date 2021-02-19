using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLevelManager : MonoBehaviour
{
   [SerializeField] string levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
