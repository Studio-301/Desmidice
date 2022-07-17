using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] LaserReciever_Activator[] activators;

    private void Awake()
    {
        foreach (var x in activators)
        {
            x.Activate.AddListener(OnActivatorChanged);
            x.Deactivate.AddListener(OnActivatorChanged);
        }
    }
    public void OnActivatorChanged()
    {
        if (activators.All(x => x.IsCompleate))
            OpenNextLevel();
    }

    bool opening;
    public void OpenNextLevel()
    {
        if (opening)
            return;

        opening = true;
        FindObjectOfType<MapManager>()?.Next();
    }
}
