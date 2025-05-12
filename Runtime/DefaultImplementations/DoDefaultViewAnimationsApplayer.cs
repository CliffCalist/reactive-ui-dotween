using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow.ReactiveUI.DoTween
{
    [Serializable]
    public class DoDefaultViewAnimationsApplayer
    {
        [Header("Common")]
        [SerializeField, Min(0F)] protected float _screenAnimationsDuration = 0.3F;
        [SerializeField, Min(0F)] protected float _popUpAnimationsDuration = 0.3F;
        [SerializeField] private List<UIView> _screens;
        [SerializeField] private List<UIView> _popUps;



        public virtual void Aplay()
        {
            foreach (var screen in _screens)
            {
                var aniamtionModule = new DoDeafultScreenAnimations() { Duration = _screenAnimationsDuration };
                screen.SetAnimations(aniamtionModule);
            }

            foreach (var popUp in _popUps)
            {
                var aniamtionModule = new DoDefaultPopUpAnimations() { Duration = _popUpAnimationsDuration };
                popUp.SetAnimations(aniamtionModule);
            }
        }
    }
}
