namespace GenshintImpact2
{
    public class PlayerMovementSM : StateMachine
    {
        public Player player {  get;}
        public PlayerStatesReusableData reusableData { get;}

        public PlayerIdleState idlingState { get; }
        public PlayerDashingState dashingState { get; }
        public PlayerWalkingState walkingState { get; }
        public PlayerRunningState runningState { get; }
        public PlayerSprintingState sprintingState { get; }
        public PlayerLightStoppingState lightStoppingState { get; }
        public PlayerMediumStoppingState mediumStoppingState { get; }
        public PlayerHardStoppingState hardStoppingState { get; }

        public PlayerJumpingState jumpingState { get; }
        public PlayerFallingState fallingState { get; }
        public PlayerLightLandingState lightLandingState { get; }
        public PlayerHardLandingState hardLandingState { get; }
        public PlayerRollingState rollingState { get; }
        public PlayerCombatState combatState { get; }
        public PlayerCombatMovementState combatMovementState { get; }
        public PlayerDancingState dancingState { get; }
        public PlayerDeadState deadState { get; }

        //Referencia al player para acceder a sus propiedades
        public PlayerMovementSM(Player playerReference)
        {
            player = playerReference;
            reusableData = new PlayerStatesReusableData();

            //Constructor para todos los estados
            idlingState = new PlayerIdleState(this);

            dashingState = new PlayerDashingState(this);
            walkingState = new PlayerWalkingState(this);
            runningState = new PlayerRunningState(this);
            sprintingState = new PlayerSprintingState(this);

            lightStoppingState = new PlayerLightStoppingState(this);
            mediumStoppingState = new PlayerMediumStoppingState(this);
            hardStoppingState = new PlayerHardStoppingState(this);

            jumpingState = new PlayerJumpingState(this);
            fallingState = new PlayerFallingState(this);

            lightLandingState = new PlayerLightLandingState(this);
            hardLandingState = new PlayerHardLandingState(this);
            rollingState = new PlayerRollingState(this);

            combatState = new PlayerCombatState(this);
            combatMovementState = new PlayerCombatMovementState(this);

            dancingState = new PlayerDancingState(this);

            deadState = new PlayerDeadState(this);
        }
    }
}
