using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class Singleton : MonoBehaviour
{
    static Singleton instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        var systems = FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();
        foreach (var x in systems.Skip(1).ToArray())
            Destroy(x.gameObject);
    }
}
