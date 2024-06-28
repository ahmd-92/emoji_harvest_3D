using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    Vector3 initialScale = Vector3.zero;
    private DiffButton diffButton;
    private bool animateButton = false;
    private void Start()
    {
        initialScale = buttonBgTrans.transform.localScale;
        diffButton = GetComponent<DiffButton>();
    }

    [SerializeField] private Transform buttonBgTrans;
    public void OnHooverEnter()
    {
        if(diffButton != null)
        {
            if (!diffButton.button.enabled) return;
        }
        

        buttonBgTrans.DOKill();
        buttonBgTrans.localScale = initialScale;
        SoundManager.instance.PlaySoundFX(SoundFXType.hooverButton);
        buttonBgTrans.DOScale(initialScale * 1.1f, 0.15f).SetEase(Ease.Linear);
        animateButton = true;
    }

    public void OnHooverExit()
    {
        if (!animateButton) return;
        buttonBgTrans.DOKill();
        buttonBgTrans.DOScale(initialScale, 0.15f).SetEase(Ease.Linear);
        animateButton = false;
    }


}
