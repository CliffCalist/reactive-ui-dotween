using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WhiteArrow.ReactiveUI.Core;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class PopUpAnimations : VisibilityAnimations
    {
        [SerializeField] private Transform _panel;

        [Space]
        [SerializeField] private Image _background;
        [SerializeField, Min(0)] private float _backgroundShownAlpha = 0.65F;

        [Space]
        [SerializeField, Min(0F)] private float _duration = 0.3F;
        [SerializeField] private Ease _showEase = Ease.OutBack;
        [SerializeField] private Ease _hideEase = Ease.InBack;



        private Sequence _sequence;



        protected override bool TryPrepare()
        {
            var hasProblem = false;
            var logs = new List<string>();

            if (_panel == null)
            {
                hasProblem = true;
                logs.Add($"The {nameof(_panel)} is not assigned to the {GetType().Name}.");
            }

            if (_background == null)
            {
                hasProblem = true;
                logs.Add($"The {nameof(_background)} is not assigned to the {GetType().Name}.");
            }

            if (hasProblem)
            {
                foreach (var log in logs)
                    Debug.LogWarning(log, _view);

                return false;
            }

            if (_view.Visibility.IsSelfShowed.CurrentValue)
            {
                _panel.localScale = Vector3.one;

                var backgroundColor = _background.color;
                backgroundColor.a = _backgroundShownAlpha;
                _background.color = backgroundColor;
            }
            else
            {
                _panel.localScale = Vector3.zero;

                var backgroundColor = _background.color;
                backgroundColor.a = 0;
                _background.color = backgroundColor;
            }

            return true;
        }



        protected override void PlayShowAnimation(Action onComplete)
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(_background.DOFade(_backgroundShownAlpha, _duration));
            _sequence.Join(_panel.DOScale(1, _duration).SetEase(_showEase));

            _sequence.OnComplete(() => onComplete?.Invoke());
            _sequence.Play();
        }

        protected override void SetEndStateOfShowAnimation()
        {
            var backgroundColor = _background.color;
            backgroundColor.a = _backgroundShownAlpha;
            _background.color = backgroundColor;

            _panel.localScale = Vector3.one;
        }



        protected override void PlayHideAnimation(Action onComplete)
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(_background.DOFade(0, _duration));
            _sequence.Join(_panel.DOScale(0, _duration).SetEase(_hideEase));

            _sequence.OnComplete(() => onComplete?.Invoke());
            _sequence.Play();
        }

        protected override void SetEndStateOfHideAnimation()
        {
            var backgroundColor = _background.color;
            backgroundColor.a = 0;
            _background.color = backgroundColor;

            _panel.localScale = Vector3.zero;
        }



        protected override void KillCurrentAnimation()
        {
            if (_sequence != null && _sequence.IsActive())
                _sequence.Kill();
        }
    }
}
