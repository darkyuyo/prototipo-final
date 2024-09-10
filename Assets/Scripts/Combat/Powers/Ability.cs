using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public AbilityBase _ability { get; set; }
    public Ability(AbilityBase pAbility)
    {
        _ability = pAbility;
    }
}
