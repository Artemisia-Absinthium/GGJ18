/*
 * LICENCE
 */
using UnityEngine;

namespace Game
{
	[System.Serializable]
	public class PlayerController : MonoBehaviour
	{
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
		private float m_forwardSpeed = 2.0f;
		[SerializeField]
		private float m_backwardSpeed = 0.75f;
		[SerializeField]
		private float m_strafeSpeed = 1.25f;
		[SerializeField]
		private Vector2 m_viewSensibility = new Vector2( 1.0f, 1.0f );
		[SerializeField]
		private Camera m_camera = null;

		private Engine.InputAction m_moveForward = null;
		private Engine.InputAction m_moveBackward = null;
		private Engine.InputAction m_moveLeft = null;
		private Engine.InputAction m_moveRight = null;
		private Engine.InputAction m_interaction = null;

		private CharacterController m_characterController = null;
		private float m_angle = 0.0f;
		private float m_verticalView = 0.0f;

		void Start()
		{
			m_moveForward = Engine.InputManager.Instance.GetAction( m_moveForwardActionName );
			m_moveBackward = Engine.InputManager.Instance.GetAction( m_moveBackwardActionName );
			m_moveLeft = Engine.InputManager.Instance.GetAction( m_moveLeftActionName );
			m_moveRight = Engine.InputManager.Instance.GetAction( m_moveRightActionName );
			m_interaction = Engine.InputManager.Instance.GetAction( m_interactionActionName );

			m_characterController = GetComponent<CharacterController>();

			Debug.Assert( m_moveForward != null );
			Debug.Assert( m_moveBackward != null );
			Debug.Assert( m_moveLeft != null );
			Debug.Assert( m_moveRight != null );
			Debug.Assert( m_interaction != null );
			Debug.Assert( m_characterController );
			Debug.Assert( m_camera );
		}

		void Update()
		{
			Vector3 speed = Vector3.zero;
			Vector2 mouse = Engine.InputManager.Instance.GetMouseDelta();
			Vector2 joystick = Engine.InputManager.Instance.GetJoystickDelta();

			Vector2 motion = mouse + joystick;
			m_angle += motion.x * m_viewSensibility.x;
			m_verticalView = Mathf.Clamp( m_verticalView + Mathf.Clamp( -motion.y, -160.0f, 160.0f ), -80.0f, 80.0f );

			m_camera.transform.localRotation = Quaternion.Euler( m_verticalView, 0.0f, 0.0f );

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
		}
	}
}
