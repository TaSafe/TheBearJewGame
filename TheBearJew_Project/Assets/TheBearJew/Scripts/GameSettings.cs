using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdownWindowType;
    [SerializeField] private Dropdown _dropdownGraphicQuality;
    [SerializeField] private Dropdown _dropdownResolution;
    
    [Header("Cursor")]
    [SerializeField] private Texture2D cursorTexture;

    private int dropdownOldValue = 0;
    private enum Window { FULLSCREEN, BORDERLESS, WINDOW }

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2, cursorTexture.height / 2), CursorMode.Auto);
        _dropdownGraphicQuality?.SetValueWithoutNotify(QualitySettings.GetQualityLevel());

        _dropdownResolution.options.Clear();
        _dropdownResolution.options.Capacity = Screen.resolutions.Length;

        int currentResolutionIndex = 0;

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var newOption = new Dropdown.OptionData(Screen.resolutions.GetValue(i).ToString());
            if (!_dropdownResolution.options.Contains(newOption))
                _dropdownResolution.options.Add(newOption);

            if (Screen.resolutions.GetValue(i).ToString() == Screen.currentResolution.ToString())
                currentResolutionIndex = i;
        }

        _dropdownResolution.SetValueWithoutNotify(currentResolutionIndex);
    }

    public void ChangeScene(string scene) => SceneManager.LoadScene(scene);

    public void WindowType(int value)
    {
        if (value != dropdownOldValue)
        {
            switch (value)
            {
                case (int)Window.FULLSCREEN:
                    if (Screen.fullScreen) return;
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case (int)Window.BORDERLESS:
                    if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow) return;
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
                case (int)Window.WINDOW:
                    if (Screen.fullScreenMode == FullScreenMode.Windowed) return;
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            }

            dropdownOldValue = value;
        }
    }

    public void QualityChange(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    public void Resolution(int value)
    {
        Resolution resolution = Screen.resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Quit() => Application.Quit();

}
