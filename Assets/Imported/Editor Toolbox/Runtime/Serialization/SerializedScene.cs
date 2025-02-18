﻿using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine
{
    /// <summary>
    /// Custom serializable class used to serialize the <see cref="SceneAsset"/> class.
    /// </summary>
    [Serializable]
    public class SerializedScene : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        private readonly static Dictionary<SceneAsset, int> cachedBuildIndexes = new Dictionary<SceneAsset, int>();

        [SerializeField]
        private SceneAsset sceneReference;
#endif
        [SerializeField, HideInInspector]
        private string sceneName;
        [SerializeField, HideInInspector]
        private string scenePath;
        [SerializeField, HideInInspector]
        private int buildIndex;


        void ISerializationCallbackReceiver.OnAfterDeserialize()
        { }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
#if UNITY_EDITOR
            TryGetBuildIndex(sceneReference, out buildIndex);
            TryGetSceneName(sceneReference, out sceneName);
            TryGetScenePath(sceneReference, out scenePath);
#endif
        }


#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            UpdateAllIndexes();

            EditorBuildSettings.sceneListChanged -= UpdateAllIndexes;
            EditorBuildSettings.sceneListChanged += UpdateAllIndexes;
        }

        private static void UpdateAllIndexes()
        {
            cachedBuildIndexes.Clear();

            var buildIndex = -1;
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    buildIndex++;
                    var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                    if (sceneAsset != null)
                    {
                        cachedBuildIndexes.Add(sceneAsset, buildIndex);
                    }
                }
            }
        }

        private static bool TryGetBuildIndex(SceneAsset sceneAsset, out int index)
        {
            if (sceneAsset == null || !cachedBuildIndexes.TryGetValue(sceneAsset, out index))
            {
                index = -1;
                return false;
            }

            return true;
        }

        private static bool TryGetSceneName(SceneAsset sceneAsset, out string name)
        {
            if (sceneAsset == null)
            {
                name = null;
                return false;
            }

            name = sceneAsset.name;
            return true;
        }

        public static bool TryGetScenePath(SceneAsset sceneAsset, out string path)
        {
            if (sceneAsset == null)
            {
                path = null;
                return false;
            }

            path = AssetDatabase.GetAssetPath(sceneAsset);
            return true;
        }


        public SceneAsset SceneReference
        {
            get => sceneReference;
            set => sceneReference = value;
        }
#endif

        public string SceneName
        {
            get => sceneName;
            set => sceneName = value;
        }

        public string ScenePath
        {
            get => scenePath;
            set => scenePath = value;
        }

        public int BuildIndex
        {
            get => buildIndex;
            set => buildIndex = value;
        }
    }
}