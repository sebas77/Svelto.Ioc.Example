using System;
using UnityEngine;
using Holoville.HOTween;

public class PathController
{
	public GameObject[] pathData {private get; set;}
	
	public Vector3 CheckPoint(int index)
	{
		return pathData[index].transform.position;
	}
	
	public bool IsEndReached(int check) { return check >= pathData.Length - 1; }
}



