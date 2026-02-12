using System;
using DG.Tweening;
using UnityEngine;
using WhiteArrow.ReactiveUI.Core;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class ScreenAnimations : VisibilityAnimations
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, Min(0F)] private float _duration = 0.3F;
        [SerializeField] private Ease _showEase = Ease.OutBack;
        [SerializeField] private Ease _hideEase = Ease.InBack;



        private Tween _lastTween;



        protected override bool TryPrepare()
        {
            if (_canvasGroup == null)
            {
                Debug.LogWarning($"The {nameof(_canvasGroup)} is not assigned to the {GetType().Name}.", _view);
                return false;
            }

            _canvasGroup.alpha = _view.Visibility.IsSelfShowed.CurrentValue ? 1 : 0;
            return true;
        }



        protected override void PlayShowAnimation(Action onComplete = null)
        {
            _lastTween = _canvasGroup
                .DOFade(1, _duration)
                .SetEase(_showEase)
                .OnComplete(() => onComplete?.Invoke());
        }

        protected override void SetEndStateOfShowAnimation()
        {
            _canvasGroup.alpha = 1;
        }



        protected override void PlayHideAnimation(Action onComplete = null)
        {
            _lastTween = _canvasGroup
                .DOFade(0, _duration)
                .SetEase(_hideEase)
                .OnComplete(() => onComplete?.Invoke());
        }

        protected override void SetEndStateOfHideAnimation()
        {
            _canvasGroup.alpha = 0;
        }



        protected override void KillCurrentAnimation()
        {
            if (_lastTween != null && _lastTween.IsActive())
                _lastTween.Kill();
        }
    }
}
