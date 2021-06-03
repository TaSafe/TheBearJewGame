using System.Collections;
using UnityEngine;

//TODO: Colocar a animação
//TODO: Acertar o timing da rolagem
//TODO: Bloquear alguns inputs do jogador enquanto está rolando

public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private float _rollDistance = 4f;
    [SerializeField] private float _rollSpeed = 10f;
    [SerializeField] private float _rayCastColisionDetectionDistance = 1f;
    [Tooltip("In seconds")][SerializeField] private float _coolDownTime;

    private Movement _movement;
    private CharacterController _characterCotroller;
    private Vector3 _rollDirection;
    private bool _isRolling;
    private bool _isCoolingDown;
    private float _rollingSpeed;

    void Start()
    {        
        _movement = GetComponent<Movement>();
        _characterCotroller = GetComponent<CharacterController>();

        UiHUD.Instance.CooldownProgressBar.SetMaxValue(_coolDownTime);
        UiHUD.Instance.CooldownProgressBar.SlideValue(_coolDownTime);
        UiHUD.Instance.CooldownProgressBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling && !_isCoolingDown)
        {
            if (_movement.InputX != 0f || _movement.InputY != 0f)
            {
                _rollDirection = new Vector3(_movement.InputX, 0f, _movement.InputY).normalized;
                _rollingSpeed = _rollSpeed;
                _characterCotroller.enabled = false;
                _isRolling = true;
                _movement.Animator.SetBool("isRolling", _isRolling);
                _isCoolingDown = true;
            }
        }

        Roll();
    }

    void Roll()
    {
        if (_isRolling)
        {
            //TODO: Filtar esse Raycast para pegar apenas paredes e inimigos
            Vector3 positionAboveTriggerColliders = new Vector3(transform.position.x, 1.3f, transform.position.z);
            if (Physics.Raycast(positionAboveTriggerColliders, _rollDirection, out RaycastHit hit, _rayCastColisionDetectionDistance))
            {
                if (hit.collider != null)
                {
                    _characterCotroller.enabled = true;
                    _isRolling = false;
                    _movement.Animator.SetBool("isRolling", _isRolling);
                    StartCoroutine(Cooldown(_coolDownTime));
                    return;
                }
            }

            transform.position += (_rollDirection * _rollDistance) * _rollingSpeed * Time.deltaTime;
            _rollingSpeed -= _rollingSpeed * 10f * Time.deltaTime;
            
            if (_rollingSpeed < 5f)
            {
                _characterCotroller.enabled = true;
                _isRolling = false;
                _movement.Animator.SetBool("isRolling", _isRolling);
                StartCoroutine(Cooldown(_coolDownTime));
            }
        }
    }

    private IEnumerator Cooldown(float time)
    {
        UiHUD.Instance.CooldownProgressBar.gameObject.SetActive(true);
        float timer = time + Time.deltaTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            UiHUD.Instance.CooldownProgressBar.SlideValue(timer);
            yield return null;
        }

        UiHUD.Instance.CooldownProgressBar.SlideValue(time);
        UiHUD.Instance.CooldownProgressBar.gameObject.SetActive(false);
        _isCoolingDown = false;
    }

}