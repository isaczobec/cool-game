using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] private float defaultDistance = -80f;
    [SerializeField] private float maxDistance = -160f;
    [SerializeField] private float minDistance = -30f;
    [SerializeField] private float scrollIncrement = 3;
    [SerializeField] private float zoomSpeed = 2f;


    
    private float targetDistance;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,defaultDistance);
        targetDistance = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (targetDistance < transform.position.z && scroll > 0) {
            targetDistance = transform.position.z;
        }
        if (targetDistance > transform.position.z && scroll < 0) {
            targetDistance = transform.position.z;
        }


        targetDistance += scroll * scrollIncrement;

        if (targetDistance < maxDistance) {
            targetDistance = maxDistance;
        }
        if (targetDistance > minDistance) {
            targetDistance = minDistance;
        }

        transform.position = new Vector3(transform.position.x,transform.position.y,Mathf.Lerp(transform.position.z,targetDistance,zoomSpeed * Time.deltaTime));

        
    }
}
