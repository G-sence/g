using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateStage : MonoBehaviour
{
    public GameObject stagePrefab;
    public Vector3 spawnPosition;
    private bool hasGenerated = false;
    private GameObject currentStage;
    private GameObject previousStage;

    void Start()
    {
        previousStage = Instantiate(stagePrefab, new Vector3(0f, 5.5f, 0), Quaternion.identity);
        currentStage = previousStage;
        spawnPosition = transform.position;
        hasGenerated = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("wall") && !hasGenerated)
        {
            if (previousStage != null)
            {
                Destroy(previousStage);
            }

            previousStage = currentStage;

            currentStage = Instantiate(stagePrefab, new Vector3(spawnPosition.x, 5.5f, 0), Quaternion.identity);

            hasGenerated = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("wall"))
        {
            hasGenerated = false;
        }
    }
}
