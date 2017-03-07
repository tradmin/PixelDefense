using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameProcess : MonoBehaviour {
	Vector3[] m_arrVecBlockPoz = new Vector3[36];

	public GameObject m_goCamera;

	public GameObject m_goWeapon;

	int m_nIndex = 0;

	GameObject[] m_goWeaponBlock = new GameObject[36];

	int[] m_nBlockInfo = new int[36];

	int m_nMovingIndex = 0;

	// Use this for initialization
	void Start () {
		CSpritesManager.instance.InitSprite ();

		float[] m_fXLinePoz = {-2.52f, -1.8f, -0.36f, 0.36f, 1.8f, 2.52f};
		float[] m_fYLinePoz = {2.51f, 1.79f, 0.35f, -0.37f, -1.81f, -2.53f};

		int nXLineIndex = 0;
		int nYLineIndex = 0;
		for (int i = 0; i < m_arrVecBlockPoz.Length; i++) {
			nXLineIndex = i % 6;
			nYLineIndex = (int)((float)i / (float)6);
			m_arrVecBlockPoz [i] = new Vector3 (m_fXLinePoz [nXLineIndex], m_fYLinePoz [nYLineIndex], 1);
		}

		for (int i = 0; i < m_goWeaponBlock.Length; i++) {
			Object objWeaponBlock = (Object)Resources.Load ("Prefabs/WeaponBlock");
			m_goWeaponBlock [i] = (GameObject)Instantiate (objWeaponBlock, m_arrVecBlockPoz [i], Quaternion.identity);
			m_goWeaponBlock [i].GetComponent<CWeaponBlockManager> ().Hide ();

			m_nBlockInfo [i] = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool bTouch = false;
		#if UNITY_EDITOR
		/*
		if( Input.touchCount > 0 )
		{
			Debug.Log("test");
			bTouch = true;
		}
		*/
		//if( Input.GetMouseButtonDown(0) )

		if( Input.GetMouseButtonDown(0) )
		{
			Vector3 vecPoz = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));

			for(int i = 0; i < m_nBlockInfo.Length; i++)
			{
				if( m_nBlockInfo[i] >= 0 )
				{
					float fX = m_arrVecBlockPoz[i].x - 0.35f;
					float fY = m_arrVecBlockPoz[i].y + 0.35f;

					if( (fX <= vecPoz.x) && ((fX + 0.7f) > vecPoz.x ))
					{
						if( (fY > vecPoz.y) && ((fY - 0.7f) <= vecPoz.y ))
						{
							m_nMovingIndex = i;
							Debug.Log("Input[" + i + "] x : " + vecPoz.x + ", y : " + vecPoz.y);
							StartCoroutine("OnMouseDown");
						}
					}
				}
			}
			//Debug.Log("x : " + vecPoz.x + ", y : " + vecPoz.y);


			//Vector3 vecPoz = Input.mousePosition;
			//Debug.Log ("GetTouch Poz : " + vecPoz.ToString());
		}
		#else
		if( Input.touchCount > 0 )
		{
			bTouch = true;
		}
		#endif
	}

	public void CreateRandomWeaponBlock()
	{
		int nEnablieBlockCount = GetEnableBlockCount ();
		if (nEnablieBlockCount <= 0) {
			Debug.Log ("Can not create weapon block - Full");
			return;
		}
		
		int nResultValue = Random.Range (0, nEnablieBlockCount);

		int nEmptyIndex = 0;

		int nCreateIndex = 0;
		for (int i = 0; i < m_nBlockInfo.Length; i++) {
			if (m_nBlockInfo [i] < 0) {
				if (nResultValue == nEmptyIndex) {
						nCreateIndex = i;
						break;
				}
				nEmptyIndex++;
			}
		}

		int nRandomValue = Random.Range (0, 6);
		int nRandomGrade = 0;
		CreateWeaponBlock (nCreateIndex, nRandomValue, nRandomGrade);
	}

	public void CreateWeaponBlock(int nIndex, int nType, int nGrade = 0)
	{
		Debug.Log ("Block Type : " + nType);
		m_goWeaponBlock [nIndex].GetComponent<CWeaponBlockManager> ().Show (nType, nGrade);
		SetWeaponBlockIndex (nIndex, nType, 0);
	}

	public void ChangeBlock()
	{
		m_goWeapon.transform.localPosition = m_arrVecBlockPoz [m_nIndex];

		m_nIndex++;

		if (m_nIndex > 35)
			m_nIndex = 0;
	}

	public void SetWeaponBlockIndex(int nIndex, int nType, int nGrade)
	{
		int nWeaponBlcokIndex = (nGrade * 10) + nType;
		m_nBlockInfo [nIndex] = nWeaponBlcokIndex;
	}

	public int GetEnableBlockCount()
	{
		int nCount = 0;
		for (int i = 0; i < m_nBlockInfo.Length; i++) {
			if (m_nBlockInfo [i] < 0) {
				nCount++;
			}
		}

		return nCount;
	}

	IEnumerator OnMouseDown()
	{
		Vector3 scrSpace = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 offset = m_goWeaponBlock [m_nMovingIndex].transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));

		while (Input.GetMouseButton(0))
		{
			Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
			m_goWeaponBlock [m_nMovingIndex].transform.position = curPosition;

			//m_goWeapon.transform.position = curPosition;
			Debug.Log ("x : " + curPosition.x + ", y : " + curPosition.y);
			yield return null;
		}

		m_goWeaponBlock [m_nMovingIndex].transform.position = m_arrVecBlockPoz [m_nMovingIndex];
		m_nMovingIndex = 0;
	}
}
