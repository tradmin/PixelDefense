using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButtonsManager : MonoBehaviour {
	void Start()
	{
	}

	public void OnClick(int nIndex)
	{
		switch (nIndex) {
		// InGame --------------
		case 1000:
			GameObject.Find ("GameProcess").GetComponent<CGameProcess> ().CreateRandomWeaponBlock ();
			break;
		}
	}
}
