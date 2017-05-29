using Svelto.IoC;
using UnityEngine;

public class MonsterView: MonoBehaviour
{
    [Inject] public MonsterPresenter    monsterPresenter      { set; private get; }
    
	void Start()
	{
        DesignByContract.Check.Require(monsterPresenter != null);

        monsterPresenter.SetView(this);
	}
	
	void Update()
	{
        monsterPresenter.Update(Time.deltaTime);

		GetComponent<Renderer>().materials[0].color = new Color(1.0f, monsterPresenter.energy, monsterPresenter.energy, 1.0f);
	}
	
	public void Killed()
	{
		Destroy(gameObject);
	}
}

