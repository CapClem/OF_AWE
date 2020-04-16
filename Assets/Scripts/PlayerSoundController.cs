using UnityEngine;

// Hacky
public class PlayerSoundController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string forestFootstepEvent = "";
    [FMODUnity.EventRef]
    public string waterFootstepEvent = "";


    public string waterTag = "Water";
    private bool _isInWater = false;

    public float walkVelocityThreshold = .5f; // If Velocity magnitude is more than this value, he's considered walking
    
    
    //public float walkSoundCooldown = 0.5f; // A walking clip can only be played every [walkSoundCooldown] seconds
    public AnimationCurve walkSoundCooldownVelocityCurve;
    
    private float _walkSoundCountdown = 0;

    //private FirstPersonAIO fpAIO;
    private Rigidbody _rb;
    private bool _hasRb;
    
    void Start()
    {
        //fpAIO = GetComponent<FirstPersonAIO>();
        _rb = GetComponent<Rigidbody>();
        _hasRb = _rb != null;
        
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
                FMODUnity.RuntimeManager.PlayOneShot(waterFootstepEvent, _rb.transform.position);
            }else if (forestFootstepEvent != "")
            {
                FMODUnity.RuntimeManager.PlayOneShot(forestFootstepEvent, _rb.transform.position);
            }

            _walkSoundCountdown = walkSoundCooldownVelocityCurve.Evaluate((new Vector2(_rb.velocity.x, _rb.velocity.z)).magnitude);//walkSoundCooldown;
        }
    }

    public bool IsWalking()
    {
        if (!_hasRb) return false;

        return /*fpAIO.IsGrounded && */(new Vector2(_rb.velocity.x, _rb.velocity.z)).magnitude >= walkVelocityThreshold;
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
