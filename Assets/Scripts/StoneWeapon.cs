using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class StoneWeapon : Weapon {

    public Sprite bullet;
   /* Rigidbody2D rb;

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () 
    {
        Vector2 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0.0f);
        float cosine = Vector3.Dot(moveVec, Vector3.right);
        float angle = Mathf.Rad2Deg * Mathf.Acos(cosine) * Mathf.Sign(Vector3.Cross(Vector3.right, moveVec).z);
        rb.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    */

    public override float getBulletSpeed()
    {
        return 6.0f;
    }

    public override float getReloadTime()
    {
        return 0.7f;
    }

    public override Sprite bulletSprite()
    {
        return bullet;
    }
}
