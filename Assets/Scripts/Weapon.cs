using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public abstract class Weapon : MonoBehaviour {

    Rigidbody2D rb;
	
    void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        float angle = 0;
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0.0f);
        if (moveVec != Vector3.zero)
        {
            float cosine = Vector3.Dot(moveVec, Vector3.right);
            angle = Mathf.Rad2Deg * Mathf.Acos(cosine) * Mathf.Sign(Vector3.Cross(Vector3.right, moveVec).z);
        }
            
        rb.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public abstract float getBulletSpeed();
    public abstract float getReloadTime();
    public abstract Sprite bulletSprite();
}
