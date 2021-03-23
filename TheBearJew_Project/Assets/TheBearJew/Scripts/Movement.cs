using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _characterSpeed = 5f;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Ground Checking")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector3 _checkPosition;
    [SerializeField] private float _checkRadius;

    private CharacterController _characterController;
    private Vector3 playerVelocity = Vector3.zero;  //Up Down velocity

    private void Start() => _characterController = GetComponent<CharacterController>();

    private void Update() => Move();

    private void Move()
    {
        //Check gravity
        var isGrounded = Physics.CheckSphere(_checkPosition + transform.position, _checkRadius, _groundLayer);  //verify if is in the ground
        
        if (isGrounded && playerVelocity.y < 0f)
            playerVelocity.y = -2f;

        //Player Input
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        //Final movement calculations
        var move = new Vector3(xInput, 0f, yInput);
        _characterController.Move(move * _characterSpeed * Time.deltaTime);

        //Aplly gravity
        playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //Check gravity gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_checkPosition + transform.position, _checkRadius);
    }

}