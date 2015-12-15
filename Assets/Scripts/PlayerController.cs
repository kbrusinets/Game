using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public Weapon weapon;
    public GameObject bullet;

    private GameObject weaponPattern;
    private GameController gameController;
    private float time;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();

        weaponPattern = GameObject.FindWithTag("Weapon");
        weapon = Instantiate(gameController.returnWeapon()) as Weapon;
        weapon.transform.parent = weaponPattern.transform;
        weapon.transform.localPosition = new Vector3(0, 0, 0);
       // GameObject weaponObject = GameObject.FindWithTag("StoneWeapon");
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (gameController.returnWeapon().tag != weapon.tag)
        {
            Destroy(GameObject.FindWithTag(weapon.tag));
            weapon = Instantiate(gameController.returnWeapon()) as Weapon;
            weapon.transform.parent = weaponPattern.transform;
            weapon.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Shoot") && time > weapon.getReloadTime())
        {
            Instantiate(bullet, weapon.transform.position, weapon.transform.rotation);
            time = 0.0f;
        }
    }

    public Sprite getBulletSprite()
    {
        return weapon.bulletSprite();
    }

    public float getBulletSpeed()
    {
        return weapon.getBulletSpeed();
    }
}