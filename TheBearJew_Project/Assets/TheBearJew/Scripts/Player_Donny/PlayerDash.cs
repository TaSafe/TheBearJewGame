using UnityEngine;

//TODO: Colocar a animação
//TODO: Acertar o timing da rolagem
//TODO: Bloquear alguns inputs do jogador enquanto está rolando

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
        Debug.DrawRay(transform.position, _rollDirection * 5f, Color.blue);

        if (_isRolling)
        {
            //TODO: Filtar esse Raycast para pegar apenas paredes e inimigos
            //TODO: Tornar as variáveis que controlam os parâmetros de Rolar em SerializeField pra editar pelo inspector
            if (Physics.Raycast(transform.position, _rollDirection, out RaycastHit hit, 5f))
            {
                if (hit.collider != null)
                {
                    _characterCotroller.enabled = true;
                    _isRolling = false;
                    return;
                }
            }

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