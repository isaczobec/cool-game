using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{

    [SerializeField] private GameObject playerVisualObject;

    GameObject playerAvatarObject;

    [SerializeField] private RuntimeAnimatorController playerAvatarAnimatorController;
    [SerializeField] private Avatar avatarMask;


    [SerializeField] private float playerScaleFactor;
    [SerializeField] private Vector3 playerRotation;


    [SerializeField] private string targetLayerName;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup() {
        playerAvatarObject = Instantiate(playerVisualObject,transform);

        Animator playerAnimator = playerAvatarObject.AddComponent<Animator>();
        playerAnimator.runtimeAnimatorController = playerAvatarAnimatorController;
        playerAnimator.avatar = avatarMask;

        playerAvatarObject.transform.localPosition = Vector3.zero;
        playerAvatarObject.transform.localEulerAngles = playerRotation;
        playerAvatarObject.transform.localScale = Vector3.one * playerScaleFactor;

        foreach (Transform child in playerAvatarObject.transform) {
            child.gameObject.layer = LayerMask.NameToLayer(targetLayerName);

            
        }

    }

}
