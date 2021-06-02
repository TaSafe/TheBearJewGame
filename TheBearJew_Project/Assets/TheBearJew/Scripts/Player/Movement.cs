using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *  Mudar o calculo da gravidade para ser chamado apenas uma vez durante o m�todo da movimenta��o
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
    public Animator Animator { get; private set; }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
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
            Scene cena = SceneManager.GetActiveScene();
            if (cena.name == "Piso_0F_Esgoto")
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Passos_metal");
            }
            else if ( cena.name == "Subida_Para_a_Abadia")
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Passos_grama");
            }
            else
                FMODUnity.RuntimeManager.PlayOneShot("event:/Passos_concreto");
        }

        //Aplly gravity
        playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);

        //## Animations ##

        //Calcula o valor do movimento de acordo com a altera��o da dire��o
        float velocityX = Vector3.Dot(move.normalized, transform.right);
        float velocityZ = Vector3.Dot(move.normalized, transform.forward);

        //Altera os valores na anima��o
        Animator.SetFloat("xVelocity", velocityX, .1f, Time.deltaTime);
        Animator.SetFloat("zVelocity", velocityZ, .1f, Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        //Check gravity gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_checkSpherePosition + transform.position, _checkSphereRadius);
    }
}