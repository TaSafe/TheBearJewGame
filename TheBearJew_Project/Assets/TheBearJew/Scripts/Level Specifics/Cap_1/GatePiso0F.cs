using UnityEngine;
using UnityEngine.SceneManagement;

public class GatePiso0F : MonoBehaviour, IInteraction
{
    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;
    public void Interaction()
    {
        UiHUD.Instance?.ShowIntereactionUI(false);
        UiHUD.Instance?.LoadingPanel(true);
        SceneManager.LoadScene("Piso_1F");
    }
}
