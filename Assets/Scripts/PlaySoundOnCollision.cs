using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    private AudioSource audioSource;
    public float volumeScaleFactor = 10f; // You can adjust this factor in the Inspector

    void Start()
    {
        // Get the AudioSource component attached to the cube
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the AudioSource is available and if an audio clip is assigned
        if (audioSource != null && audioSource.clip != null)
        {
            // Get the impact force
            float impactForce = collision.relativeVelocity.magnitude;

            // Adjust the volume of the AudioSource based on the impact force
            audioSource.volume = Mathf.Clamp(impactForce / volumeScaleFactor, 0.1f, 1f);

            // Play the audio clip
            audioSource.Play();
        }
    }
}
