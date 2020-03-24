using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableEffect : MonoBehaviour
{
    protected bool _isPlaying;
    private Coroutine effectCoroutine;

    public bool IsPlaying
    {
        get => _isPlaying;
        protected set => _isPlaying = value;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool PlayEffect()
    {
        if (IsPlaying) return false;

        effectCoroutine = StartCoroutine(EffectCoroutine());
        
        return true;
    }

    public virtual void Stop()
    {
        StopCoroutine(effectCoroutine);
        ResetEffect();
    }

    protected abstract IEnumerator EffectCoroutine();
    protected abstract void ResetEffect();
}
