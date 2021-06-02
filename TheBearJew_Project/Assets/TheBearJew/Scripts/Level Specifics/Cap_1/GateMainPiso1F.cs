using UnityEngine;

public class GateMainPiso1F : Gate
{
    [SerializeField] private DialogueSequence dialogueDontHaveKey;

    [SerializeField] private Transform gateSideL;
    [SerializeField] private Transform gateSideR;
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _particle;

    public override void Interaction() => GateActions();

    public override void GateActions()
    {
        if (CheckKeyInPlayerInventary())
        {
            GameStatus.Instance.HasOpenedEndGate = true;
            RemoveKeyFromPlayerInventary();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Eventos/portao_aberto");
            GateInGameFeedback();
            return;
        }
        else
        {
            DialogueSystem.Instance.DialogueChanger(dialogueDontHaveKey);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Eventos/portao_fechado");
        }
    }

    public void GateInGameFeedback()
    {
        gateSideL.Rotate(Vector3.up, -90f);
        gateSideR.Rotate(Vector3.up, 90f);

        _lock.SetActive(false);
        _particle.SetActive(false);

        GetComponent<Collider>().enabled = false;
    }
}