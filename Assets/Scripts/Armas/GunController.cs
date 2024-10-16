using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField]
    private ArmaDisparoAlternado ArmaDisparoAlternado;

    private ArmaBasica _armaAtual;

    void Awake();
    public void EquipAlterGun()
    {
        this.ArmaAtual = this.ArmaDisparoAlternado;
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
