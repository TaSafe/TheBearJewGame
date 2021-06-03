using UnityEngine;

public class Handler : MonoBehaviour, IInteraction
{
    [SerializeField] private Collider _collider;

    [Header("Indicator Mesh")]
    [SerializeField] private MeshRenderer _indicatorMeshRenderer;
    [SerializeField] private Material _materialOn;
    
    [Header("Indicator Light")]
    [SerializeField] private Light _indicatorLight;
    [SerializeField] private Color _inidcatorLighColor;

    public void Interaction()
    {
        _indicatorMeshRenderer.material = _materialOn;
        _indicatorLight.color = _inidcatorLighColor;

        _collider.enabled = false;

        ManagerSubida.Instance.HandleOn();
    }
}
