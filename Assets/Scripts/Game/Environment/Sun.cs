/*
 * LICENCE
 */
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class Sun : MonoBehaviour
	{
		[SerializeField]
		private Transform m_sunLight;
		[SerializeField]
		private float m_distance = 500.0f;

		private Transform m_playerCamera;

		void Start()
		{
			Debug.Assert( m_sunLight );
			GameObject go = GameCache.Instance.GetObject( GameCacheObjects.PlayerCamera );
			Debug.Assert( go );
			m_playerCamera = go.transform;
			Update();
		}

		void Update()
		{
			transform.position = m_playerCamera.position - m_sunLight.forward * m_distance;
			transform.LookAt( m_playerCamera );
		}
	}
}
