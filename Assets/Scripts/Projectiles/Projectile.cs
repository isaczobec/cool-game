using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        UpdateProjectile();
    }

    public virtual void UpdateProjectile() {

    }

    public virtual HitInfo GetHitInfo() { // placeholder method that will be replaced in each individual subclass
        return new HitInfo();
    }

}
