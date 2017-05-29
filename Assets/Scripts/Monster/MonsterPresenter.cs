using System;
using Svelto.IoC;
using UnityEngine;

enum MonsterState
{
    Move,
    Hit
};

public class MonsterPresenter: INeedToMoveUntilIDie, ITarget, IInitialize
{
    [Inject] public UnderAttackSystem   attackSystem { set; private get; }
    [Inject] public MonsterPathFollower pathFollower { set; private get; }
    [Inject] public IMonsterCountHolder monsterCounter      { set; private get; }

    public event System.Action<INeedToMoveUntilIDie> OnKilled;
        
    public Transform    target { get { return _view.transform; } }
    public float        energy { get { return _energy; } }

    void IInitialize.OnDependenciesInjected()
	{
        monsterCounter.AddMonster();
	}

    public void SetView(MonsterView view)
    {
        _view = view;

        pathFollower.SetMonster(this);
        attackSystem.AddMonster(this);
    }

    public void StartBeingHit()
    {
        _hitEnergy += DAMAGE;
    }

    public void StopBeingHit()
    {
        _hitEnergy -= DAMAGE;
    }

    public void Update(float deltaTime)
    {
        _energy -= deltaTime * _hitEnergy;

        if (_energy <= 0)
        {
            KilledOrEscapedDoenstMatterDoesIt();
        }
    }

    void KilledOrEscapedDoenstMatterDoesIt()
    {
        if (OnKilled != null)
            OnKilled(this);

        monsterCounter.RemoveMonster();

        _view.Killed();
    }

    public void Escaped()
    {
        KilledOrEscapedDoenstMatterDoesIt();
    }

    const float DAMAGE = 0.25f;

    float           _energy = 1.0f;
    float           _hitEnergy = 0.0f;
    MonsterView     _view;
}
