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
    public GameObject fireTurret;

    private bool purchasedTurret;

    private void Awake()
    {
        buyCanvas.enabled = false;

        ballista.SetActive(false);
        fireTurret.SetActive(false);
    }

    public void BuyBallista()
    {
        ballista.SetActive(true);

        purchasedTurret = true;
        
        buyCanvas.enabled = false;
    }

    public void BuyFireTurret()
    {
        fireTurret.SetActive(true);

        purchasedTurret = true;

        buyCanvas.enabled = false;
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
