using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    // First model
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private Material phaseMaterial;


    // Second Model
    [SerializeField] GameObject secondModel;
    [SerializeField] private Renderer _secondRenderer;
    [SerializeField] private Material secondModelOriginalMaterial;
    [SerializeField] private Material secondModelPhaseMaterial;


    [SerializeField] private float fadeTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _renderer.material = phaseMaterial;
        DoFade(0, 2, fadeTime);
    }

    private void DoFade(float start, float dest, float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", start, "to", dest, "time", time, "onupdatetarget", gameObject,
            "onupdate", "TweenOnUpdate", "oncomplete", "TweenOnComplete", "easetype", iTween.EaseType.easeInOutCubic
            ));
    }

    void TweenOnUpdate(float value)
    {
        _renderer.material.SetFloat("_SplitValue", value);
        _secondRenderer.material.SetFloat("_SplitValue", value);
    }

    void TweenOnComplete()
    {
        _renderer.material = originalMaterial;
        //_secondRenderer.material = originalMaterial;
    }
}
