using UnityEngine;

/*
 *  Mudar o calculo da gravidade para ser chamado apenas uma vez durante o método da movimentação
 */

public class Movement : MonoBehaviour
{
    [SerializeField] private float _characterSpeed = 3f;
    [SerializeField] private float _gravity = -22f;

    [Header("Ground Checking")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector3 _checkSpherePosition;
    [SerializeField] private float _checkSphereRadius;

    private CharacterController _characterController;
    private Vector3 playerVelocity = Vector3.zero;  // Up/Down movement velocity

    public float InputX { get; private set; }
    public float InputY { get; private set; }

    //Animation
    private Animator _animator;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void Move(float inputX, float inputY)
    {
        if (_characterController.enabled == false) return;

        //Check gravity
        bool isGrounded = Physics.CheckSphere(_checkSpherePosition + transform.position, _checkSphereRadius, _groundLayer);  //verify if is in the ground

        if (isGrounded && playerVelocity.y < 0f)
            playerVelocity.y = -2f;

        //Player Input
        InputX = inputX; //Input.GetAxisRaw("Horizontal");
        InputY = inputY; //Input.GetAxisRaw("Vertical");

        //Final movement calculations
        Vector3 move = new Vector3(InputX, 0f, InputY);

        if (move.magnitude > 0f)
        {
            move.Normalize();  //This part is responsible for make the diagonals become part of the circle of values and not de addition (1,1) -> (0.707,0.0707)
            _characterController.Move(move * _characterSpeed * Time.deltaTime);
            //som passos
            FMODUnity.RuntimeManager.PlayOneShot("event:/Passos_concreto");
        }

        //Aplly gravity
        playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);

        //## Animations ##

        //Calcula o valor do movimento de acordo com a alteração da direção
        float velocityX = Vector3.Dot(move.normalized, transform.right);
        float velocityZ = Vector3.Dot(move.normalized, transform.forward);

        //Altera os valores na animação
        _animator.SetFloat("xVelocity", velocityX, .1f, Time.deltaTime);
        _animator.SetFloat("zVelocity", velocityZ, .1f, Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        //Check gravity gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_checkSpherePosition + transform.position, _checkSphereRadius);
    }
}