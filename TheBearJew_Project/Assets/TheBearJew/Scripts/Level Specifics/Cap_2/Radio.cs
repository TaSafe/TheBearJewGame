using UnityEngine;
using UnityEngine.SceneManagement;

public class Radio : MonoBehaviour, IInteraction
{
    [SerializeField] private string nextScene;

    public void Interaction()
    {
        SceneManager.LoadScene(nextScene);
    }
}
