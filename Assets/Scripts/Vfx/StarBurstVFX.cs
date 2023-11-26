using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StarBurstVFX : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup()
    {
        Vector3 origDirVec = MouseInfo.GetPlayerMouseDirectionVector();
        Vector3 dirVec = MouseInfo.GetPlayerMouseDirectionVector();

        if (dirVec.x >= 0) {
            dirVec = new Vector3(dirVec.y,-dirVec.x,0f);
        } else {
            dirVec = new Vector3(-dirVec.y,dirVec.x,0f);
        }


        Vector3 lookAtVec = transform.position + dirVec;
        transform.LookAt(lookAtVec);

        
        if (origDirVec.y <= 0) {
            
            transform.rotation = Quaternion.Inverse(transform.rotation);
        }
        
    }

    
}
