using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    string LevelName;
    [SerializeField] Image[] images;
    [SerializeField] TMPro.TMP_Text[] texts;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoOutAnimation", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoInAnimation(string LevelName)
    {
        this.LevelName = LevelName;
        foreach (var item in images)
        {
            item.DOFade(1, 1);
        }

        foreach (var item in texts)
        {
            item.DOFade(1, 1);
        }

        Invoke("OnInAnimationEnd", 1.1f);
    }

    public void OnInAnimationEnd()
    {
        LevelManager.instance.LoadLevel(LevelName);
    }

    public void DoOutAnimation()
    {
        foreach (var item in images)
        {
            item.DOFade(0, 1);
        }

        foreach (var item in texts)
        {
            item.DOFade(0, 1);
        }
    }

    public void OnOutAnimationEnd()
    {

    }
}
