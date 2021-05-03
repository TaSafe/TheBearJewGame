using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdownWindowType;
    [SerializeField] private Dropdown _dropdownGraphicQuality;

    private int dropdownOldValue = 0;
    private enum Window { FULLSCREEN, BORDERLESS, WINDOW }

    private void Awake()
    {
        //_dropdownWindowType.SetValueWithoutNotify(dropdownOldValue);  //Pra isso funcionar tem que manter o valor salvo entre as cenas
        _dropdownGraphicQuality.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
    }

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

}
