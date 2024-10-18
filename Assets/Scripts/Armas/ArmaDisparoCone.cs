using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDisparoCone : ArmaBasica
{
    [SerializeField]
    private float anguloEntreDisparos;

    [SerializeField]
    private int qtdTiros;

    public override void Fire()
    {
        Vector2 positiontiro = this.gunPosition[0].position;
        for (int i = 0 ; i < qtdTiros; i++)
        {
        Projectile projectile = CriarTiro(positiontiro);
        projectile.Direcao = CalcularDirecaoTiro(i);
        }
        
    }

    private Vector2 CalcularDirecaoTiro(int indiceTiro)
    {
        int indiceTiroArco;
        if((this.qtdTiros % 2) == 0)
        {
            indiceTiroArco = indiceTiro + 1;
        }
        else
        {
            indiceTiroArco = indiceTiro;
        }

        indiceTiroArco = Mathf.CeilToInt(indiceTiroArco / 2f);

        float angulo = (this.anguloEntreDisparos * indiceTiroArco);
        if ((indiceTiro % 2) != 0)
        {
            angulo *= -1;
        }

        Quaternion rotacao = Quaternion.AngleAxis(angulo, Vector3.forward);
        Vector2 direcao = rotacao * Vector3.up;
        return direcao;
    }
}
