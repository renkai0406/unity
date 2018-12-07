using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;

	void Awake () {
        player = GameObject.FindGameObjectWithTag(Tags.TAG_PLAYER);
	}


    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 4);
    }
}
