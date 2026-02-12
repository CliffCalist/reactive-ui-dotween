# ReactiveUI DoTween Extension

Extension package for [ReactiveUI for Unity](https://github.com/CliffCalist/reactive-ui-unity) that adds seamless integration with DoTween animations.

This module provides a minimal set of ready-to-use MonoBehaviour-based animations and a base class for creating custom transitions using DoTween.

> ⚠️ **Documentation Notice**
>
> This documentation may be partially outdated and will be revised in an upcoming update.
>
> ReactiveUI DoTween Extension is currently in **alpha stage**. The API and behavior may change significantly between versions.

# Features

- Plug-and-play support for `UIView` animations with DoTween
- Base class `DoViewAnimations` for fast creation of custom animations
- Included built-in animations: fade and scale
- Supports proper tween disposal and clean lifecycle handling
- No external dependencies besides DoTween

# Installing

Add the following line to your Unity project's `manifest.json`:

```json
"com.white-arrow.reactive-ui.dotween": "https://github.com/CliffCalist/reactive-ui-dotween.git"
```

# Usage

## Attach a preset

For quick prototyping, simply add one of the built-in animation components to the same GameObject as your `UIView`:

- `DoFadeViewAnimations` — fades in/out the canvas group
- `DoScaleViewAnimations` — scales the transform

They will be automatically detected and used by `UIView` on show/hide.
_No additional setup or code is required._

## Create a custom animation

To implement your own DoTween animation, inherit from `DoViewAnimations`:

```csharp
public class SlideInAnimation : DoViewAnimations
{
    [SerializeField] private Vector3 _offscreenPos;
    [SerializeField] private float _duration = 0.25f;
    [SerializeField] private Ease _ease = Ease.OutCubic;

    private Tween _activeTween;
    private Transform _target;

    protected override void InitCore(UIView view)
    {
        _target = view.transform;
    }

    protected override void PlayShowCore()
    {
        _activeTween = _target
            .DOLocalMove(Vector3.zero, _duration)
            .SetEase(_ease)
            .OnComplete(() => _showEnded.OnNext(Unit.Default));
    }

    protected override void PlayHideCore()
    {
        _activeTween = _target
            .DOLocalMove(_offscreenPos, _duration)
            .SetEase(_ease)
            .OnComplete(() => _hideEnded.OnNext(Unit.Default));
    }

    protected override void KillLastTweenIfExist()
    {
        if (_activeTween?.IsActive() == true)
            _activeTween.Kill();
    }
}
```

The `DoViewAnimations` base class ensures:

- `Init()` is called before any animation logic
- `KillLastTweenIfExist()` is automatically called before each animation
- `Dispose()` is automatically called when the view is destroyed, which also clears tweens via `KillLastTweenIfExist()`

# Built-in Presets

- `DoFadeViewAnimations` — fade in/out using `CanvasGroup.alpha`
- `DoScaleViewAnimations` — scale from/to `Vector3.zero`

These are useful for fast prototyping or simple transitions.

---
For complex animation sequences or pooling support, create a custom animation using `DoViewAnimations`.
