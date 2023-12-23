using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeLineGroups : MonoBehaviour {
    [SerializeField] private DialougeLineGroup[] dialogeLineGroups;

    public void QueueDialougeLines(string groupName, bool playInstantly = false) {
        DialougeLineGroup group = Array.Find(dialogeLineGroups,group => group.dialougeGroupName == groupName);

        if (group != null) {
            
            DialougeBubble.Instance.AddDialougeLinesToQueue(group.dialougeLines,playInstantly);
    }
        }
        
    }


