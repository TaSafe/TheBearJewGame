using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPJDois : MonoBehaviour
{
    public static GameManagerPJDois instance;

    [SerializeField] private int testInt;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
