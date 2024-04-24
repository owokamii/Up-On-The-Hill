using UnityEngine;

public class GraveInteract : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            sp.enabled = true;
        }
    }
}
