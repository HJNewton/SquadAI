using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretPoint : MonoBehaviour
{
    [Header("Turret Point Setup")]
    public Canvas buyCanvas;
    public GameObject ballista;
    public int ballistaCost;
    public GameObject fireTurret;
    public int fireTurretCost;

    private bool purchasedTurret;

    GameManagerScript gameManager;

    private void Awake()
    {
        buyCanvas.enabled = false;

        ballista.SetActive(false);
        fireTurret.SetActive(false);

        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>();
    }

    public void BuyBallista()
    {
        if (gameManager.coinsTotal >= ballistaCost)
        {
            gameManager.coinsTotal -= ballistaCost;

            ballista.SetActive(true);

            purchasedTurret = true;

            buyCanvas.enabled = false;
        }
    }

    public void BuyFireTurret()
    {
        if (gameManager.coinsTotal >= fireTurretCost)
        {
            gameManager.coinsTotal -= fireTurretCost;

            fireTurret.SetActive(true);

            purchasedTurret = true;

            buyCanvas.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        if (!purchasedTurret)
        {
            buyCanvas.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (!purchasedTurret)
        {
            buyCanvas.enabled = false;
        }
    }
}
