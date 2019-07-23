namespace Playerone.Game
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameMng : MonoBehaviour
    {

        private void Awake()
        {
            var manager = GameObject.FindGameObjectWithTag(Tag.Manager);
            Object.DontDestroyOnLoad(manager);
        }

        public void DestoryObject(GameObject obj)
        {
            Destroy(obj);
        }
    }
}