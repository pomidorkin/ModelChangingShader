using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    // First model
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material originalMaterial;
    //[SerializeField] private Material dissolveMaterial;
    [SerializeField] private Material phaseMaterial;


    // Second Model
    [SerializeField] GameObject secondModel;
    [SerializeField] private Renderer _secondRenderer;
    [SerializeField] private Material secondModelOriginalMaterial;
    [SerializeField] private Material secondModelPhaseMaterial; // Reversed shader material


    [SerializeField] private float fadeTime = 2f;

    [SerializeField] bool TEST_BUTTON_DELETE_ME = false;

    private bool fadeBackwards = false;
    // Start is called before the first frame update
    void Start()
    {
        _renderer.material = phaseMaterial;
        DoFade(0, 2, fadeTime);
    }

    private void DoFade(float start, float dest, float time)
    {
        if (fadeBackwards)
        {
            _renderer.material = secondModelPhaseMaterial;
            _secondRenderer.material = phaseMaterial;
        }
        else
        {
            _secondRenderer.material = secondModelPhaseMaterial;
            _renderer.material = phaseMaterial;

        }
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", start, "to", dest, "time", time, "onupdatetarget", gameObject,
            "onupdate", "TweenOnUpdate", "oncomplete", "TweenOnComplete", "easetype", iTween.EaseType.easeInOutCubic
            ));
        //fadeBackwards = true;
    }

    void TweenOnUpdate(float value)
    {
        _renderer.material.SetFloat("_SplitValue", value);
        _secondRenderer.material.SetFloat("_SplitValue", value);
    }

    void TweenOnComplete()
    {
        if (!fadeBackwards)
        {
            _renderer.material = originalMaterial;
            //_secondRenderer.material = 
        }
        else
        {
            _secondRenderer.material = originalMaterial;
        }
        
        //_secondRenderer.material = originalMaterial;
        fadeBackwards = !fadeBackwards;
    }

    private void Update()
    {
        if (TEST_BUTTON_DELETE_ME)
        {
            DoFade(0, 2, fadeTime);
            TEST_BUTTON_DELETE_ME = false;
        }
    }
}
