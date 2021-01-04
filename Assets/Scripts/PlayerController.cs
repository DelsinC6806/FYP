using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    #region SingleTon

    public static PlayerController instance;

     void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Player found!");
        }
        instance = this;
    }

    #endregion


    public Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private float Speed = 5.0f, RotationSpeed = 240.0f, JumpSpeed = 7.0f;

    public Interactable currentWeapon;

    public bool isAttacking;

    public AudioSource audioSource;

    public AudioClip[] clips;

    float rayDistance = 4f;

    public static bool drop;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        enableDrop();
        movement();
        playWalkingSound();
        useItem();
    }

    void movement()
    {

        _animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();


            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

            if (_characterController.isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;

                if (Input.GetButton("Jump"))
                {
                    _animator.SetBool("is_in_air", true);
                    audioSource.PlayOneShot(clips[1]);
                    _moveDirection.y = JumpSpeed;

                }
                else
                {
                    _animator.SetBool("is_in_air", false);
                    _animator.SetBool("run", move.magnitude > 0);
                }
            }
            else
            {
                Gravity = 20.0f;
            }


            _moveDirection.y -= Gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime); 
    }

    bool isAbleToControl()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    //default Animation Event
    public void DisableControl()
    {
        this.enabled = false;
    }

    public void EnableControl()
    {
        this.enabled = true;
    }

    void playWalkingSound()
    {
        if(_characterController.isGrounded == true &&_characterController.velocity.magnitude > 2f && audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(clips[0]);
        }
    }

    void enableDrop()
    {
        RaycastHit hit;
    
        if(Physics.Raycast(transform.position,transform.forward,out hit,rayDistance))
        {
            if (hit.transform != null)
            {
                drop = false;
            }
        }
        else
        {
            drop = true;
        }

    }

    void useItem()
    {
        if(currentWeapon != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentWeapon.use();
        }
    }

    void CancelIsAttacking()
    {
        isAttacking = false;
    }
}
