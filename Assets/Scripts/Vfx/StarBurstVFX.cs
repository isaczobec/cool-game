using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StarBurstVFX : MonoBehaviour
{

    [SerializeField] public float scaleFactor = 1f;

    [SerializeField] private GameObject vfxObject;

    // Start is called before the first frame update
    public void Setup(float angle)
    {


        vfxObject.SetActive(true);

        
        Debug.Log(new Vector3(0,0,angle));

        transform.localScale = Vector3.one * scaleFactor;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));

    }

    
}
