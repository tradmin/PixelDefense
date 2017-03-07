using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpritesManager : MonoBehaviour {
	public static CSpritesManager instance = null;

	Sprite[] m_arrSprite;

	public static CSpritesManager Instance
	{
		get {
			if (instance == null) {
				Debug.Log ("CSpriteManager install null");
			} 
			return instance;
		}
	}

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InitSprite()
	{
		m_arrSprite = Resources.LoadAll <Sprite>("Sprites/sprite_ingame");

//		for (int i = 0; i < m_arrSprite.Length; i++) {
//			Debug.Log ("SpriteName[" + i + "] : " + m_arrSprite [i].name);
//		}
	}

	public Sprite GetSprite(string strSpriteName)
	{
		for (int i = 0; i < m_arrSprite.Length; i++) {
//			Debug.Log (i + " - " + strSpriteName + " : " + m_arrSprite [i].name);
			if (strSpriteName.Equals (m_arrSprite [i].name)) {
				return m_arrSprite [i];
			}
		}

		Debug.Log ("[Error] GetSprite");
		return null;
	}
}

