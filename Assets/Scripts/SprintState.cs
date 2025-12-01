using UnityEngine;
public class SprintState : State
{
    float gravityValue;
    Vector3 currentVelocity;
    private const float _threshold = 0.01f;
    public bool LockCameraPosition = false;

    bool grounded;
    bool sprint;
    float playerSpeed;
    bool sprintJump;
    Vector3 cVelocity;
            private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return character.playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }
    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sprint = false;
        sprintJump = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;        
    }

    public override void HandleInput()
    {
        base.Enter();
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);
        CameraRotation();
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
        if (sprintAction.triggered || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
		if (jumpAction.triggered)
		{
            sprintJump = true;
        }
    }

    

    public override void LogicUpdate()
    {
        if (sprint)
        {
            character.animator.SetFloat("speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
		}
		else
		{
            stateMachine.ChangeState(character.standing);
        }
		if (sprintJump)
		{
            stateMachine.ChangeState(character.sprintjumping);
        }
    }
private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (lookAction.ReadValue<Vector2>().x + lookAction.ReadValue<Vector2>().y >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                character._cinemachineTargetYaw += lookAction.ReadValue<Vector2>().x * deltaTimeMultiplier;
                character._cinemachineTargetPitch += lookAction.ReadValue<Vector2>().y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            character._cinemachineTargetYaw = Character.ClampAngle(character._cinemachineTargetYaw, float.MinValue, float.MaxValue);
            character._cinemachineTargetPitch = Character.ClampAngle(character._cinemachineTargetPitch, character.BottomClamp, character.TopClamp);

            // Cinemachine will follow this target
            character.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(character._cinemachineTargetPitch + character.CameraAngleOverride, character._cinemachineTargetYaw, 0.0f);
        }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);


        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
}
