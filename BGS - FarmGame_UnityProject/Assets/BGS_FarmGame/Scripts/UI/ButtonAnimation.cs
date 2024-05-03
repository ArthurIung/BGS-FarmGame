using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : EventTrigger
{

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


    public void Anim_Idle()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.05f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.rotateZ(gameObject, -5, 0f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.rotateZ(gameObject, 5, 1f).setLoopPingPong().setEase(LeanTweenType.easeInOutSine);
    }

    public void Anim_Over()
    {
        LeanTween.cancel(gameObject);
        LeanTween.rotateZ(gameObject, 0, 1f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.6f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    public void Anim_Out()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() => Anim_Idle());
    }

    public void Anim_Click()
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.one * 0.8f, 0.15f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() => Anim_Idle());
        });

    }
}
