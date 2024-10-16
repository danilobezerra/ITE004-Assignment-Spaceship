using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControladorPontuacao
{
    private static int pontuacao;

    public static int Pontuacao
    {
        get
        {
            return pontuacao;
        }
        set
        {
            pontuacao = value;
            if (Pontuacao < 0)
            {
                Pontuacao = 0;
            }
            Debug.Log("Pontua  o atualizada para: " + Pontuacao);
        }
    }
}
