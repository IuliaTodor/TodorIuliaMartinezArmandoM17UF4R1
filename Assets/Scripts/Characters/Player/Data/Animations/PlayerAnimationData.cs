using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerAnimationData
    {
        [SerializeField] private string groundedParameterName = "Grounded";
        [SerializeField] private string movingParameterName = "Moving";
        [SerializeField] private string stoppingParameterName = "Stopping";
        [SerializeField] private string landingParameterName = "Landing";
        [SerializeField] private string airParameterName = "Air";

        [SerializeField] private string idleParameterName = "isIdling";
        [SerializeField] private string dashParameterName = "isDashing";
        [SerializeField] private string walkParameterName = "isWalking";
        [SerializeField] private string runParameterName = "isRunning";
        [SerializeField] private string sprintParameterName = "isSprinting";
        [SerializeField] private string mediumStopParameterName = "isMediumStopping";
        [SerializeField] private string hardStopParameterName = "isHardStopping";
        [SerializeField] private string rollParameterName = "isRolling";
        [SerializeField] private string hardLandParameterName = "isHardLanding";

        [SerializeField] private string dancingParameterName = "isDancing";

        [SerializeField] private string deadParameterName = "isDead";

        [SerializeField] private string fallParameterName = "isFalling";

        [SerializeField] private string aimParameterName = "isAiming";

        public int groundedParameterHash { get; private set; }
        public int movingParameterHash { get; private set; }
        public int stoppingParameterHash { get; private set; }
        public int landingParameterHash { get; private set; }
        public int airParameterHash { get; private set; }

        public int idleParameterHash { get; private set; }
        public int dashParameterHash { get; private set; }
        public int walkParameterHash { get; private set; }
        public int runParameterHash { get; private set; }
        public int sprintParameterHash { get; private set; }
        public int mediumStopParameterHash { get; private set; }
        public int hardStopParameterHash { get; private set; }
        public int rollParameterHash { get; private set; }
        public int hardLandParameterHash { get; private set; }
        public int dancingParameterHash { get; private set; }
        public int deadParameterHash { get; private set; }

        public int fallParameterHash { get; private set; }
        public int aimParameterHash { get; private set; }

        public void Initialize()
        {
            groundedParameterHash = Animator.StringToHash(groundedParameterName);
            movingParameterHash = Animator.StringToHash(movingParameterName);
            stoppingParameterHash = Animator.StringToHash(stoppingParameterName);
            landingParameterHash = Animator.StringToHash(landingParameterName);
            airParameterHash = Animator.StringToHash(airParameterName);

            idleParameterHash = Animator.StringToHash(idleParameterName);
            dashParameterHash = Animator.StringToHash(dashParameterName);
            walkParameterHash = Animator.StringToHash(walkParameterName);
            runParameterHash = Animator.StringToHash(runParameterName);
            sprintParameterHash = Animator.StringToHash(sprintParameterName);
            mediumStopParameterHash = Animator.StringToHash(mediumStopParameterName);
            hardStopParameterHash = Animator.StringToHash(hardStopParameterName);
            rollParameterHash = Animator.StringToHash(rollParameterName);
            hardLandParameterHash = Animator.StringToHash(hardLandParameterName);

            dancingParameterHash = Animator.StringToHash(dancingParameterName);

            deadParameterHash = Animator.StringToHash(deadParameterName);

            fallParameterHash = Animator.StringToHash(fallParameterName);

            aimParameterHash = Animator.StringToHash(aimParameterName);
        }
    }
}
