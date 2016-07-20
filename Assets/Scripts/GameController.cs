using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class GameController : MonoBehaviour {

	public VRInteractiveItem item;

	// Use this for initialization
	void Start () {
		item.OnOver += HandleOver;
		item.OnOut += HandleOut;
		item.OnOut += () => Debug.Log( "Lambda Action" );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HandleOver () {
		Debug.Log ("Over the thing");
	}

	private void HandleOut () {
		Debug.Log ("Off the thing");
	}
}
