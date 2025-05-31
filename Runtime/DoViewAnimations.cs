using System;
using R3;

namespace WhiteArrow.ReactiveUI.DoTween
{
    public abstract class DoViewAnimations : MonoViewAnimations
    {
        protected override sealed void PlayShowCore()
        {
            KillLastTweenIfExist();
            DOPlayShowCore(() => _showEnded.OnNext(Unit.Default));
        }

        protected abstract void DOPlayShowCore(Action onComplete);



        protected override sealed void PlayHideCore()
        {
            KillLastTweenIfExist();
            DOPlayHideCore(() => _hideEnded.OnNext(Unit.Default));
        }

        protected abstract void DOPlayHideCore(Action onComplete);



        protected override void DisposeCore()
        {
            KillLastTweenIfExist();
        }



        protected abstract void KillLastTweenIfExist();
    }
}
