using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatePiso0F : MonoBehaviour, IInteraction
{
    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;
    public void Interaction()
    {
        SceneManager.LoadScene("Piso_1F");
        UiHUD.Instance?.ShowIntereactionUI(false);
    }
}
