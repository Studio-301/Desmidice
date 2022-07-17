using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetLayerRecursively(this GameObject gameobject, int layer)
    {
        gameobject.layer = layer;
        foreach (Transform child in gameobject.transform)
            child.gameObject.SetLayerRecursively(layer);
    }

    public static void SetLayer(this GameObject gameobject, int layer)
    {
        gameobject.layer = layer;
    }

    public static T GetMandatoryComponent<T>(this GameObject gameobject)
    {
        if (gameobject.TryGetComponent<T>(out var value))
            return value;
        else
            throw new System.Exception($"Component {typeof(T).Name} was not present on {gameobject.name}");
    }
    public static T GetMandatoryComponentInChildren<T>(this GameObject gameobject)
    {
        var value = gameobject.GetComponentInChildren<T>();
        if (value != null)
            return value;
        else
            throw new System.Exception($"Component {typeof(T).Name} was not present on {gameobject.name}");
    }
}
