

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    /// <summary>
    /// SUPER HACKY CHEAP RABBIT AI
    /// </summary>
    public class SimpleRabbitAI : MonoBehaviour
    {
        public bool debugEnableMovementInput;
        
        public bool scaredOfPlayer = false;
        public float viewDistance = 10f;
        public LayerMask obstacleLayers;
        public List<string> scaredOfTags;

        public float targetReachedThreshold = 2f;

        public AudioSource audioSource;
        //public AudioClip eatingSound;
        public float eatingSoundDuration = 0;
        public float eatingSoundCooldown = 0;
        
        private MovementController _movementController;

        public enum AIState
        {
            Idle, HopAround, RunAway
        }

        public AIState defaultState = AIState.Idle;
        public AIState currentState;
        

        private Vector3 _currentTargetLocation;
        public float angleToTargetThreshold = 5;
        public float angleToTargetMoveThreshold = 30; // Rabbit can only move & turn at the same time if angle to target is <= this angle
        public float maxRotationStep = 30;


        public float eatingTime = 4;
        public float movingTime = 15;
        private float eatingCountdown = 0;
        private float movingCountdown = 0;

        public float newTargetSearchRadius = 5;
        // Start is called before the first frame update
        void Start()
        {
            _movementController = GetComponent<MovementController>();
            currentState = defaultState;
            //audioSource = GetComponent<AudioSource>();
/*

            if (eatingSound != null)
                eatingSoundDuration = eatingSound.length;*/
        }

        // Update is called once per frame
        void Update()
        {
            if (eatingSoundCooldown > 0)
                eatingSoundCooldown -= Time.deltaTime;
            
            if (debugEnableMovementInput)
                DebugInputMovement();
            
            GetSensoryInput();
            DoStateAction();

        }

        private void DebugInputMovement()
        {
            Vector3 inputMovement = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
                inputMovement.z = 1;
            else if (Input.GetKey(KeyCode.S))
                inputMovement.z = -1;
            
            if (Input.GetKey(KeyCode.A))
                inputMovement.x = -1;
            else if (Input.GetKey(KeyCode.D))
                inputMovement.x = 1;
            
            _movementController.inputMovement = inputMovement;
        }

        private void GetSensoryInput()
        {
            // Sensory Input
            if (Physics.Raycast(transform.position, transform.forward, out var hit, viewDistance, obstacleLayers))
            {
                if (scaredOfTags?.Contains(hit.collider.tag) ?? false)
                {
                    
                }
            }
        }

        private void DoStateAction()
        {
            switch (currentState)
            {
                case AIState.HopAround: DoHopAroundState();
                    break;
                case AIState.RunAway: DoRunAwayState();
                    break;
                case AIState.Idle: break;
            }
        }

        private void DoHopAroundState()
        {

            if (eatingCountdown > 0)
            {
                
                eatingCountdown -= Time.deltaTime;

                if (eatingCountdown <= 0)
                {
                    audioSource?.Stop();
                    Debug.Log("STOP NOM");
                    movingCountdown = movingTime;
                    return;
                }
                
                // Eating Code
                _movementController.inputMovement.z = 0;
                
                // TODO Hacky, Inperformant
                if (audioSource != null && !audioSource.isPlaying/* && eatingSoundCooldown <= 0*/)
                {
                    audioSource.Play();
                    Debug.Log("PLAY NOM");
                    /*audioSource.PlayOneShot(eatingSound, 1f);
                    eatingSoundCooldown = eatingSoundDuration;*/
                }
                
            }else if (movingCountdown > 0)
            {
                
                movingCountdown -= Time.deltaTime;

                if (movingCountdown <= 0)
                {
                    eatingCountdown = eatingTime;
                    return;
                }
                
                // Moving Code
                var distToTarget = Vector3.Distance(transform.position, _currentTargetLocation);

                if (distToTarget <= targetReachedThreshold)
                {
                    _currentTargetLocation = GetNewTargetLocation();
                }
                
                MoveToTarget();
            }
            else
            {
                movingCountdown = movingTime;
            }
            
        }

        // Hacky
        private Vector3 GetNewTargetLocation()
        {
            Vector3 newTargetLoc = Vector3.zero;



            var sampleCounter = 0;
            Vector3 newDir = Quaternion.AngleAxis(Random.Range(0, 360), transform.up) * transform.forward;
            
            while (Physics.Raycast(transform.position, newDir, out var hit, newTargetSearchRadius) && sampleCounter < 20)
            {
                newDir = Quaternion.AngleAxis(Random.Range(0, 360), transform.up) * transform.forward;
                newTargetLoc = hit.point;
                sampleCounter++;
            }

            if (newTargetLoc.Equals(Vector3.zero))
            {
                newTargetLoc = transform.position + newDir*newTargetSearchRadius;
            }
            
            return newTargetLoc;
        }

        private void DoRunAwayState()
        {
            throw new System.NotImplementedException();
        }

        /*private void RotateToTarget()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(_currentTargetLocation - transform.position), maxRotationStep);
        }*/

        public float angleToTarget;
        private void MoveToTarget()
        {
            var groundedForwardDir = transform.forward;
            groundedForwardDir.y = 0;
            var groundedTargetDir = _currentTargetLocation - transform.position;
            groundedTargetDir.y = 0;
            angleToTarget = Vector3.SignedAngle(groundedForwardDir, groundedTargetDir,Vector3.up);
            if (angleToTarget > angleToTargetThreshold)
            {
                _movementController.inputMovement.x = 1;
                if(Mathf.Abs(angleToTarget) > angleToTargetMoveThreshold)
                    _movementController.inputMovement.z = 0;
                else
                    _movementController.inputMovement.z = 1;
            }
            else if (angleToTarget < -angleToTargetThreshold)
            {
                _movementController.inputMovement.x = -1;
                if(Mathf.Abs(angleToTarget) > angleToTargetMoveThreshold)
                    _movementController.inputMovement.z = 0;
                else
                    _movementController.inputMovement.z = 1;
            }
            else
            {
                _movementController.inputMovement.x = 0;
                _movementController.inputMovement.z = 1;
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_currentTargetLocation, 2);
            
            var distToTarget = Vector3.Distance(transform.position, _currentTargetLocation);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward*distToTarget);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, Quaternion.AngleAxis(angleToTarget, transform.up) * transform.forward);
        }
    }
}
