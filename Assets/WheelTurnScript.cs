using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurnScript : MonoBehaviour
{
    public Transform[] wheels;

    Vector2 oldPos;

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 dir = (oldPos - newPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle = angle * -1 + 90;

        foreach(Transform w in wheels) {
            w.localEulerAngles = new Vector3(0, dir.sqrMagnitude > 0 ? angle : 0, 0);
        }

        oldPos = newPos;
    }
}
