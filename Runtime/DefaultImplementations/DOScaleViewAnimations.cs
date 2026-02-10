using System;
using DG.Tweening;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class DoScaleViewAnimations : DoViewAnimations
    {
        [SerializeField, Min(0F)] private float _duration = 0.3F;

        [Space]
        [SerializeField, Min(0F)] private float _showStartScale = 0F;
        [SerializeField, Min(0F)] private float _showEndScale = 1F;
        [SerializeField] private Ease _showEase = Ease.OutBack;

        [Space]
        [SerializeField, Min(0F)] private float _hideStartScale = 1F;
        [SerializeField, Min(0F)] private float _hideEndScale = 0F;
        [SerializeField] private Ease _hideEase = Ease.InBack;



        private Transform _transform;
        private Tween _lastTween;



        protected override void InitCore(UIView view)
        {
            _transform = view.transform;

            if (view.IsSelfShowed.CurrentValue)
                _transform.localScale = Vector3.one * _hideStartScale;
            else _transform.localScale = Vector3.one * _showStartScale;
        }



        protected override void DOPlayShowCore(Action onComplete)
        {
            _transform.localScale = Vector3.one * _showStartScale;

            _lastTween = _transform
                .DOScale(_showEndScale, _duration)
                .SetEase(_showEase)
                .OnComplete(() => onComplete());
        }

        protected override void DOPlayHideCore(Action onComplete)
        {
            _transform.localScale = Vector3.one * _hideStartScale;

            _lastTween = _transform
                .DOScale(_hideEndScale, _duration)
                .SetEase(_hideEase)
                .OnComplete(() => onComplete());
        }



        protected override void KillLastTweenIfExist()
        {
            if (_lastTween != null && _lastTween.IsActive())
                _lastTween.Kill();
        }
    }
}
