using UnityEngine;

public class VolumeBasedDistance : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float fallOffDistanceRatio;
    private Transform playerTransform;
    private AudioSource audioSource;
    private float originalVolume;
    private float fallOffDistance;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
        audioSource.maxDistance = maxDistance;
        audioSource.minDistance = 0;

        fallOffDistance = maxDistance * fallOffDistanceRatio;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= fallOffDistance)
        {
            audioSource.volume = originalVolume;
        }
        else if (distance <= maxDistance)
        {
            float distanceBeyondFallOff = distance - fallOffDistance;
            float fallOffRange = maxDistance - fallOffDistance;
            float volume = Mathf.Lerp(originalVolume, 0f, distanceBeyondFallOff / fallOffRange);
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}
