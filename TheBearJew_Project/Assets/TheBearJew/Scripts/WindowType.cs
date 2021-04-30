using UnityEngine;

public class WindowType : MonoBehaviour
{
    private float dropdownOldValue;

    private enum Window { FULLSCREEN, BORDERLESS, WINDOW }

    public void DropdownValueChange(int value)
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
}
