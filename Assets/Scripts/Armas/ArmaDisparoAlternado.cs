using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDisparoAlternado : ArmaBasica
{
    private Transform Nextposition;

    public override void Start()
    {
        base.Start();
        this.Nextposition = gunPosition[0];
    }

    public override void Fire()
    {
        CriarTiro(this.Nextposition.position);
        if (this.Nextposition == this.gunPosition[0])
        {
            this.Nextposition = this.gunPosition[1];
        }
        else { 
            this.Nextposition = this.gunPosition[0];
        }
    }
}
