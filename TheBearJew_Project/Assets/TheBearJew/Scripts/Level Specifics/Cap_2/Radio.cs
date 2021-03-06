using UnityEngine;
using UnityEngine.SceneManagement;

public class Radio : MonoBehaviour, IInteraction
{
    [SerializeField] private string nextScene;

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;

    public void Interaction()
    {
        UiHUD.Instance?.LoadingPanel(true);
        SceneManager.LoadScene(nextScene);
    }
}
