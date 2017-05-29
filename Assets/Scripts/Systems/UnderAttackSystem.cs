using System.Collections.Generic;
using UnityEngine;
using Svelto.IoC;

public class UnderAttackSystem:IInitialize
{
    [Inject] public IMonsterCountHolder     monsterCounter      { set; private get; }

    public UnderAttackSystem()
	{
        _freeWeapons = new List<WeaponPresenter>();
        _monstersDic = new Dictionary<Transform, ITarget>();
	}

    public void OnDependenciesInjected()
	{
        DesignByContract.Check.Require(monsterCounter != null);

        TaskRunner.Instance.Run(new Svelto.Tasks.LoopActionEnumerator(Tick));
	}

    //ITickable Interface

    public void Tick()
    {
        CheckTargets();
    }

    /// <summary>
    /// public methods
    /// </summary>

    public void AddFreeWeapon(WeaponPresenter weapon)
    {
        _freeWeapons.Add(weapon);
    }

    public void AddMonster(ITarget monster)
    {
        _monstersDic[monster.target] = monster;

        monsterCounter.AddMonster();

        monster.OnKilled += OnMonsterKilled;
    }

    void CheckTargets()
    {
        for (var monsterEnumerator = _monstersDic.Values.GetEnumerator(); monsterEnumerator.MoveNext();)
        {
            var currentMonster = monsterEnumerator.Current;       
    
            for (int i = 0; i < _freeWeapons.Count; i++)
            {
                WeaponPresenter currentWeapon = _freeWeapons[i];

                if (currentWeapon.CheckAndAcquireTarget(currentMonster.target) == true)
                {
                    currentMonster.StartBeingHit();
                    
                    currentWeapon.OnTargetNotFound += TargetOutOfRange;

                    _freeWeapons.RemoveAt(i);

                    return;
                }
            }
        }
    }

    void OnMonsterKilled(INeedToMoveUntilIDie monster)
    {
        monster.OnKilled -= OnMonsterKilled;

        monsterCounter.RemoveMonster();

        _monstersDic.Remove(monster.target);
    }

    void TargetOutOfRange(WeaponPresenter weapon, bool targetIsDead)
    {
        weapon.OnTargetNotFound -= TargetOutOfRange;

        if (targetIsDead == false)
            _monstersDic[weapon.target].StopBeingHit();

        AddFreeWeapon(weapon);
    }

    List<WeaponPresenter>             _freeWeapons; 
    Dictionary<Transform, ITarget>    _monstersDic;
}
