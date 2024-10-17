using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField]
    private ArmaDisparoAlternado ArmaDisparoAlternado;

    [SerializeField]
    private ArmaDisparoCone ArmaDisparoCone;

    private ArmaBasica _armaAtual;

    void Awake()
    {
        ArmaDisparoAlternado.Desativar();
    }

    public void EquipAlterGun()
    {
        this.ArmaAtual = this.ArmaDisparoAlternado;
    }

    public void EquipConeGun()
    {
        this.ArmaAtual = this.ArmaDisparoCone;
    }

    private ArmaBasica ArmaAtual
    {
        set
        {
            if (this._armaAtual != null)
            {
                this._armaAtual.Desativar();
            }
            this._armaAtual = value;
            this._armaAtual.Ativar();
        }
    }
}
