using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Controle de Movimento")]
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    //public bool isGrounded;

    [Header("Animation Smoothing")]
    /*[Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;*/
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    [Header("Itens Do Ataque")]
    public Transform magicAttackSpawn;
    public GameObject magicAttackObject;
    public Rigidbody magicAttackRigd;
    /*public Transform meleeEffectMuzzle;
    public GameObject meleeAttackEffect;*/

    //[Header("Canvas de derrota")]
    //public GameObject defeatCanvas;

    private float verticalVel;
    private Vector3 moveVector;
    // Animator indexes
    private int speedIndex;
    private int moveIndex;
    private int sprintIndex;
    private int placeTrapIndex;
    private int magicAttackIndex;
    private int movePlaceTrapIndex;
    private int moveMagicAttackIndex;
    //private int jumpIndex;
   // private int damageIndex;
    private int deadIndex;
    private bool canMove;
    [SerializeField]
    private bool canAttack;
    private bool takeHit;
    private bool canDie;
    private bool canRespawn;
    private bool jumping;
    public bool isPlacingTraps;

    // Use this for initialization
    void Start()
    {
        isPlacingTraps = MainData.placingTraps;
        anim = GetComponent<Animator>();
        //cam = Camera.main;
        controller = GetComponent<CharacterController>();
        speedIndex = Animator.StringToHash("speed");
        moveIndex = Animator.StringToHash("move");
        sprintIndex = Animator.StringToHash("sprint");
        placeTrapIndex = Animator.StringToHash("placeTrap");
        magicAttackIndex = Animator.StringToHash("magicAttack");
        movePlaceTrapIndex = Animator.StringToHash("movePlaceTrap");
        moveMagicAttackIndex = Animator.StringToHash("moveMagicAttack");
        //jumpIndex = Animator.StringToHash("jump");
        //damageIndex = Animator.StringToHash("damage");
        deadIndex = Animator.StringToHash("dead");
        canMove = true;
        canAttack = true;
        takeHit = true;
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded == false)
        {
            Vector3 moveVector = Physics.gravity;

            controller.Move(moveVector * Time.deltaTime);

        }

        isPlacingTraps = MainData.placingTraps;

        if (canMove == true)
        {
            //Calculate Input Vectors
            InputX = Input.GetAxis("Horizontal");
            InputZ = Input.GetAxis("Vertical");

            //Calculate the Input Magnitude
            speed = new Vector2(InputX, InputZ).sqrMagnitude;
            if (InputZ < 0)
            {
                speed = speed * -1;
            }
            else if (InputZ > 0)
            {
                speed = speed * 1;
            }

            InputMagnitude();
            PlayerMoveAndRotation();
        }

        /*if (isPlacingTraps == false)
        {*/
            //criar um void proprio para o ataque ou um script 
            VerifyAttack();
        //}
    }


    //Movimentação
    void PlayerMoveAndRotation()
    {
        /*InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");*/
        bool s = Input.GetKey(KeyCode.LeftShift) ? true : false;

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        if (InputX != 0 && InputZ >= 0)
        {
            desiredMoveDirection = forward * InputZ + right * InputX;
        }
        else
        {
            desiredMoveDirection = forward + right * InputX;
        }

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
        else if (blockRotationPlayer == true)
        {
            Vector3 ajusteRotation = cam.transform.rotation.eulerAngles;
            if (ajusteRotation.x <= 0)
            {
                Quaternion novaRotação = Quaternion.Euler(ajusteRotation);
                transform.rotation = novaRotação;
            }
            else
            {
                Quaternion novaRotação = Quaternion.Euler(0,ajusteRotation.y,0);
                transform.rotation = novaRotação;
            }
        }
        anim.SetBool(sprintIndex, s);
    }

    void InputMagnitude()
    {
        /*
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //Calculate the Input Magnitude
        speed = new Vector2(InputX, InputZ).sqrMagnitude;*/

        //Physically move player
        if (speed > allowPlayerRotation)
        {
            anim.SetFloat(speedIndex, Mathf.Clamp(speed, -1, 1), StartAnimTime, Time.deltaTime);
            //PlayerMoveAndRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            anim.SetFloat(speedIndex, Mathf.Clamp(speed, -1, 1), StopAnimTime, Time.deltaTime);
        }
        anim.SetBool(moveIndex, Mathf.Abs(speed) >= 0.2F);
    }
    //Ataque
    public void VerifyAttack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Debug.Log("Era pra ataca");
            if (speed != 0.0f)
            {
                anim.SetLayerWeight(1, 1.0f);
                anim.SetTrigger(moveMagicAttackIndex);
                canAttack = false;
                blockRotationPlayer = true;
            }
            else
            {
                anim.SetTrigger(magicAttackIndex);
                canAttack = false;
                blockRotationPlayer = true;
            }
            takeHit = false;
        }
    }

    public void ShowPlacingTrap()
    {
           if (speed != 0.0f)
           {
               anim.SetLayerWeight(1, 1.0f);
               anim.SetTrigger(movePlaceTrapIndex);
               canAttack = false;
           }
           else
           {
               anim.SetTrigger(placeTrapIndex);
               canAttack = false;
           }
           takeHit = false;
    }

    public void CreateAttackEffect()
    {
        var pos = magicAttackSpawn.position;
        //ar rot = gameObject.transform.rotation;

        //Instantiate(magicAttackObject, pos, Quaternion.identity/*rot*/);

        var b = Instantiate(magicAttackRigd, pos, magicAttackSpawn.rotation) /*rot*/ as Rigidbody;
        b.AddForce(magicAttackSpawn.forward * 500);
    }

    public void AllowMovement()
    {
        anim.SetLayerWeight(1, 0.0f);
        canAttack = true;
        takeHit = true;
        blockRotationPlayer = false;
    }

    public void EndOfLevel()
    {
        canMove = false;
        canAttack = false;
        takeHit = false;
        canDie = false;
        canRespawn = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //defeatCanvas.SetActive(true);
    }


}
