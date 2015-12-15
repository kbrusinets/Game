using UnityEngine;
using System.Collections;

public class PistolWeapon : Weapon {

    public Sprite bullet;

    public override float getBulletSpeed()
    {
        return 12f;
    }

    public override float getReloadTime()
    {
        return 0.4f;
    }

    public override Sprite bulletSprite()
    {
        return bullet;
    }
}
