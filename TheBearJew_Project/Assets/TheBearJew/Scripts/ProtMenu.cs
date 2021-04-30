using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtMenu : MonoBehaviour
{
    public GameObject _gameMenu;

    public void ChangeScene(string scene) => SceneManager.LoadScene(scene);

    public void ExitGame() => Application.Quit();

    #region IN-GAME
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Level_01" && _gameMenu.activeSelf == false)
        {
            Time.timeScale = 0f;
            _gameMenu.SetActive(true);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
    }
    #endregion
}
