using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponBlockManager : MonoBehaviour {
	bool m_bActive = true;

	public GameObject m_goBlock;
	public GameObject m_goWeapon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show(int nType, int nGrade = 0)
	{
		m_bActive = true;

		if( !m_goBlock.activeSelf )
			m_goBlock.SetActive (true);

		if( !m_goWeapon.activeSelf )
			m_goWeapon.SetActive (true);

		string strSpriteName = "sprite_weapon_" + nType.ToString ("00") + "_" + nGrade.ToString ("00");
		m_goWeapon.GetComponent<SpriteRenderer> ().sprite = CSpritesManager.instance.GetSprite (strSpriteName);

		strSpriteName = "sprite_block_" + nGrade.ToString ("00");
		m_goBlock.GetComponent<SpriteRenderer> ().sprite = CSpritesManager.instance.GetSprite (strSpriteName);
	}

	public void Hide()
	{
		m_bActive = false;

		if( m_goBlock.activeSelf )
			m_goBlock.SetActive (false);

		if( m_goWeapon.activeSelf )
			m_goWeapon.SetActive (false);
	}
}
