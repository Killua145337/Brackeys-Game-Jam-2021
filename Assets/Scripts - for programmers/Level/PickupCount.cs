using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI starText;
    AudioSource audioSource;
    public int starsCollected = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        starText.text = "Stars Collected: " + starsCollected.ToString();

        if(starsCollected >= 40)
        {
            audioSource.Play();
            
        }

    }
}
