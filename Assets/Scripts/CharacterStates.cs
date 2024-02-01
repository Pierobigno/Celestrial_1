using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates: MonoBehaviour
{
    [Header("State")]
    public bool isDead;
    public bool isIdle,
    isDetected,
    isFacingRight,
    isStuckByEvent,
    isGrounded,
    isAbleToTalkToPNJ,
    isAbleToInteractWithEnv,
    isAbleToActivateSavePoint,
    isControlled,
    isAffectedByMuninPower,
    isEjectedToSky,
    isEjectedToGround;

    [Header("Actions")]
    public bool isWalking;
    public bool isWalkingOnWater,
    isRunning,
    isJumping, isDoubleJumping,
    isSwimming,
    isTouchingWall,
    isWallSliding,
    isFallingAfterJump,
    isDashing,
    isTouchingPuzzle,
    isSleeping,
    isWakingUp,
    isWondering,
    isLanding,
    isTouchingWater,
    isUsingMunin;
    
    [Header("Combat")]
    public bool isAttacking;
    public bool isAttacking1, isAttacking2, isAttacking3, isAttacking4, isAttacking5, isAttackingWithImpulse,
    isUsingShield,
    isDodging,
    isCountered, isCounteredOnce, isCountering,
    isBlockedByShield, isBlockedByShieldOnce,
    isCloseEnoughToAttack,
    isEnraged,
    isPreparingToBeEnraged,
    isEjected,
    isShooting,
    isLaunchingFireball,
    isTakingDamage,
    isInvulnerable,
	isHurtingHard, isHurtingSoft, isHurtingByGround,
    isAbleToAttack, isAbleToUsePowers,
    isFightingForTheFirstTime;

    [Header("UI")]
    public bool isBumping;
    public bool isInInventory;

    [Header("PNJ")]
    public bool isAbleToTalkToPlayer;
    public bool isAbleToHealThePlayer,
    isHealing,
    isHealingToMaxHealth;
}