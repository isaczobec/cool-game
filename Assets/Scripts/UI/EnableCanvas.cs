using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Canvas>(out Canvas canvas)) {
            if (canvas != null) {
            canvas.enabled = true;
            }
        }
    }

}
