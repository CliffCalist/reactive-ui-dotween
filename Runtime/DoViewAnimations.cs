using DG.Tweening;
using R3;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public abstract class DoViewAnimations : ViewAnimations
    {
        [SerializeField] private float _duration = 0.3F;


        private Tween _lastTween;


        public float Duration
        {
            get => _duration;
            set => _duration = Mathf.Max(0, value);
        }



        protected override sealed void PlayShowCore()
        {
            KillLastTween();
            _lastTween = DOPlayShowCore();
            _lastTween.OnComplete(() => _showEnded.OnNext(Unit.Default));
        }

        protected abstract Tween DOPlayShowCore();


        protected override sealed void PlayHideCore()
        {
            KillLastTween();
            _lastTween = DOPlayHideCore();
            _lastTween.OnComplete(() => _hideEnded.OnNext(Unit.Default));
        }

        protected abstract Tween DOPlayHideCore();



        private void KillLastTween()
        {
            if (_lastTween == null || !_lastTween.IsActive())
                return;

            _lastTween.Kill();
        }
    }
}
