using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionTriggerType {
    collider,
    tag,
    name
}

public class CollisionTrigger : TriggerBehaviour
{

    public Collider triggeredByObject;
    public string triggeredByTag;
    public string triggeredByName;

    public CollisionTriggerType type = CollisionTriggerType.collider;

    public bool destroyOnContact = false;


    void OnTriggerEnter(Collider other) {
        if(!gameObject.activeSelf || this.isActive) return;
        
        switch(type) {
            case CollisionTriggerType.collider:
                if(other.gameObject == triggeredByObject.gameObject) {
                    this.isActive = true;
                }
                break;

            case CollisionTriggerType.tag:
                if(other.gameObject.tag == triggeredByTag) {
                    this.isActive = true;
                }
                break;

            case CollisionTriggerType.name:
                if(other.gameObject.name == triggeredByName) {
                    this.isActive = true;
                }
                break;
        }

        if(this.isActive && this.destroyOnContact) {
            GameObject.Destroy(other.gameObject);
        }
    }
}
