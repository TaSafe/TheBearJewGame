using System;
using UnityEngine;

public class GunPick : MonoBehaviour, IInteraction
{

    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public void IdleInteraction()
    {
        UiInteraction.instance.ShowUi(false);
    }

    public void Interacting()
    {
        UiInteraction.instance.ShowUi(true);
    }

    public void Interaction()
    {
        UiInteraction.instance.ShowUi(false);
        AttachGunToHand();
    }

    private void AttachGunToHand()
    {
        var child = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Transform>();
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

    public void DropGun()
    {
        gameObject.transform.parent = null;
    }

}
