using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.MoveGameObjectToScene(PlayerInput.instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(CameraDontDestroy.instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(GameManagerPJDois.instance.gameObject, SceneManager.GetActiveScene());

        SceneManager.LoadScene("MenuFinal");
    }

}
