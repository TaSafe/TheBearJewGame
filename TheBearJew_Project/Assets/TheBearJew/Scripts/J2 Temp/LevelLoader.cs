using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string _levelName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadLevel(_levelName);
    }

    void LoadLevel(string levelName) => SceneManager.LoadScene(levelName);

}
