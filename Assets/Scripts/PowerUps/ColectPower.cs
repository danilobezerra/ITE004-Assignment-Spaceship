using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColectPower : MonoBehaviour
{
    public abstract PowerEffect PowerEffect { get; }

    public void Coletar()
    {
        Destroy(this.gameObject);
    }
}
