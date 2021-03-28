using UnityEngine;

public class LifeSystem
{
    public float CurrentLife { get; private set; } 

    public LifeSystem(float totalLife)
    {
        CurrentLife = totalLife;
    }

    public void AddLife(float valueToAdd)
    {
        if (valueToAdd < 0)
        {
            Debug.LogError("LifeSystem.AddLife() : Valor de ser maior que zero."); 
            return;
        }
        else
            CurrentLife += valueToAdd;
    }

    public void RemoveLife(float valueToRemove)
    {
        if (valueToRemove < 0)
        {
            Debug.LogError("LifeSystem.RemoveLife() : Valor de ser maior que zero.");
            return;
        }
        else
            CurrentLife -= valueToRemove;
    }

    public bool DeathCheck()
    {
        if (CurrentLife <= 0)
            return true;
        else
            return false;
    }

}
