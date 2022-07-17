using System.Collections;
using System.Collections.Generic;
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
            Destroy(this);
    }
}
