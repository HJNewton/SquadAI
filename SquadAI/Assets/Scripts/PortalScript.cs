using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PortalScript : MonoBehaviour
{
    [Header("Portal Setup")]
    public TextMeshProUGUI amountThroughText;
    [SerializeField] private float amountAllowedThrough;
    [SerializeField] private float amountThrough;

    private void Update()
    {
        amountThroughText.text = amountThrough.ToString() + " / " + amountAllowedThrough.ToString();

        if (amountThrough >= amountAllowedThrough)
        {
            SceneManager.LoadScene("End Game Scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            amountThrough++;
            Destroy(other.gameObject);
        }
    }
}
