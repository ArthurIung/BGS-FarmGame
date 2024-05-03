using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] Scriptable_Items _currentItem;
    [SerializeField] Image _iconSprite;

    public void ConfigureCell(NPC_Interaction.Shop _c)
    {
        _iconSprite.sprite = _c._item._itemIcon;
        _currentItem = Instantiate(_c._item);
        
    }

    public void Button_SelectThisItem()
    {
        LogicController.Instance._uiController.ClickOnShopCell(_currentItem);
    }

    #region Animations

    public void Anim_Idle()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.05f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.rotateZ(gameObject, -10, 0f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.rotateZ(gameObject, 10, 1f).setLoopPingPong().setEase(LeanTweenType.easeInOutCubic);
    }

    public void Anim_Over()
    {
        LeanTween.cancel(gameObject);
        LeanTween.rotateZ(gameObject, 0, 1f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.3f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    public void Anim_Out()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() => Anim_Idle());
    }

    public void Anim_Click()
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.one * 0.8f, 0.15f).setEase(LeanTweenType.easeInOutSine).setOnComplete(()=>
        {
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() => Anim_Idle());
        });

    }

    #endregion

}
