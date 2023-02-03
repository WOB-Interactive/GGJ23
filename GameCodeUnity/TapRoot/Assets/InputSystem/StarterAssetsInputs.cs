using UnityEngine;
using System;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{

		public static event Action OnPausePressed;
		public static event Action OnInteractPressed; // to add once ready
		public static event Action OnActivatePower; // to add once ready

		public bool canMove = false;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;


		[Header("Custom Settings")]
		public bool paused;
		public bool powerActivate;


        private void OnEnable()
        {

			GameManager.GameStateChange += OnGameStateChangeHandler;
		}


        private void OnDisable()
        {

			GameManager.GameStateChange -= OnGameStateChangeHandler;
		}


		void OnGameStateChangeHandler(GameStates state)
		{
			switch (state)
			{
				case GameStates.Play:
					// MOVEABLE
					canMove = true;
					break;
				default:
					//not moving
					canMove = false;
					break;
			}
		}


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			if (canMove)
			{
				MoveInput(value.Get<Vector2>());
			}
			else
            {
				MoveInput(new Vector2());
            }
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook && canMove)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if (canMove)
			{
				JumpInput(value.isPressed);
			}
		}

		public void OnSprint(InputValue value)
		{
			if (canMove)
			{
				SprintInput(value.isPressed);
			}
		}

        public void OnPause(InputValue value)
        {
			paused = value.isPressed;
			if (value.isPressed)
				OnPausePressed?.Invoke();
		}

		public void OnPowersActivate(InputValue value)
        {
			powerActivate = value.isPressed;
			if (value.isPressed)
				OnActivatePower?.Invoke();
		}
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}