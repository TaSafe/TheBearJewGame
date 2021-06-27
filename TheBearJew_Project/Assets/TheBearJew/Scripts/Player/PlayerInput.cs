using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool IsAllInputsEnable { get; set; } = true; //HACK: pra parar tudo temporariamente de forma direta na propriedade
    private bool inputEnabled = true;
    private bool videoInputEnabled;

    private void Update() => PlayerInputs();

    private void PlayerInputs()
    {
        //Vídeo
        if (videoInputEnabled)
        {
            if (Input.GetKey(KeyCode.Space))
                VideoController.Instance.JumpVideo();
            else
                VideoController.Instance.JumpVideoReset();

            return;
        }

        //Pausa
        if (Input.GetKeyDown(KeyCode.Escape))
            UiHUD.Instance.Pause();

        if (!IsAllInputsEnable) return;

        //Interação
        if (Player.Instance.PlayerInteraction.CurrentInteraction != null)
        {
            if (Input.GetMouseButtonDown(1) && Player.Instance.PlayerInteraction.CurrentInteraction.MyType == IInteraction.InteractionType.GUN)
                Player.Instance.PlayerInteraction.WeaponHandlerInteraction(Player.Instance.PlayerWeaponHandler);

            if (Input.GetKeyDown(KeyCode.E) && Player.Instance.PlayerInteraction.CurrentInteraction.MyType == IInteraction.InteractionType.GENERAL)
                Player.Instance.PlayerInteraction.WeaponHandlerInteraction(Player.Instance.PlayerWeaponHandler);
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
                Player.Instance.PlayerInteraction.WeaponHandlerInteraction(Player.Instance.PlayerWeaponHandler);
        }

        if (!inputEnabled) return;

        //In-game
        Player.Instance.Aim.Aiming();

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Player.Instance.Movement.Move(xInput, yInput);

        if (Input.GetMouseButton(0))
            Player.Instance.PlayerWeaponHandler.Attack(false);

        if (Input.GetMouseButtonDown(0))
            Player.Instance.PlayerWeaponHandler.Attack(true);
        
        if (Input.GetKeyDown(KeyCode.Q))
            Player.Instance.PlayerWeaponHandler.SwitchWeapons();

        if (Input.GetKeyDown(KeyCode.Space))
            Player.Instance.PlayerRoll.ActivateRoll = true;

        if (Input.GetKeyUp(KeyCode.Space))
            Player.Instance.PlayerRoll.ActivateRoll = false;
    }

    public void EnableInput() => inputEnabled = true;
    public void DisableInput() => inputEnabled = false;

    public void InputStateOnVideo(bool state) => videoInputEnabled = state;
}