using System;
using UnityEngine;
using Holoville.HOTween;
using Svelto.IoC;

//
//A new MonsterPathFollower is created for each injection.
//It basically take the responsability to move the monster.
//

public class MonsterPathFollower
{
	[Inject] public PathController		pathController 			{ set; private get; }
    [Inject] public IMonsterCountHolder monsterCounter      { set; private get; }

    void MoveNext()
	{
        if (pathController.IsEndReached(_currentCheckPoint) == false)
        {
            TweenParms paramaters = new TweenParms().Prop("position", pathController.CheckPoint(_currentCheckPoint + 1)).Ease(EaseType.Linear).OnComplete(MoveNext);

            _tweener = HOTween.To(_transform, 2, paramaters);
            _tweener.Play();

            _currentCheckPoint++;
        }
        else
            _monster.Escaped();
	}

    internal void SetMonster(INeedToMoveUntilIDie monster)
    {
        _monster = monster;

        _transform = monster.target;
		_transform.position = pathController.CheckPoint(_currentCheckPoint);
        
        monster.OnKilled += CleanUP;
		
		MoveNext();
    }

    internal void CleanUP(INeedToMoveUntilIDie monster)
    {
        monster.OnKilled -= CleanUP;

        _tweener.Kill();
    }
	
	int                     _currentCheckPoint = 0;
    Transform               _transform;
    Tweener                 _tweener;
    INeedToMoveUntilIDie    _monster;
}

