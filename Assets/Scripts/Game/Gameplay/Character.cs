/*
 * LICENCE
 */
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class Character : Engine.BehaviourBase
	{
		public enum Name
		{
			NOBODY,
			PEASANT1_GRANDMOTHER,
			PEASANT2_FATHER,
			PEASANT3_COUSIN,
			PEASANT4_NIECE,
			PEASANT5_WORKER,
			PEASANT6_CHILD,

			RELIGIOUS0_DOOR,
			RELIGIOUS1_COMMANDER,
			RELIGIOUS2_TEACHER,

			BARTENDER1_MOTHER,
			BARTENDER2_CHILD1,
			BARTENDER3_CHILD2,
			BARTENDER4_DOG,

			MEDICIN1_FATHER,
			MEDECIN2_MOTHER,
			MEDECIN3_DAUGHTER,
			MEDECIN4_SON,
			MEDECIN5_CHILD,

			NOBLE1_GRANDMOTHER,
			NOBLE2_GRANDFATHER,
			NOBLE3_FATHER,
			NOBLE4_SON,
			NOBLE5_CHILD,

			COUSIN,

			DOOR1,
			DOOR2,
			DOOR3,
			WATERINGCAN
		}

		[SerializeField]
		private Name m_character = Name.NOBODY;
		[SerializeField]
		private Sprite m_sprite = null;

		private Transform m_spriteTransform = null;

		void Start()
		{
			Debug.Assert( m_sprite );
			foreach ( Transform t in transform )
			{
				if ( t.name == "Sprite" )
				{
					m_spriteTransform = t;
					break;
				}
			}
			foreach( SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>() )
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
			position.y = m_spriteTransform.position.y;
			m_spriteTransform.LookAt( position );
			m_spriteTransform.Rotate( 0.0f, 180.0f, 0.0f );
		}
		
		public override void Trigger( Engine.TriggerBase _trigger, Object _data )
		{
			GameController.Instance.OnCharacterInteraction( m_character );
		}
	}
}
