using System;
using DG.Tweening;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class DoFadeViewAnimations : DoViewAnimations
    {
        [SerializeField, Min(0F)] private float _duration = 0.3F;

        [Space]
        [SerializeField, Min(0F)] private float _showStartAlpha = 0F;
        [SerializeField, Min(0F)] private float _showEndAlpha = 1F;

        [Space]
        [SerializeField, Min(0F)] private float _hideStartAlpha = 1F;
        [SerializeField, Min(0F)] private float _hideEndAlpha = 0F;



        private CanvasGroup _canvasGroup;
        private Tween _lastTween;



        protected override void InitCore(UIView view)
        {
            if (!view.TryGetComponent(out _canvasGroup))
                throw new NullReferenceException($"The {view.name} don't have {nameof(CanvasGroup)}.");

            if (view.IsSelfShowed.CurrentValue)
                _canvasGroup.alpha = _hideStartAlpha;
            else _canvasGroup.alpha = _showStartAlpha;
        }



        protected override void DOPlayShowCore(Action onComplete)
        {
            _lastTween = _canvasGroup
                .DOFade(_showEndAlpha, _duration)
                .OnComplete(() => onComplete());
        }

        protected override void DOPlayHideCore(Action onComplete)
        {
            _lastTween = _canvasGroup
                .DOFade(_hideEndAlpha, _duration)
                .OnComplete(() => onComplete());
        }



        protected override void KillLastTweenIfExist()
        {
            if (_lastTween != null && _lastTween.IsActive())
                _lastTween.Kill();
        }
    }
}
