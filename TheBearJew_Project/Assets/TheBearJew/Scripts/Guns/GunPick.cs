using UnityEngine;

public class GunPick : MonoBehaviour, IInteraction
{
    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public void IdleInteraction()
    {
    }

    public void Interacting()
    {
    }

    public void Interaction()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var child = player.GetComponentsInChildren<Transform>();
        GameObject neededChild = null;
        for (int i = 0; i < child.Length; i++)
        {
            if (child[i].Find("swat:RightHand") != null)
            {
                neededChild = child[i].gameObject;
                break;
            }
        }
        if (neededChild != null)
        {
            gameObject.transform.SetParent(neededChild.transform);
            gameObject.transform.localPosition = inHandPos;
            gameObject.transform.localEulerAngles = inHandRot;
        }
    }

}
