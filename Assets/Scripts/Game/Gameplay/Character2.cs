/*
 * LICENCE
 */
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class Character2 : Engine.BehaviourBase
	{
		[SerializeField]
		private Character.Name m_character1 = Character.Name.NOBODY;
		[SerializeField]
		private Character.Name m_character2 = Character.Name.NOBODY;
		[SerializeField]
		private Sprite m_sprite = null;

		void Start()
		{
			Debug.Assert( m_sprite );
			foreach ( SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>() )
			{
				if ( sr.name == "Image" )
				{
					sr.sprite = m_sprite;
				}
			}
		}

		void Update()
		{
			Vector3 position = GameCache.Instance.GetObject( GameCacheObjects.PlayerCamera ).transform.position;
			position.y = transform.position.y;
			transform.LookAt( position );
			transform.Rotate( 0.0f, 180.0f, 0.0f );
		}

		public override void Trigger( Engine.TriggerBase _trigger, Object _data )
		{
			Transform camera = GameCache.Instance.GetObject( GameCacheObjects.PlayerCamera ).transform;
			Vector3 delta = transform.position - camera.position;
			delta.y = 0;
			Vector3 forward = camera.forward;
			forward.y = 0.0f;
			if ( Vector3.Cross( delta, forward ).y > 0.0f )
			{
				GameController.Instance.OnCharacterInteraction( m_character1 );
			}
			else
			{
				GameController.Instance.OnCharacterInteraction( m_character2 );
			}
		}
	}
}
