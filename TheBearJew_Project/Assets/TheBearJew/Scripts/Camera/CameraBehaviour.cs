using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour Instance { get; private set; }

    [SerializeField] LayerMask layersToHideWhenViewObstructed;
    MeshRenderer _environmentMeshToHide;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start() => StartCoroutine(HideWhenViewObstructed());

    private IEnumerator HideWhenViewObstructed()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();  //Isso é para reduzir garbage

        while (true)
        {
            Physics.Raycast(
                gameObject.transform.position,
                Player.Instance.transform.position - gameObject.transform.position,
                out RaycastHit hit,
                float.MaxValue
                );

#if UNITY_EDITOR
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 30f, Color.yellow);
#endif

            if (!hit.collider.CompareTag("Player"))
            {
                MeshRenderer newMaterial = hit.collider.gameObject.GetComponent<MeshRenderer>();

                if (newMaterial)
                {
                    _environmentMeshToHide = newMaterial;
                    _environmentMeshToHide.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
            }
            else if (_environmentMeshToHide)
            {
                _environmentMeshToHide.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                _environmentMeshToHide = null;
            }

            yield return waitForFixedUpdate;
        }
    }
}