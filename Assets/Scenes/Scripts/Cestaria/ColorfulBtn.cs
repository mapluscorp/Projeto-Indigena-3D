using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorfulBtn : MonoBehaviour
{
    public BasketManager basketManager;
    public int index;
    private Image myColor;
    public AudioSource source;

    private void Start()
    {
        myColor = this.GetComponent<Image>();
    }

    public void SetMyColor() // chamado ao clicar no quadrado
    {
        myColor.color = basketManager.GetColor();
        basketManager.SetColorOnBasket(index);
        source.Play();
    }
}
