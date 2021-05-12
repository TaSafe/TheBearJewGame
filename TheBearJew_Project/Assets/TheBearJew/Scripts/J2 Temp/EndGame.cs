using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.MoveGameObjectToScene(PlayerInput.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(CameraDontDestroy.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(GameStatus.Instance.gameObject, SceneManager.GetActiveScene());

        SceneManager.LoadScene("MenuFinal");
    }

}
