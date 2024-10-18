using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraVida : MonoBehaviour
{
    public GameObject[] barrasVermelhas;
    private Spaceship nave;

    private void Start()
    {
        nave = FindObjectOfType<Spaceship>(); // Certifique-se de que a nave esteja acessível
    }
    public void ExibirVida(int vidas)
    {
        for (int i = 0; i < this.barrasVermelhas.Length; i++)
             if (i < vidas) {
                this.barrasVermelhas[i].SetActive(true);
            }
            else
            {
                this.barrasVermelhas[i].SetActive(false);
                nave.DiminuirVelocidade(2f);
            }
    }
}
