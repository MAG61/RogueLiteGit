using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private CharacterMovement movementManager;
    private SkillManager skillManager;
    private WeaponManager weaponManager;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();

        playerInputs.Player.Dash.performed += PlayerDash;
        playerInputs.Player.Skill.performed += PlayerSkill;
        //playerInputs.Player.Fire.performed += PlayerFire;

        weaponManager = GetComponent<WeaponManager>();
        skillManager = GetComponent<SkillManager>();
        movementManager = GetComponent<CharacterMovement>();
    }

    //private void PlayerFire(InputAction.CallbackContext context)
    //{
    //    if (context.ReadValue<float>() > 0.1f)
    //    {
    //        weaponManager.Shooting();
    //    }
    //}

    private void PlayerSkill(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            skillManager.SkillUsed();
        }
    }

    private void PlayerDash(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            movementManager.dash();
        }
    }

    public Vector2 GetPlayerInputNormalized()
    {
        return playerInputs.Player.Move.ReadValue<Vector2>();
    }
}
