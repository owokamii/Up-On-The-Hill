using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private Animator animator;

    [Header("Parameters")]
    [SerializeField] private bool keyPress;

    private int levelToLoad;
    private bool isTransitioning = false;

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && !isTransitioning)
        {
            if(keyPress)
            {
                isTransitioning = true;

                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    FadeToLevel(0);
                }
                else
                {
                    Debug.Log("next level");
                    FindObjectOfType<AudioManager>().PlaySFX("5");
                    FadeToNextLevel();
                }
            }
        }
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Start");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        isTransitioning = false;
    }
}
