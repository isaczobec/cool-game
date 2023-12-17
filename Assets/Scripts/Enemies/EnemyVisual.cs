using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    //class with a bunch of methods that are used for a bunch of enemies

    

    [SerializeField] public Enemy enemy;
    public Player player;

    private void Start() {
        player = Player.Instance;
        InitializeVisual();
    }

    /// <summary>
    /// Base class method that is ran when the script is started.
    /// </summary>
    public virtual void InitializeVisual() {

    }


    public void FacePlayer(float spinSpeed, float minLerpAngle, float rightAngle = 90, float leftAngle = 270f) {

        Vector3 targetRotation;
        Vector3 oldRotation = transform.rotation.eulerAngles;

        if (player.transform.position.x > enemy.transform.position.x) {

            targetRotation = new Vector3(oldRotation.x,rightAngle,oldRotation.z);
        } else {
            targetRotation = new Vector3(oldRotation.x,leftAngle,oldRotation.z);
        }


        transform.rotation = Quaternion.Euler(Vector3.Slerp(oldRotation,targetRotation,spinSpeed * Time.deltaTime));

        /*
        if (Vector3.Angle(transform.rotation.eulerAngles.normalized,targetRotation.normalized) < minLerpAngle) {
            transform.rotation = Quaternion.Euler(targetRotation);
        } */ //no idea why this doesnt work
        
    }




}
