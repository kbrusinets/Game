using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    private Rigidbody2D rb;
    private Transform tr;
    private float speed;
    private GameController gameController;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        player = playerControllerObject.GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody2D>();
        if (tag == "Bullet")
        {
            speed = player.getBulletSpeed();
        }
        else
        speed = gameController.returnSpeed();

        tr = GetComponent<Transform>();
        rb.velocity = tr.rotation*Vector3.right * speed;
	}
}
