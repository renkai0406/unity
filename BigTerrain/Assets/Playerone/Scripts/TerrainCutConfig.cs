namespace Playerone.Unity {

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TerrainCutConfig : MonoBehaviour
    {
        [Serializable]
        public struct Chunk
        {
            public int width;
            public int height;
            public String namePrefix;
        }


        [SerializeField]
        private bool onlyStatic = true;

        [SerializeField]
        private Chunk chunk;

        public Chunk ChunkInfo
        {
            get { return chunk; }
        }

        public bool OnlyStatic
        {
            get { return onlyStatic; }
        }
    }
}