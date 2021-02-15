using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChange : MonoBehaviour
{

    //Char1 is black
    //char2 is white
    [SerializeField] Material nightSky;
    [SerializeField] Material daySky;

    public GameObject char1, char2;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchChar();
        }
    }

    void SwitchChar()
    {
        if (char1.activeInHierarchy == true)
        {
            char1.SetActive(false);
            char2.SetActive(true);
            RenderSettings.skybox = daySky;
        }
        else if (char2.activeInHierarchy == true)
        {
            char2.SetActive(false);
            char1.SetActive(true);
            RenderSettings.skybox = nightSky;
        }
    }
}
