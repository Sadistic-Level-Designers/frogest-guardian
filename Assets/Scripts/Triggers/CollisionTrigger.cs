﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : TriggerBehaviour
{
    public Collider triggeredBy;

    void OnTriggerEnter(Collider other) {
        if(!gameObject.activeSelf) return;
        
        if(other.gameObject == triggeredBy.gameObject) {
            this.isActive = true;
        }
    }
}
