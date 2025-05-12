using DG.Tweening;
using R3;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public class DoDefaultPopUpAnimations : DoViewAnimations
    {
        private Transform _transform;



        protected override void InitCore(UIView view)
        {
            _transform = view.transform;

            if (view.IsSelfShowed.CurrentValue)
                _transform.localScale = Vector3.one;
            else _transform.localScale = Vector3.zero;
        }



        protected override Tween DOPlayShowCore()
        {
            return _transform.DOScale(1, Duration).OnComplete(() => _showEnded.OnNext(Unit.Default));
        }

        protected override Tween DOPlayHideCore()
        {
            return _transform.DOScale(0, Duration).OnComplete(() => _hideEnded.OnNext(Unit.Default));
        }
    }
}
