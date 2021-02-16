using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI starText;
    public int starsCollected = 0;

    // Update is called once per frame
    void Update()
    {
        starText.text = "Stars Collected: " + starsCollected.ToString();
    }
}
