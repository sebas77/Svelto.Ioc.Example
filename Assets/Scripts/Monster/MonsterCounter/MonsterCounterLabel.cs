using UnityEngine;
using Svelto.IoC;

public class MonsterCounterLabel : MonoBehaviour 
{
	[Inject] public IMonsterCounter monsterSystem { private get; set; }
	
	void Start()
	{
		DesignByContract.Check.Assert(monsterSystem != null, "MonsterCounterLabel - monsterSystem non correctly Injected");
	}
	
	void OnGUI () 
	{
		GUIStyle style = new GUIStyle();
		
		style.fontSize = 30;
		
    	GUI.Label (new Rect (10, 10, 100, 30), monsterSystem.monsterCount.ToString(), style);
	}
}
