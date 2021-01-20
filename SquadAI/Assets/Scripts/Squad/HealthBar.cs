using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Setup")]
    public float distanceCutOff;
    [SerializeField] private float distanceFromCam;
    public GameObject[] children;

    [SerializeField]SquadMemberCombat squadMemberCombat;

    private void Awake()
    {
        squadMemberCombat = gameObject.GetComponentInParent<SquadMemberCombat>();
    }

    private void Update()
    {
        CheckDistance();

        if (distanceFromCam > distanceCutOff) // Hide health bars if the camera is too far away
        {
            foreach (GameObject child in children)
            {
                child.SetActive(false);
            }
        }

        if (distanceFromCam < distanceCutOff || squadMemberCombat.attackState == SquadMemberCombat.AttackState.Attacking)
        {
            foreach (GameObject child in children) // Keep health bar active if cam is in distance
            {
                child.SetActive(true);
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform); // Keeps the healthbar rotated towards the camera
        transform.Rotate(0, 0, 0);
    }

    float CheckDistance() // Returns the current distance from the camera
    {
        distanceFromCam = Vector3.Distance(transform.position, Camera.main.transform.position);

        return distanceFromCam;
    }
}
