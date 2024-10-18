using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDisparoAlternado : ColectPower
{
    public override PowerEffect PowerEffect
    {
        get
        {
            return new EfeitoPowerupDisparoAlternativo();
        }
    }
}
