using UnityEngine;

public class Key : MonoBehaviour, IInteraction
{
    [SerializeField] private Sprite _keyHudImage;

    public void IdleInteraction() { }
    public void Interacting() { }
    public void Interaction()
    {
        PlayerInput.instance.Inventory.AddItem(gameObject);
        gameObject.transform.position = new Vector3(0f, 1000f, 0f);
        gameObject.SetActive(false);
    }
}