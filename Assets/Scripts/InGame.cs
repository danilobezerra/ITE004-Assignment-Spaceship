using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{

    public Text textoPontuacao;
   

    void Update()
    {
        this.textoPontuacao.text = ControladorPontuacao.Pontuacao.ToString();
    }
}
