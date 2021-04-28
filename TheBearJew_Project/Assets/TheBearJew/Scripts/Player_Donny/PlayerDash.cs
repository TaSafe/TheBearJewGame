using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Movement _movement;
    private CharacterController _characterCotroller;
    private Vector3 _rollDirection;
    private bool _isRolling;
    private float _rollSpeed = 10f;
    private float _rollingSpeed;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _characterCotroller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rollDirection = new Vector3(_movement.InputX, 0f, _movement.InputY).normalized;
            _rollingSpeed = _rollSpeed;
            _characterCotroller.enabled = false;
            _isRolling = true;
        }

        Roll();
    }

    void Roll()
    {
        if (_isRolling)
        {
            //if (Physics.Raycast(transform.position + (rollDirection * .75f), rollDirection, out RaycastHit hit, 1.5f))
            //{

            //}

            transform.position += (_rollDirection * 2f) * _rollingSpeed * Time.deltaTime;
            _rollingSpeed -= _rollingSpeed * 10f * Time.deltaTime;
            if (_rollingSpeed < 5f)
            {
                _characterCotroller.enabled = true;
                _isRolling = false;
            }
        }
    }

}