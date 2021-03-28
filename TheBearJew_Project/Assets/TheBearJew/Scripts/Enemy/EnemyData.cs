using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Game Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _life;

    public string Name { get { return _name; } }
    public float Life { get { return _life; } }

}