/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MenuInputModule : PointerInputModule
{
	private float m_PrevActionTime;
	Vector2 m_LastMoveVector;
	int m_ConsecutiveMoveCount = 0;

	private Vector2 m_LastMousePosition;
	private Vector2 m_MousePosition;

#pragma warning disable 649
	[SerializeField]
	private string m_confirm;
	[SerializeField]
	private string m_cancel;
	[SerializeField]
	private string m_menuUp;
	[SerializeField]
	private string m_menuDown;
	[SerializeField]
	private string m_menuLeft;
	[SerializeField]
	private string m_menuRight;
#pragma warning restore 649

	[SerializeField]
	private float m_InputActionsPerSecond = 10;
	[SerializeField]
	private float m_RepeatDelay = 0.5f;
	[SerializeField]
	private bool m_ForceModuleActive;

	private Engine.InputAction m_actionConfirm;
	private Engine.InputAction m_actionCancel;
	private Engine.InputAction m_actionMenuUp;
	private Engine.InputAction m_actionMenuDown;
	private Engine.InputAction m_actionMenuLeft;
	private Engine.InputAction m_actionMenuRight;

	public bool allowActivationOnMobileDevice
	{
		get { return m_ForceModuleActive; }
		set { m_ForceModuleActive = value; }
	}

	public bool forceModuleActive
	{
		get { return m_ForceModuleActive; }
		set { m_ForceModuleActive = value; }
	}

	public float inputActionsPerSecond
	{
		get { return m_InputActionsPerSecond; }
		set { m_InputActionsPerSecond = value; }
	}

	public float repeatDelay
	{
		get { return m_RepeatDelay; }
		set { m_RepeatDelay = value; }
	}

	public override void UpdateModule()
	{
		m_LastMousePosition = m_MousePosition;
		m_MousePosition = Input.mousePosition;
	}

	public override bool IsModuleSupported()
	{
		return m_ForceModuleActive || Input.mousePresent;
	}

	public override bool ShouldActivateModule()
	{
		if ( !base.ShouldActivateModule() || Engine.InputManager.Instance == null )
		{
			return false;
		}

		bool shouldActivate = m_ForceModuleActive;
		shouldActivate |= !Engine.InputManager.Instance.GetAction( m_confirm ).State;
		shouldActivate |= !Engine.InputManager.Instance.GetAction( m_cancel ).State;
		shouldActivate |= !Engine.InputManager.Instance.GetAction( m_menuUp ).State;
		shouldActivate |= !Engine.InputManager.Instance.GetAction( m_menuDown ).State;
		shouldActivate |= !Engine.InputManager.Instance.GetAction( m_menuLeft ).State;
		shouldActivate |= !Engine.InputManager.Instance.GetAction( m_menuRight ).State;
		shouldActivate |= ( m_MousePosition - m_LastMousePosition ).sqrMagnitude > 0.0f;
		shouldActivate |= Input.GetMouseButtonDown( 0 );
		return shouldActivate;
	}

	public override void ActivateModule()
	{
		base.ActivateModule();
		m_MousePosition = Input.mousePosition;
		m_LastMousePosition = Input.mousePosition;

		m_actionConfirm = Engine.InputManager.Instance.GetAction( m_confirm );
		m_actionCancel = Engine.InputManager.Instance.GetAction( m_cancel );
		m_actionMenuUp = Engine.InputManager.Instance.GetAction( m_menuUp );
		m_actionMenuDown = Engine.InputManager.Instance.GetAction( m_menuDown );
		m_actionMenuLeft = Engine.InputManager.Instance.GetAction( m_menuLeft );
		m_actionMenuRight = Engine.InputManager.Instance.GetAction( m_menuRight );

		GameObject toSelect = eventSystem.currentSelectedGameObject;
		if ( toSelect == null )
		{
			toSelect = eventSystem.firstSelectedGameObject;
		}

		eventSystem.SetSelectedGameObject( toSelect, GetBaseEventData() );
	}

	public override void DeactivateModule()
	{
		base.DeactivateModule();
		ClearSelection();
	}

	public override void Process()
	{
		bool usedEvent = SendUpdateEventToSelectedObject();

		if ( eventSystem.sendNavigationEvents )
		{
			if ( !usedEvent )
			{
				usedEvent |= SendMoveEventToSelectedObject();
			}
			if ( !usedEvent )
			{
				SendSubmitEventToSelectedObject();
			}
		}

		ProcessMouseEvent();
	}

	protected bool SendSubmitEventToSelectedObject()
	{
		if ( eventSystem.currentSelectedGameObject == null )
		{
			return false;
		}

		var data = GetBaseEventData();
		if ( m_actionConfirm.Down )
		{
			ExecuteEvents.Execute( eventSystem.currentSelectedGameObject, data, ExecuteEvents.submitHandler );
		}

		if ( m_actionCancel.Down )
		{
			ExecuteEvents.Execute( eventSystem.currentSelectedGameObject, data, ExecuteEvents.cancelHandler );
		}

		return data.used;
	}

	private Vector2 GetRawMoveVector()
	{
		Vector2 move = Vector2.zero;
		move.x = 0.0f;
		if ( m_actionMenuLeft.Down )
		{
			move.x -= 1.0f;
		}
		if ( m_actionMenuRight.Down )
		{
			move.x += 1.0f;
		}
		move.y = 0.0f;
		if ( m_actionMenuDown.Down )
		{
			move.y -= 1.0f;
		}
		if ( m_actionMenuUp.Down )
		{
			move.y += 1.0f;
		}
		return move;
	}

	protected bool SendMoveEventToSelectedObject()
	{
		float time = Time.unscaledTime;

		Vector2 movement = GetRawMoveVector();
		if ( Mathf.Approximately( movement.x, 0f ) && Mathf.Approximately( movement.y, 0f ) )
		{
			m_ConsecutiveMoveCount = 0;
			return false;
		}

		// If user pressed key again, always allow event
		bool allow = m_actionMenuUp.Down || m_actionMenuDown.Down || m_actionMenuLeft.Down || m_actionMenuRight.Down;
		bool similarDir = ( Vector2.Dot( movement, m_LastMoveVector ) > 0 );
		if ( !allow )
		{
			// Otherwise, user held down key or axis.
			// If direction didn't change at least 90 degrees, wait for delay before allowing consequtive event.
			if ( similarDir && m_ConsecutiveMoveCount == 1 )
			{
				allow = ( time > m_PrevActionTime + m_RepeatDelay );
			}
			// If direction changed at least 90 degree, or we already had the delay, repeat at repeat rate.
			else
			{
				allow = ( time > m_PrevActionTime + 1f / m_InputActionsPerSecond );
			}
		}
		if ( !allow )
		{
			return false;
		}

		// Debug.Log(m_ProcessingEvent.rawType + " axis:" + m_AllowAxisEvents + " value:" + "(" + x + "," + y + ")");
		var axisEventData = GetAxisEventData( movement.x, movement.y, 0.6f );
		ExecuteEvents.Execute( eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler );
		if ( !similarDir )
		{
			m_ConsecutiveMoveCount = 0;
		}

		m_ConsecutiveMoveCount++;
		m_PrevActionTime = time;
		m_LastMoveVector = movement;
		return axisEventData.used;
	}

	protected void ProcessMouseEvent()
	{
		ProcessMouseEvent( 0 );
	}

	protected void ProcessMouseEvent( int id )
	{
		var mouseData = GetMousePointerEventData( id );
		var leftButtonData = mouseData.GetButtonState( PointerEventData.InputButton.Left ).eventData;

		// Process the first mouse button fully
		ProcessMousePress( leftButtonData );
		ProcessMove( leftButtonData.buttonData );
		ProcessDrag( leftButtonData.buttonData );

		// Now process right / middle clicks
		ProcessMousePress( mouseData.GetButtonState( PointerEventData.InputButton.Right ).eventData );
		ProcessDrag( mouseData.GetButtonState( PointerEventData.InputButton.Right ).eventData.buttonData );
		ProcessMousePress( mouseData.GetButtonState( PointerEventData.InputButton.Middle ).eventData );
		ProcessDrag( mouseData.GetButtonState( PointerEventData.InputButton.Middle ).eventData.buttonData );

		if ( !Mathf.Approximately( leftButtonData.buttonData.scrollDelta.sqrMagnitude, 0.0f ) )
		{
			var scrollHandler = ExecuteEvents.GetEventHandler<IScrollHandler>( leftButtonData.buttonData.pointerCurrentRaycast.gameObject );
			ExecuteEvents.ExecuteHierarchy( scrollHandler, leftButtonData.buttonData, ExecuteEvents.scrollHandler );
		}
	}

	protected bool SendUpdateEventToSelectedObject()
	{
		if ( eventSystem.currentSelectedGameObject == null )
		{
			return false;
		}

		var data = GetBaseEventData();
		ExecuteEvents.Execute( eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler );
		return data.used;
	}

	/// <summary>
	/// Process the current mouse press.
	/// </summary>
	protected void ProcessMousePress( MouseButtonEventData data )
	{
		var pointerEvent = data.buttonData;
		var currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;

		// PointerDown notification
		if ( data.PressedThisFrame() )
		{
			pointerEvent.eligibleForClick = true;
			pointerEvent.delta = Vector2.zero;
			pointerEvent.dragging = false;
			pointerEvent.useDragThreshold = true;
			pointerEvent.pressPosition = pointerEvent.position;
			pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;

			DeselectIfSelectionChanged( currentOverGo, pointerEvent );

			// search for the control that will receive the press
			// if we can't find a press handler set the press
			// handler to be what would receive a click.
			var newPressed = ExecuteEvents.ExecuteHierarchy( currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler );

			// didnt find a press handler... search for a click handler
			if ( newPressed == null )
			{
				newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>( currentOverGo );
			}

			// Debug.Log("Pressed: " + newPressed);

			float time = Time.unscaledTime;

			if ( newPressed == pointerEvent.lastPress )
			{
				var diffTime = time - pointerEvent.clickTime;
				if ( diffTime < 0.3f )
				{
					++pointerEvent.clickCount;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}

				pointerEvent.clickTime = time;
			}
			else
			{
				pointerEvent.clickCount = 1;
			}

			pointerEvent.pointerPress = newPressed;
			pointerEvent.rawPointerPress = currentOverGo;

			pointerEvent.clickTime = time;

			// Save the drag handler as well
			pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>( currentOverGo );

			if ( pointerEvent.pointerDrag != null )
			{
				ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag );
			}
		}

		// PointerUp notification
		if ( data.ReleasedThisFrame() )
		{
			// Debug.Log("Executing pressup on: " + pointer.pointerPress);
			ExecuteEvents.Execute( pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler );

			// Debug.Log("KeyCode: " + pointer.eventData.keyCode);

			// see if we mouse up on the same element that we clicked on...
			var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>( currentOverGo );

			// PointerClick and Drop events
			if ( pointerEvent.pointerPress == pointerUpHandler && pointerEvent.eligibleForClick )
			{
				ExecuteEvents.Execute( pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler );
			}
			else if ( pointerEvent.pointerDrag != null && pointerEvent.dragging )
			{
				ExecuteEvents.ExecuteHierarchy( currentOverGo, pointerEvent, ExecuteEvents.dropHandler );
			}

			pointerEvent.eligibleForClick = false;
			pointerEvent.pointerPress = null;
			pointerEvent.rawPointerPress = null;

			if ( pointerEvent.pointerDrag != null && pointerEvent.dragging )
			{
				ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler );
			}

			pointerEvent.dragging = false;
			pointerEvent.pointerDrag = null;

			// redo pointer enter / exit to refresh state
			// so that if we moused over somethign that ignored it before
			// due to having pressed on something else
			// it now gets it.
			if ( currentOverGo != pointerEvent.pointerEnter )
			{
				HandlePointerExitAndEnter( pointerEvent, null );
				HandlePointerExitAndEnter( pointerEvent, currentOverGo );
			}
		}
	}

	public void PlayGame( string _sceneName )
	{
		SceneManager.LoadScene( _sceneName );
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
