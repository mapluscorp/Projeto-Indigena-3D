using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavras_FaseScript : MonoBehaviour
{
    private void OnEnable()
    {
        int objective = this.transform.Find("Slots").childCount; // conta a qtd de slots dessa fase

        WordGameScript.objective = objective; // seta como objetivo da fase
        WordGameScript.corretos = 0;
        WordGameScript.erros = 0;

    }

}
