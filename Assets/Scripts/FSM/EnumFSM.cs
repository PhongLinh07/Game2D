using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player
public enum PlayerMovementStateID
{
    //Move
    None,
    Idle,
    Move,
    Dash
}

public enum PlayerMovementTrigger
{
    None,
    MoveInput,
    NoInput,
    DashPressed,
    DashFinished
}


public enum PlayerCombatStateID
{
    None,
    Idle,
    Attack,
    AttackCombo,
    SkillCast
}

public enum PlayerCombatTrigger
{
    AttackPressed,
    ComboContinued,
    SkillActivated,
    AttackFinished
}


//Enemy
public enum EnemyMovementStateID
{
    None,
    Any,
    Idle,
    Chase,
}

public enum EnemyMovementTrigger
{
    None,
    TargetSpotted,
    TargetLost,
}

public enum EnemyCombatStateID
{
    None,
    BaseAttack
}

public enum EnemyCombatTrigger
{
    InRange,
    OutOfRange
}
