/*
 * LICENCE
 */
using UnityEngine;
using cakeslice;

namespace Game
{
	[System.Serializable]
	public class PlayerController : MonoBehaviour
	{

		Engine.TriggerBase triggerObject;

		[SerializeField]
		private string m_moveForwardActionName = "Move Forward";
		[SerializeField]
		private string m_moveBackwardActionName = "Move Backward";
		[SerializeField]
		private string m_moveLeftActionName = "Strafe Left";
		[SerializeField]
		private string m_moveRightActionName = "Strafe Right";
		[SerializeField]
		private string m_interactionActionName = "Interaction";
		[SerializeField]
		private string m_pauseActionName = "Pause";
		[SerializeField]
		private float m_forwardSpeed = 2.0f;
		[SerializeField]
		private float m_backwardSpeed = 0.75f;
		[SerializeField]
		private float m_strafeSpeed = 1.25f;
		[SerializeField]
		private Vector2 m_viewSensibility = new Vector2( 1.0f, 1.0f );
		[SerializeField]
		private Transform m_camera = null;
		[SerializeField]
		private Transform m_interactionCaster = null;
		[SerializeField]
		private float m_bobbingSpeed = 1.0f;
		[SerializeField]
		private float m_bobbingAmplitude = 0.05f;
		[SerializeField]
		private float m_bobbingHeight = 0.05f;
		[SerializeField]
		private Transform m_bobbing = null;
		[SerializeField]
		private float m_bobbingLerpSpeed = 0.75f;

		private Engine.InputAction m_moveForward = null;
		private Engine.InputAction m_moveBackward = null;
		private Engine.InputAction m_moveLeft = null;
		private Engine.InputAction m_moveRight = null;
		private Engine.InputAction m_interaction = null;
		private Engine.InputAction m_menuAction = null;

		private CharacterController m_characterController = null;
		private float m_angle = -90.0f;
		private float m_verticalView = 0.0f;
		private float m_bobbingHeightBase = 1.6f;

		private int m_layerMask = 0;

		//Sounds
		[SerializeField]
		private AudioClip m_StepSound;
		[SerializeField]
		private AudioSource m_AudioSource;

		private bool m_previousStepPositive = false;

		void Start()
		{
			//Set Cursor to not be visible
			Cursor.visible = false;

			//
			m_AudioSource = GetComponent<AudioSource>();
			m_AudioSource.volume = 0.8f;

			m_moveForward = Engine.InputManager.Instance.GetAction( m_moveForwardActionName );
			m_moveBackward = Engine.InputManager.Instance.GetAction( m_moveBackwardActionName );
			m_moveLeft = Engine.InputManager.Instance.GetAction( m_moveLeftActionName );
			m_moveRight = Engine.InputManager.Instance.GetAction( m_moveRightActionName );
			m_interaction = Engine.InputManager.Instance.GetAction( m_interactionActionName );
			m_menuAction = Engine.InputManager.Instance.GetAction( m_pauseActionName );

			m_characterController = GetComponent<CharacterController>();

			Debug.Assert( m_moveForward != null );
			Debug.Assert( m_moveBackward != null );
			Debug.Assert( m_moveLeft != null );
			Debug.Assert( m_moveRight != null );
			Debug.Assert( m_interaction != null );
			Debug.Assert( m_menuAction != null );
			Debug.Assert( m_characterController );
			Debug.Assert( m_camera );
			Debug.Assert( m_interactionCaster );
			Debug.Assert( m_bobbing );

			m_layerMask = LayerMask.GetMask( "Trigger" );

			m_angle = transform.localRotation.eulerAngles.y;
			transform.localRotation = Quaternion.Euler( 0.0f, m_angle, 0.0f );

			m_bobbingHeightBase = m_bobbing.localPosition.y;
		}

		void Update()
		{
			if ( GameController.Instance.IsSpeaking || 
				MainMenuManager.Instance.IsInMenu ||
				Cinematics.Instance.Playing )
			{
				return;
			}
			if ( !MainMenuManager.Instance.IsInMenu && m_menuAction.Down )
			{
				MainMenuManager.Instance.Pause();
				return;
			}
			Vector3 speed = Vector3.zero;
			Vector2 mouse = Engine.InputManager.Instance.GetMouseDelta();
			Vector2 joystick = Engine.InputManager.Instance.GetJoystickDelta();

			Vector2 motion = mouse + joystick;
			m_angle += motion.x * m_viewSensibility.x;
			m_verticalView = Mathf.Clamp( m_verticalView + Mathf.Clamp( -motion.y, -160.0f, 160.0f ), -80.0f, 80.0f );

			m_camera.localRotation = Quaternion.Euler( m_verticalView, 0.0f, 0.0f );

			if ( m_moveForward.State )
			{
				speed.z += m_forwardSpeed;
			}
			else if ( m_moveBackward.State )
			{
				speed.z -= m_backwardSpeed;
			}
			if ( m_moveLeft.State )
			{
				speed.x -= m_strafeSpeed;
			}
			if ( m_moveRight.State )
			{
				speed.x += m_strafeSpeed;
			}
			Quaternion rotation = Quaternion.Euler( 0.0f, m_angle, 0.0f );
			transform.localRotation = rotation;
			speed = rotation * speed;
			m_characterController.SimpleMove( speed );

			if ( speed.sqrMagnitude > m_strafeSpeed / 2.0f )
			{
				if ( !m_AudioSource.isPlaying )
				{
					if ( ( m_bobbing.localPosition.x > 0.0f && !m_previousStepPositive ) ||
						( m_bobbing.localPosition.x < 0.0f && m_previousStepPositive ) )
					{
						m_previousStepPositive = !m_previousStepPositive;
						m_AudioSource.clip = m_StepSound;
						m_AudioSource.Play();
					}
				}
				float t = 2.0f * Time.time * Mathf.PI * m_bobbingSpeed;
				float bobbingAmplitude = Mathf.Sin( t ) * m_bobbingAmplitude;
				float bobbingHeight = Mathf.Cos( 2.0f * t) * m_bobbingHeight;
				m_bobbing.localPosition = Vector3.Lerp(
					m_bobbing.localPosition,
					new Vector3( bobbingAmplitude, m_bobbingHeightBase + bobbingHeight),
					Time.deltaTime * m_bobbingLerpSpeed );
			}
			else if ( !Mathf.Approximately( m_bobbing.localPosition.x, 0.0f ) )
			{
				m_bobbing.localPosition = Vector3.Lerp(
					m_bobbing.localPosition,
					new Vector3( 0.0f, m_bobbingHeightBase, 0.0f ),
					Time.deltaTime * m_bobbingLerpSpeed );
			}

			if ( !GameController.Instance.IsSpeaking && !GameController.Instance.WasSpeaking )
			{
				if ( m_interaction.Down || ( ( int )( Time.time * 10 ) % 5 == 0 ) )
				{
					RaycastHit hitInfo;
					if ( Physics.Raycast( m_interactionCaster.position, m_interactionCaster.forward, out hitInfo, 1.5f, m_layerMask ) )
					{
						Engine.TriggerBase trigger = hitInfo.collider.GetComponent<Engine.TriggerBase>();

						if ( m_interaction.Down )
						{
							if ( trigger )
							{
								m_interaction.Update();
								trigger.OnTrigger();
							}
						}

						if ( trigger )
						{
							if ( triggerObject != trigger && triggerObject != null )
							{
								triggerObject.GetComponentInChildren<Outline>().enabled = false;
							}
							trigger.GetComponentInChildren<Outline>().enabled = true;
							triggerObject = trigger;
						}
						else
						{
							if ( triggerObject != null )
							{
								triggerObject.GetComponentInChildren<Outline>().enabled = false;
								triggerObject = null;
							}
						}
					}
					else
					{
						if ( triggerObject != null )
						{
							triggerObject.GetComponentInChildren<Outline>().enabled = false;
							triggerObject = null;
						}
					}
				}
			}
		}
	}
}
