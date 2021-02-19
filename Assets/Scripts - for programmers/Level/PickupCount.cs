using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PickupCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI starText;
    AudioSource audioSource;
    public int starsCollected = 0;

    private void Start() {
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        starText.text = "Stars Collected: " + starsCollected.ToString() + "/40";

        if(starsCollected >= 40)
        {
            // audioSource.Play();
            SceneManager.LoadScene("Cutscene");

        }

    }
}
