using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void ChangeScene(string scene) => SceneManager.LoadScene(scene);

    public void Quit() => Application.Quit();
}