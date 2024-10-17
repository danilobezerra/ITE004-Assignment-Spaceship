using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDisparoCone : ColectPower
{
    public override PowerEffect PowerEffect
    {
        get
        {
            return new EfeitoPowerupDisparoCone();
        }
    }
}
