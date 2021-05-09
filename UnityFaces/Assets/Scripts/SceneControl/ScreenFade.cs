
//Aitor: This script is an adaptation of the OVRScreenFade class included in the Oculus SDK

using UnityEngine;
using System.Collections; // required for Coroutines

public class ScreenFade : MonoBehaviour
{
	public float fadeTime = 2.0f;
	public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

	Material fadeMaterial = null;
	private bool isFading = false;
    private bool isFaded = false;
	private YieldInstruction fadeInstruction = new WaitForEndOfFrame();

    private void Start()
    {
        EventManager.Instance.AddListener<SceneFadesIn>(CallFadeIn);
        EventManager.Instance.AddListener<SceneFadesOut>(CallFadeOut);

        fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
        //fadeMaterial = new Material(Shader.Find("Unlit/Transparent"));
        CallFadeOut(new SceneFadesOut(0.001f));
    }

    //void Awake()
    //{
    //    // create the fade material
    //    fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
    //    //fadeMaterial = new Material(Shader.Find("Unlit/Color"));
    //}

    //void OnEnable()
    //{
    //    StartCoroutine(FadeIn());
    //}

    public void CallFadeIn(SceneFadesIn sfi)
    {
        if (!isFaded || isFading) return;

        fadeTime = sfi.fadeTime;
        EventManager.Instance.TriggerEvent(new DataLogEvent("Scene fades in."));
        StartCoroutine(FadeIn());
    }
    public void CallFadeOut(SceneFadesOut sfo)
    {
        if (isFaded || isFading) return;

        fadeTime = sfo.fadeTime;
        EventManager.Instance.TriggerEvent(new DataLogEvent("Scene fades out."));
        StartCoroutine(FadeOut());
    }


    // public functions
    //public void CallFadeIn(float fTime)
    //{
    //    if (!isFaded || isFading) return;

    //    fadeTime = fTime;
    //    StartCoroutine(FadeIn());
    //}
    //public void CallFadeOut(float fTime)
    //{
    //    if (isFaded || isFading) return;

    //    fadeTime = fTime;
    //    StartCoroutine(FadeOut());
    //}


    //#if UNITY_5_4_OR_NEWER
    //	void OnLevelFinishedLoading(int level)
    //#else
    //	void OnLevelWasLoaded(int level)
    //#endif
    //	{
    //		StartCoroutine(FadeIn());
    //	}

    void OnDestroy()
    {
        if (fadeMaterial != null)
        {
            DestroyImmediate(fadeMaterial, true);
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        fadeMaterial.color = fadeColor;
        Color color = fadeColor;
        isFading = true;

        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;
        isFaded = false;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        fadeMaterial.color = fadeColor;
        //Debug.Log(fadeMaterial.color);
        Color color = fadeColor;
        isFading = true;

        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;
        isFaded = true;
    }

    void OnPostRender()
    {
        if (isFading || isFaded)
        {
            fadeMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Color(fadeMaterial.color);
            GL.Begin(GL.QUADS);
            GL.Vertex3(0f, 0f, -12f);
            GL.Vertex3(0f, 1f, -12f);
            GL.Vertex3(1f, 1f, -12f);
            GL.Vertex3(1f, 0f, -12f);
            GL.End();
            GL.PopMatrix();
        }
    }
}
