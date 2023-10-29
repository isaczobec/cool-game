using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittableEntity
{
    
    public void GetHit(HitInfo hitInfo);

    public void SetInvincibilityTime(float Time);
    public float GetInvincibilityTime();

}
