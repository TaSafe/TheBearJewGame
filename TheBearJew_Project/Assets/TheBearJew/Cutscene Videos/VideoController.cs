using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public static VideoController Instance { get; private set; }

    [SerializeField] private GameObject _videoPanel;
    [SerializeField] private VideoClip _video;
    [SerializeField] private Slider sliderJumpProgress;
    [SerializeField] private float jumpTimeTime;
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

    public void VideoActivate()
    {
        PlayerInput.Instance?.SetVideo(true);
        _videoPanel.SetActive(true);
    }

    private void VideoHasEnded(VideoPlayer source)
    {
        PlayerInput.Instance?.SetVideo(false);
        OnVideoEnd?.Invoke();
        _videoPanel.SetActive(false);
    }

    public void JumpVideo()
    {
        if (sliderJumpProgress.value < sliderJumpProgress.maxValue)
            sliderJumpProgress.value += (1f / jumpTimeTime) * Time.deltaTime;
        else
            _videoPanel.GetComponentInChildren<VideoPlayer>().time = _videoPanel.GetComponentInChildren<VideoPlayer>().clip.length;
    }

    public void JumpVideoReset() => sliderJumpProgress.value = 0f;

}