namespace Playerone.Unity { 
    using System.Collections;
    using System.Collections.Generic;
    using Playerone.Game;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;


    public class TerrainCuttingEditor : Editor {

        private static bool onlyStatic = true;

        private static TerrainCutConfig config;

        private static GameMng gameMng;


        [MenuItem(" PO/Terrain/Cutting")]
        private static void CuttingTerrain()
        {
            config = GameObject.FindGameObjectWithTag(Tag.TerrainTerrainCutMng).GetComponent<TerrainCutConfig>();
            gameMng = GameObject.FindGameObjectWithTag(Tag.GameMng).GetComponent<GameMng>();

            //遍历场景，获取含有Mesh的物体
            GameObject[] roots = EditorSceneManager.GetActiveScene().GetRootGameObjects();
            List<GameObject> objDealing = new List<GameObject>();
            foreach(GameObject root in roots)
            {
                travelGameObjects(root, objDealing);
            }

            for (int i = 0; i < objDealing.Count; i++)
            {
                ClassifyGameObject(objDealing[i]);
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
            Transform rootTrans = root.transform;

            for(int i = 0; i < rootTrans.childCount; i++)
            {
                travelGameObjects(rootTrans.GetChild(i).gameObject, objDealing);
            }
        }

        private static void ClassifyGameObject(GameObject obj)
        {
            Vector3 pos = obj.transform.position;
            int width = config.ChunkInfo.width;
            int height = config.ChunkInfo.height;
            string preName = config.ChunkInfo.namePrefix;
            // chunk的索引
            int targetChunkX = (int)(pos.x / width) + 1;
            int targetChunkZ = (int)(pos.z / height) + 1;
            string chunkName = preName + string.Format("{0}_{1}", targetChunkX, targetChunkZ);
            GameObject chunkRoot = GameObject.Find(chunkName);
            if (chunkRoot == null)
            {
                chunkRoot = new GameObject(chunkName);
            }

            //复制层次关系到Chunk的节点下面
            GameObject tempObj = obj;
            List<GameObject> objs2Copy = new List<GameObject>();
            while (tempObj.transform.parent)
            {
                objs2Copy.Add(tempObj.transform.parent.gameObject);
                tempObj = tempObj.transform.parent.gameObject;
            }
            tempObj = chunkRoot;
            for (int i = objs2Copy.Count - 1; i > -1; --i)
            {
                GameObject targetObj = objs2Copy[i];
                // 对于符合Chunk命名规则的父节点不进行拷贝过程。
                if (targetObj.name.StartsWith(preName))
                {
                    continue;
                }
                Transform parent = tempObj.transform.FindChild(targetObj.name);
                if (parent == null)
                {
                    parent = new GameObject(targetObj.name).transform;
                    CopyComponents(targetObj, parent.gameObject);
                    parent.parent = tempObj.transform;
                    targetObj = parent.gameObject;
                }
                tempObj = parent.gameObject;
            }
            Transform tempParent = obj.transform.parent;
            obj.transform.parent = tempObj.transform;
            // 移动完毕之后发现父节点没有孩子节点的情况下，向上遍历将无用节点删除。
            while (tempParent != null && tempParent.childCount == 0)
            {
                Transform temp = tempParent.parent;
                Object.Destroy(tempParent.gameObject);
                tempParent = temp;
            }
        }

        public static void CopyComponents(GameObject org, GameObject tar)
        {
            foreach (Component com in org.GetComponents<Component>())
            {
                var newCom = tar.AddComponent(com.GetType());
                UnityEditorInternal.ComponentUtility.CopyComponent(com);
                UnityEditorInternal.ComponentUtility.PasteComponentValues(newCom);
            }
        }
    }

}
