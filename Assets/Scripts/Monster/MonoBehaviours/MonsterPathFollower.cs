using System;
using UnityEngine;
using Holoville.HOTween;
using Svelto.IoC;

public class MonsterPathFollower
{
	[Inject] public PathController		pathController 			{ set; private get; }

    public event Action OnPathEnded;
	
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
		{
            OnPathEnded();
		}
	}

    internal void AddMonster(INeedToMoveUntilIDie monster)
    {
        _transform = monster.target;
		_transform.position = pathController.CheckPoint(_currentCheckPoint);
        monster.OnKilled += CleanUP;
		
		MoveNext();
    }

    internal void CleanUP(INeedToMoveUntilIDie monster)
    {
        _tweener.Kill();
    }

	
	int              _currentCheckPoint = 0;
    Transform        _transform;
    Tweener         _tweener;
}

