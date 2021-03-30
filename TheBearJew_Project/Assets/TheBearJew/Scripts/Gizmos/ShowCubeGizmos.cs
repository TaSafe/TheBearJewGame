using UnityEngine;

public class ShowCubeGizmos : MonoBehaviour
{

    [SerializeField] private Collider _collider;
    [SerializeField] private Color _color;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawCube(_collider.bounds.center, _collider.bounds.size);

        Gizmos.color = new Color(_color.r, _color.g, _color.b, _color.a + ((_color.a * 20f) / 100f));
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}