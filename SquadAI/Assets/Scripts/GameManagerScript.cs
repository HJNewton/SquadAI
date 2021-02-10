using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public int coinsTotal;
    public TextMeshProUGUI coinsText;

    private void Update()
    {
        coinsText.text = "Coins: " + coinsTotal.ToString();
    }
}
