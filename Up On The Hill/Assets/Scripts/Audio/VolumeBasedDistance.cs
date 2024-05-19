using UnityEngine;

public class VolumeBasedDistance : MonoBehaviour
{
    [SerializeField] private float minDistance = 0.9f;
    [SerializeField] private float minVolume = 0.8f;
    [SerializeField] private float dropRate = 2.0f;

    private Transform playerTransform;
    private AudioSource audioSource;
    private float originalVolume;
    private float maxDistance;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();

        maxDistance = audioSource.maxDistance;
        originalVolume = audioSource.volume;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float distanceFactor = distance / maxDistance;
        float volume;

        if (distanceFactor < minDistance)
        {
            // Clamp volume between minVolume and originalVolume
            volume = Mathf.Clamp(1 - distanceFactor, minVolume, originalVolume);
        }
        else if(distanceFactor > 1.0f)
        {
            volume = 0.0f;
        }
        else
        {
            // Apply fast drop off
            float excessDistanceFactor = (distanceFactor - minDistance) / (1 - minDistance);
            volume = Mathf.Clamp(0.9f * Mathf.Exp(-dropRate * excessDistanceFactor), 0, 0.9f);
        }

        audioSource.volume = volume;
    }
}