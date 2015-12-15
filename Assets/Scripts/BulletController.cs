using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        player = playerControllerObject.GetComponent<PlayerController>();
        GetComponent<SpriteRenderer>().sprite = player.getBulletSprite();
	}
}
