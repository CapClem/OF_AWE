using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        private CharacterController _charController;

        public float gravity = -9.81f;
        public float jumpForce = 20;

        public bool running = false;
        
        public Vector2 maxWalkSpeed = new Vector3(1,2); // Backward, Fwd
        public Vector2 maxRunSpeed = new Vector3(2,3); // Backward, Fwd
        public float turnSpeed = 45; //(Angles/Sec)
        public float acceleration = 2;
        public Vector3 inputMovement; // 0-1

        private bool _isGrounded;
        private float _distToGround = 0.5f;
        
        public AudioSource audioSource;

        public bool playFootStepSounds = false;
        //public AudioClip movingSound;

        private void Awake()
        {
            _charController = GetComponent<CharacterController>();

            if(_charController != null)
                _distToGround = _charController.bounds.extents.y;
            //audioSource = GetComponent<AudioSource>();
            //audioSource.clip = movingSound;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            _isGrounded = IsGrounded();
            Move();

            if (audioSource == null)
                return;
            
            // HACKY
            if (_charController.velocity.magnitude > 0.1 && playFootStepSounds)
            {
                if(!audioSource.isPlaying)
                    audioSource.Play();
            }
            else if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            //Debug.Log(_charController.velocity);
        }

        private Vector3 _movementVector = new Vector3(0,0, 0);
        protected virtual void Move()
        {
        
            // TODO Handle Rotation
            Vector3 rotationVector = Vector3.zero;
            if (inputMovement.x > 0)
                rotationVector.y = turnSpeed * Time.deltaTime;
            else if(inputMovement.x < 0)
                rotationVector.y = -turnSpeed * Time.deltaTime;
            
            transform.Rotate(rotationVector);
            
            // Movement

            
            if (inputMovement.z > 0)
                _movementVector.z += acceleration * Time.deltaTime;
            else if(inputMovement.z < 0)
                _movementVector.z -= acceleration * Time.deltaTime;
            else
                _movementVector.z = Mathf.MoveTowards(_movementVector.x, 0, 2 * acceleration);
            _movementVector.z = running ? Mathf.Clamp(_movementVector.z, -Mathf.Abs(maxRunSpeed.x), Mathf.Abs(maxRunSpeed.y)) : Mathf.Clamp(_movementVector.z, -Mathf.Abs(maxWalkSpeed.x), Mathf.Abs(maxWalkSpeed.y));

            
            Vector3 move = transform.TransformVector(_movementVector);
            
            if (inputMovement.y > 0 && _isGrounded)
                move.y += jumpForce;
            move.y = gravity;

            _charController.SimpleMove(move);
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, out var hit, _distToGround + 0.05f); // TODO
        }
    }
}
