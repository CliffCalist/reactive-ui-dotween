using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    [Serializable]
    public class DoDefaultViewAnimationsApplier
    {
        [Header("Common")]
        [SerializeField, Min(0F)] protected float _screenAnimationsDuration = 0.3F;
        [SerializeField, Min(0F)] protected float _popUpAnimationsDuration = 0.3F;
        [SerializeField] private List<UIView> _screens;
        [SerializeField] private List<UIView> _popUps;



        public virtual void Apply()
        {
            foreach (var screen in _screens)
            {
                var animationModule = new DoDefaultScreenAnimations() { Duration = _screenAnimationsDuration };
                screen.SetAnimations(animationModule);
            }

            foreach (var popUp in _popUps)
            {
                var animationModule = new DoDefaultPopUpAnimations() { Duration = _popUpAnimationsDuration };
                popUp.SetAnimations(animationModule);
            }
        }
    }
}
