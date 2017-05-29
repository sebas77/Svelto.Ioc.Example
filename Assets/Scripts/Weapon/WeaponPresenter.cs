using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Svelto.IoC;
using System.Collections.Generic;

public enum WeaponState
{
	idle,
	fire
}

public class WeaponPresenter : IInitialize
{
    [Inject] public UnderAttackSystem weaponSystem { private get; set; }
	// Use this for initialization

    public event System.Action<WeaponPresenter, bool> OnTargetNotFound;

    public Transform target { get { return _lockedTarget; } }

    public void OnDependenciesInjected()
    {
        _lockedTarget = null;
        _currentViewState = WeaponState.idle;

        weaponSystem.AddFreeWeapon(this);
    }

	public void Update()
	{
        if (_currentViewState == WeaponState.fire)
        {
            bool targetIsDead = _lockedTarget == null;

            if (targetIsDead || TargetIsInRange(_lockedTarget) == false)
            {
                Idle();

                OnTargetNotFound(this, targetIsDead);

                _lockedTarget = null;

                return;
            }

            _view.transform.LookAt(_lockedTarget.transform);
        }
	}
	
    public bool CheckAndAcquireTarget(Transform currentTarget)
    {
        if (_lockedTarget == null && TargetIsInRange(currentTarget))
        {
            _lockedTarget = currentTarget;

            Fire();

            return true;
        }

        return false;
    }

    public void SetView(WeaponView weaponView)
    {
        _view = weaponView;
    }

    void Fire()
    {
        _currentViewState = WeaponState.fire;
        
        _view.PauseTweener();
    }

    void Idle()
    {
        _currentViewState = WeaponState.idle;

        _view.PlayIdle();
    }

    bool TargetIsInRange(Transform currentTarget)
    {
        if ((_view.transform.position - currentTarget.position).sqrMagnitude < 30)
        {
            return true;
        }

        return false;
    }
	
	WeaponState 		_currentViewState;
    WeaponView          _view;
    Transform           _lockedTarget;
    Queue<GameObject> 	_targets;

    
}
