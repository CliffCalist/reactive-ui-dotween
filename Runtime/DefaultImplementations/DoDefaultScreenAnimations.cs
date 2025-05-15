using System;
using DG.Tweening;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class DoDefaultScreenAnimations : DoViewAnimations
    {
        private CanvasGroup _canvasGroup;



        protected override void InitCore(UIView view)
        {
            if (!view.TryGetComponent(out _canvasGroup))
                throw new NullReferenceException($"The {view.name} don't have {nameof(CanvasGroup)}.");

            if (view.IsSelfShowed.CurrentValue)
                _canvasGroup.alpha = 1;
            else _canvasGroup.alpha = 0;
        }



        protected override Tween DOPlayShowCore()
        {
            return _canvasGroup.DOFade(1, Duration);
        }

        protected override Tween DOPlayHideCore()
        {
            return _canvasGroup.DOFade(0, Duration);
        }
    }
}
