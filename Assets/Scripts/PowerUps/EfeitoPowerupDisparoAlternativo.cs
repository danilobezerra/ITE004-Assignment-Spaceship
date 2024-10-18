using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoPowerupDisparoAlternativo : PowerEffect
{
    public override void Aplicar(Spaceship spaceship)
    {
        spaceship.EquipAlterGun();
    }
}
