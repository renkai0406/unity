using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace playerone.unity { 

    public class TerrainCuttingEditor : Editor {

        private static bool onlyStatic = true;

        [MenuItem("Terrain/Cutting")]
        private static void CuttingTerrain()
        {
            GameObject[] roots = EditorSceneManager.GetActiveScene().GetRootGameObjects();
            List<GameObject> objDealing = new List<GameObject>();
            foreach(GameObject root in roots)
            {
                travelGameObjects(root, objDealing);
            }
        }

        private static void travelGameObjects(GameObject root, List<GameObject> objDealing)
        {
            if (!root.activeSelf)
            {
                return;
            }

            if ((root.GetComponent<MeshRenderer>() || root.GetComponent<Terrain>()) && (!onlyStatic || (onlyStatic && root.isStatic)))
            {
                objDealing.Add(root);
                return;
            }
        }
    }

}
