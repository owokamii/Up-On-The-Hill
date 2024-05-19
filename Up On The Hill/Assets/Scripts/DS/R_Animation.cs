using UnityEngine;

public class R_Animation : MonoBehaviour
{
    [SerializeField] private Animator racoonAnimator;

    public void PlayRacoonAudio()
    {
        FindObjectOfType<AudioManager>().PlaySFX("RacoonNoise");
    }

    public void RacoonIdleState()
    {
        racoonAnimator.SetBool("RacoonIdle", true);
    }

    public void DestroyRacoonEvent()
    {
        gameObject.SetActive(false);
    }
}
