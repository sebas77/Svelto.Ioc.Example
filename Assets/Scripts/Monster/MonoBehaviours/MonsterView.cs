using Svelto.IoC;
using UnityEngine;

public class MonsterView: MonoBehaviour
{
    [Inject] public MonsterPresenter    monster      { set; private get; }
    
	void Start()
	{
        DesignByContract.Check.Require(monster != null);

        monster.SetView(this);
	}
	
	void Update()
	{
        monster.Update(Time.deltaTime);

		GetComponent<Renderer>().materials[0].color = new Color(1.0f, monster.energy, monster.energy, 1.0f);
	}
	
	public void Killed()
	{
		GameObject.Destroy(this.gameObject);
	}
}

