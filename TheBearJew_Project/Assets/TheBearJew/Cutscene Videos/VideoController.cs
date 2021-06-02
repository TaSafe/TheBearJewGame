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

    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        
    }

    private void OnEnable()
    {
        _videoPlayer = _videoPanel.GetComponentInChildren<VideoPlayer>();
        _videoPlayer.clip = _video;
        _videoPlayer.loopPointReached += VideoHasEnded;
        
    }

    private void OnDisable() => _videoPlayer.loopPointReached -= VideoHasEnded;

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

    //Para mudar o vídeo em runtime
    public void ChangeVideo(VideoClip video)
    {
        _videoPlayer.clip = video;
        _videoPlayer.time = 0f;
    }

    public void JumpVideo()
    {
        if (sliderJumpProgress.value < sliderJumpProgress.maxValue)
            sliderJumpProgress.value += (1f / jumpTimeTime) * Time.deltaTime;
        else
            _videoPlayer.time = _videoPlayer.clip.length;
    }

    public void JumpVideoReset() => sliderJumpProgress.value = 0f;

}