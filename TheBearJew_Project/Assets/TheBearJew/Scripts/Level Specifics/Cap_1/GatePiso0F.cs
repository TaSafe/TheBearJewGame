using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatePiso0F : MonoBehaviour, IInteraction
{

    public void Interaction()
    {
        SceneManager.LoadScene("Piso_1F");
        UiHUD.instance?.ShowIntereactionUI(false);
    }
}
