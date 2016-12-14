using UnityEngine;
using System.Collections;
public class PlayerSetup : MonoBehaviour{

	void Start () 
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameCamera> ().target = this.gameObject.transform;

	}
}
