using UnityEngine;

public class Handler : MonoBehaviour, IInteraction
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _handle;
    [SerializeField] private GameObject _vfxInteraction;
    [FMODUnity.EventRef] [SerializeField] private string _handleTurningSound;

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;

    public void Interaction()
    {
        _handle.transform.Rotate(new Vector3(0f, 180f, 0f));
        _vfxInteraction.SetActive(false);
        _collider.enabled = false;

        FMODUnity.RuntimeManager.PlayOneShot(_handleTurningSound);

        ManagerSubida.Instance.HandleOn();
    }
}
