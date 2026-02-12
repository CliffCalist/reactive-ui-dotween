using System;
using DG.Tweening;
using UnityEngine;
using WhiteArrow.ReactiveUI.Core;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class ScaleAnimations : VisibilityAnimations
    {
        [SerializeField] private Transform _scalable;
        [SerializeField, Min(0F)] private float _duration = 0.3F;
        [SerializeField] private Ease _showEase = Ease.OutBack;
        [SerializeField] private Ease _hideEase = Ease.InBack;



        private Tween _tween;



        protected override void OnViewAttached()
        {
            _scalable.localScale = _view.Visibility.IsSelfShowed.CurrentValue
                ? Vector3.one
                : Vector3.zero;
        }



        protected override void PlayShowAnimation(Action onComplete = null)
        {
            _tween = _scalable.DOScale(Vector3.one, _duration)
                .SetEase(_showEase)
                .OnComplete(() => onComplete?.Invoke());
        }

        protected override void SetEndStateOfShowAnimation()
        {
            _scalable.localScale = Vector3.one;
        }



        protected override void PlayHideAnimation(Action onComplete = null)
        {
            _tween = _scalable.DOScale(Vector3.zero, _duration)
                .SetEase(_hideEase)
                .OnComplete(() => onComplete?.Invoke());
        }

        protected override void SetEndStateOfHideAnimation()
        {
            _scalable.localScale = Vector3.zero;
        }



        protected override void KillCurrentAnimation()
        {
            if (_tween != null && _tween.IsActive())
                _tween.Kill();
        }
    }
}