using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    public float fallThreshold = -10f; // You can adjust this threshold in the Inspector

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Destroy(gameObject);
        }
    }
}
