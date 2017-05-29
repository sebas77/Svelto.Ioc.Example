using UnityEngine;
using Svelto.IoC;

public class PathDTO:MonoBehaviour
{
	[Inject] public PathController pathController { private get; set; }
	
	public GameObject[] placeHolders;
	
	void Start()
	{
		pathController.pathData = this.placeHolders;
		
		GameObject.Destroy(this);
	}
}

