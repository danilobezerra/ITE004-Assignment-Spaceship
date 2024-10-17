using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoPowerupDisparoCone : PowerEffect
{
    public override void Aplicar(Spaceship spaceship)
    {
        spaceship.EquipConeGun();
    }
}
