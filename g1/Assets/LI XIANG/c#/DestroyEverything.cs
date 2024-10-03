using UnityEngine;

public class DestroyEverything : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            Destroy(other.gameObject);
        }
    }
}



