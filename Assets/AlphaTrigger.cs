using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTrigger : MonoBehaviour
{
    public string targetName;
    public float alphaIn = 1f;
    public float alphaOut = 0f;
    public float transitionDuration = 1f;

    private Material mat;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        float t = transitionDuration;
        transitionDuration = 0;

        target = GameObject.Find(targetName).transform;
        if((transform.position - target.position).magnitude < GetComponent<SphereCollider>().radius) {
            SetAlpha(alphaIn);
        } else {
            SetAlpha(alphaOut);
        }

        transitionDuration = t;
    }

    void OnTriggerExit(Collider other) {
        if(other.transform == target) {
            StartCoroutine(FadeColor(alphaOut));
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.transform == target) {
            StartCoroutine(FadeColor(alphaIn));
        }
    }

    IEnumerator FadeColor(float to) {
        float t = transitionDuration;

        Color a = mat.color;
        Color b = mat.color;
        b.a = to;

        do {
            t -= Time.deltaTime;
            SetAlpha(Mathf.Lerp(a.a, to, Mathf.Clamp01(1 - t / transitionDuration)));

            yield return new WaitForEndOfFrame();
        } while (t > 0);

        yield return null;
    }

    void SetAlpha(float value) {
        Color c = mat.color;
        c.a = value;
        mat.color = c;
    }
}
