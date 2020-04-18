using UnityEngine;

// Hacky
public class PlayerSoundController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string forestFootstepEvent = "";
    [FMODUnity.EventRef]
    public string waterFootstepEvent = "";
    [FMODUnity.EventRef]
    public string musicBGM = "";
    public FMOD.Studio.EventInstance Music;



    [FMODUnity.EventRef]
    public string windEvent = "";

    public GameObject Player;
    public string waterTag = "Water";
    private bool _isInWater = false;

    public float walkVelocityThreshold = .5f; // If Velocity magnitude is more than this value, he's considered walking
    
    
    //public float walkSoundCooldown = 0.5f; // A walking clip can only be played every [walkSoundCooldown] seconds
    public AnimationCurve walkSoundCooldownVelocityCurve;
    
    private float _walkSoundCountdown = 0;

    //private FirstPersonAIO fpAIO;
    private Rigidbody _rb;
    private CharacterController _characterController;
    private bool _hasRb;
    private bool _hasCharContr;
    
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(musicBGM, Player);
        //Music = FMODUnity.RuntimeManager.CreateInstance(musicBGM);
        //Music.start();
        //Music.setParameterByName("Music Section", 1f);
        FMODUnity.RuntimeManager.PlayOneShotAttached(windEvent, Player);
        //fpAIO = GetComponent<FirstPersonAIO>();
        _rb = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _hasRb = _rb != null;
        _hasCharContr = _characterController != null;

    }

    // Update is called once per frame
    void Update()
    {
        if (_walkSoundCountdown > 0)
            _walkSoundCountdown -= Time.deltaTime;
        
        if (_walkSoundCountdown <= 0 && _hasRb && IsWalking())
        {
            if (_isInWater && waterFootstepEvent != "")
            {
                FMODUnity.RuntimeManager.PlayOneShot(waterFootstepEvent, transform.position);
            }else if (forestFootstepEvent != "")
            {
                FMODUnity.RuntimeManager.PlayOneShot(forestFootstepEvent, transform.position);
            }

            _walkSoundCountdown = walkSoundCooldownVelocityCurve.Evaluate(GetCurrentVelocityMagnitude());//walkSoundCooldown;
        }
    }

    public float GetCurrentVelocityMagnitude()
    {
        var vec2Velocity = new Vector2(0,0);

        if (_hasCharContr)
        {
            vec2Velocity.x = _characterController.velocity.x;
            vec2Velocity.y = _characterController.velocity.z;
        }else if (_hasRb)
        {
            vec2Velocity.x = _rb.velocity.x;
            vec2Velocity.y = _rb.velocity.z;
        }
        
        return vec2Velocity.magnitude;
    }

    public bool IsWalking()
    {
        if (!_hasRb) return false;

        return /*fpAIO.IsGrounded && */GetCurrentVelocityMagnitude() >= walkVelocityThreshold;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(waterTag))
        {
            _isInWater = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(waterTag))
        {
            _isInWater = false;
        }
    }
}
