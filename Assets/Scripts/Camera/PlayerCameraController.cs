using System.Collections;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] private float defaultDistance = -80f;
    [SerializeField] private float maxDistance = -160f;
    [SerializeField] private float minDistance = -30f;
    [SerializeField] private float scrollIncrement = 3;
    [SerializeField] private float zoomSpeed = 2f;

    [SerializeField] private Transform playerCameraTransform; // the camera that is in front of the player
    [SerializeField] private Transform parentTransform;
    private Transform cameraTargetTransform; // the transform this camera will slerp towards

    private Vector3 targetPosition;
    private Quaternion targetRotation;


    [SerializeField] private float lerpSpeed = 4f;
    [SerializeField] private float minimumLerpDistance = 0.1f;


    
    private float targetDistance;

    public static PlayerCameraController Instance {get; private set;}


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        SetTargetTransform(playerCameraTransform);
        Debug.Log("playerCameraTransform");
        Debug.Log(playerCameraTransform);

        transform.position = new Vector3(transform.position.x,transform.position.y,defaultDistance);
        targetDistance = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoomingPlayerCamera();

        MoveTowardsTargetTransform();

        

    }

    private void MoveTowardsTargetTransform()
    {
        if (cameraTargetTransform == playerCameraTransform)
        {
            LerpTowardsTargetPositionRotation(cameraTargetTransform.position,cameraTargetTransform.rotation);
        }
        else {

            // Debug.Log("Dostuff");

            // cameraTargetTransform.GetPositionAndRotation(out Vector3 tPos, out Quaternion tRot);

            // transform.GetPositionAndRotation(out Vector3 pos, out Quaternion rot);
            
            // Vector3 newPos = Vector3.Lerp(pos, tPos, Time.deltaTime * lerpSpeed);
            // Quaternion newRot = Quaternion.Slerp(rot, tRot, Time.deltaTime * lerpSpeed);

            // transform.SetPositionAndRotation(newPos,newRot);

            LerpTowardsTargetPositionRotation(cameraTargetTransform.position,cameraTargetTransform.rotation);
        }

    }

    private void LerpTowardsTargetPositionRotation(Vector3 position, Quaternion rotation)
    {
        if (Vector3.Distance(transform.position, position) < minimumLerpDistance)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lerpSpeed);
        }
    }

    private void HandleZoomingPlayerCamera()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (targetDistance < transform.position.z && scroll > 0)
        {
            targetDistance = transform.position.z;
        }
        if (targetDistance > transform.position.z && scroll < 0)
        {
            targetDistance = transform.position.z;
        }


        targetDistance += scroll * scrollIncrement;

        if (targetDistance < maxDistance)
        {
            targetDistance = maxDistance;
        }
        if (targetDistance > minDistance)
        {
            targetDistance = minDistance;
        }

        playerCameraTransform.position = new Vector3(playerCameraTransform.position.x,playerCameraTransform.position.y,targetDistance);
    }

    public Transform GetTargetTransform() {
        return cameraTargetTransform;
    }
    public void SetTargetTransform(Transform targetTransform) {


        
        cameraTargetTransform = targetTransform;

        if (targetTransform == playerCameraTransform) {
            transform.parent = parentTransform;
        } else {
            transform.parent = targetTransform;
        }

        Transform targetTransformParent = targetTransform.parent;

        targetTransform.parent = null;

        targetTransform.GetPositionAndRotation(out targetPosition,out targetRotation);

        targetTransform.parent = targetTransformParent;

        

        


        // // save the current rotation
        // Vector3 currentPosition = transform.localPosition;
        // Quaternion curerntRotation = transform.localRotation;


        // // set the parent to the target transform and set the position to zero
        // transform.parent = cameraTargetTransform;
        // transform.localPosition = Vector3.zero;
        // transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        // // reset the parent and save what local position its target should be
        // transform.parent = parentTransform;
        // targetPosition = transform.localPosition;
        // targetRotation = transform.localRotation;

        // // reset the position and rotation
        // transform.localPosition = currentPosition;
        // transform.localRotation = curerntRotation;
        
    }

    public void ReturnToPlayerCamera() {
        SetTargetTransform(playerCameraTransform);
    }
}
