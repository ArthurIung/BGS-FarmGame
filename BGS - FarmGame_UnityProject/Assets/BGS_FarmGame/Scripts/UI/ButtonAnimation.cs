using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class to animate Images with this preset.
/// </summary>
public class ButtonAnimation : EventTrigger
{
    [Space, SerializeField] bool startAnimation;

    [Header("Idle Animation")]
    [SerializeField] Vector3 idle_scaleDestination = Vector3.one * 1.05f;
    [SerializeField] float idle_durationTime = 1;
    [SerializeField] LeanTweenType idle_tweenScale = LeanTweenType.easeInOutSine;
    [Space]
    [SerializeField] float idle_scaleValue = 5;
    [SerializeField] float idle_scaleTime = 2f;
    [SerializeField] LeanTweenType idle_tweenRotation = LeanTweenType.easeInOutSine;

    [Header("Over Animation")]
    [SerializeField] float over_timeToStopRotation = 1f;
    [SerializeField] Vector3 over_scaleDestination = Vector3.one * 1.1f;
    [SerializeField] float over_timeToScale = 0.6f;
    [SerializeField] LeanTweenType over_tweenScale = LeanTweenType.easeInOutSine;

    [Header("Out Animation")]
    [SerializeField] Vector3 out_scaleDestination = Vector3.one;
    [SerializeField] float out_timeToScale = 0.3f;
    [SerializeField] LeanTweenType out_tweenScale = LeanTweenType.easeInOutSine;

    [Header("Click Animation")]
    [SerializeField] Vector3 click_scaleDestination = Vector3.one * 0.8f;
    [SerializeField] float click_timeToScale = 0.15f;
    [SerializeField] LeanTweenType click_tweenScale = LeanTweenType.easeInOutSine;


    private void Awake()
    {
        if (startAnimation)
            Anim_Idle();
    }

    #region EventTigger Override

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        Anim_Click();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Anim_Over();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        Anim_Out();
    }

    #endregion

    public void Anim_Idle()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, idle_scaleDestination, idle_durationTime).setEase(idle_tweenScale).setLoopPingPong();
        LeanTween.rotateZ(gameObject, idle_scaleValue, idle_scaleTime).setLoopPingPong().setEase(idle_tweenRotation);
    }

    public void Anim_Over()
    {
        LeanTween.cancel(gameObject);
        LeanTween.rotateZ(gameObject, 0, over_timeToStopRotation);
        LeanTween.scale(gameObject, over_scaleDestination, over_timeToScale).setEase(over_tweenScale).setLoopPingPong();
    }

    public void Anim_Out()
    {
        LeanTween.rotateZ(gameObject, -idle_scaleValue, out_timeToScale).setLoopPingPong();
        LeanTween.scale(gameObject, out_scaleDestination, out_timeToScale).setEase(out_tweenScale).setOnComplete(() => Anim_Idle());
    }

    public void Anim_Click()
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, click_scaleDestination, click_timeToScale).setEase(click_tweenScale).setOnComplete(() =>
        {
            Anim_Out();
        });

    }
}
