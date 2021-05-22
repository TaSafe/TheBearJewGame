using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public static VideoController Instance { get; private set; }

    [SerializeField] private GameObject _videoPanel;
    [SerializeField] private VideoClip _video;
    [SerializeField] private UnityEvent OnVideoEnd;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        VideoPlayer videoPlayer = _videoPanel.GetComponentInChildren<VideoPlayer>();
        videoPlayer.clip = _video;
        videoPlayer.loopPointReached += VideoHasEnded;
    }

    private void OnDisable()
    {
        _videoPanel.GetComponentInChildren<VideoPlayer>().loopPointReached -= VideoHasEnded;
    }

    private void VideoHasEnded(VideoPlayer source)
    {
        OnVideoEnd?.Invoke();
        _videoPanel.SetActive(false);
    }

    public void VideoActivate()
    {
        PlayerInput.Instance?.DisableInput();
        _videoPanel.SetActive(true);
    }
}