using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class WartBrightness : MonoBehaviour
{
    public float fadeTime = 0.6f;
    public Material wartMat;
    public Material skinMat;
    [ColorUsage(true, true)]
    private Color baseWartColor;
    private Color baseSkinColor;
    public Color finalSkinColor;
    public float maxBrightness;

    public float minSkinBrightness = 8f;
    public float maxSkinBrightness = 32f;

    // Start is called before the first frame update
    void Start()
    {
        wartMat = transform.Find("Frog/FrogLow").gameObject.GetComponent<Renderer>().materials[1];
        skinMat = transform.Find("Frog/FrogLow").gameObject.GetComponent<Renderer>().materials[0];

        baseWartColor = wartMat.GetColor("_EmissionColor");
        baseSkinColor = skinMat.GetColor("_SSSColor");
    }

    private float stomachPrev;
    void Update()
    {
        float stomach = Variables.Object(this).Get<float>("StomachFullness");

        if(stomach != stomachPrev) {
            float factor = stomach * maxBrightness;

            Color wart = new Color(baseWartColor.r * factor, baseWartColor.g*factor, baseWartColor.b * factor, baseWartColor.a);
            StartCoroutine(FadeColor(wartMat, "_EmissionColor", wartMat.GetColor("_EmissionColor"), wart, fadeTime));

            Color skin = Color.Lerp(baseSkinColor, finalSkinColor, stomach);
            float skinScale = Mathf.Lerp(minSkinBrightness, maxSkinBrightness, stomach);
            StartCoroutine(FadeFloat(skinMat, "_SSSScale", skinMat.GetFloat("_SSSScale"), skinScale, fadeTime));
            StartCoroutine(FadeColor(skinMat, "_SSSColor", skinMat.GetColor("_SSSColor"), skin, fadeTime));
        }

        stomachPrev = stomach;
    }

    IEnumerator FadeColor(Material mat, string prop, Color a, Color b, float time) {
        float c = time;
        do {
            time -= Time.deltaTime;

            mat.SetColor(prop, Color.Lerp(a, b, 1f - time / c));

            yield return new WaitForEndOfFrame();
        } while (time > 0);

        yield return null;
    }

    IEnumerator FadeFloat(Material mat, string prop, float a, float b, float time) {
        float c = time;
        do {
            time -= Time.deltaTime;

            mat.SetFloat(prop, Mathf.Lerp(a, b, 1f - time / c));

            yield return new WaitForEndOfFrame();
        } while (time > 0);

        yield return null;
    }
}
