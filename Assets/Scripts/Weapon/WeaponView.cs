using UnityEngine;
using Holoville.HOTween;
using Svelto.IoC;

public class WeaponView : MonoBehaviour
{
    [Inject] public WeaponPresenter weapon { private get; set; }
	// Use this for initialization
	void Start () 
	{
        _tweenparms = new TweenParms()
           .Prop("rotation", new Vector3(0, 180, 0))
           .Ease(EaseType.EaseInOutQuad)
           .Loops(-1, LoopType.Yoyo);

        _tweener = HOTween.To(this.transform, 4, _tweenparms);

        _tweener.Play();

        weapon.SetView(this);
	}

	public void PlayIdle()
	{
        _tweener.Play();
	}
	
	void Update()
	{
        weapon.Update();
	}
	
	public void PauseTweener()
	{
        _tweener.Pause();
	}
	
	WeaponState 		_currentViewState;
	Tweener				_tweener;
    GameObject          _lockedTarget;
    TweenParms          _tweenparms;
}
