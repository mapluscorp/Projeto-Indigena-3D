using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumeralButton : MonoBehaviour
{
    public NumeralManager manager;
    public bool isCorrect = false;
    private Image myImage;
    private Color normalColor;
    private Color greenColor = new Color32(0x62, 0xFF, 0x00, 0xFF);
    private Color redColor = new Color32(0xFF, 0x51, 0x4F, 0xFF);

    void Start()
    {
        myImage = this.GetComponent<Image>();
        normalColor = myImage.color;
    }

    public void OnSelect()
    {
        StartCoroutine(BlinkColor());
        manager.RegisterAnswer(isCorrect);
    }

    IEnumerator BlinkColor()
    {
        myImage.color = isCorrect ? greenColor : redColor;
        yield return new WaitForSeconds(0.1f);
        myImage.color = normalColor;
        yield return new WaitForSeconds(0.1f);
        myImage.color = isCorrect ? greenColor : redColor;

    }
}
