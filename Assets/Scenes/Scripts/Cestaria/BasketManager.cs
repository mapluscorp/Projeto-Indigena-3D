using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    public GameObject[] enableAtStart;
    public Material[] materials;
    public Color defaultColor;
    public Color[] colors;
    public Transform selectionSprite;
    public int selectedColor = 0;

    private void Start()
    {
        foreach(GameObject g in enableAtStart)
        {
            g.SetActive(true);
        }
        ResetMaterials();
    }

    private void ResetMaterials()
    {
        foreach(Material m in materials)
        {
            m.color = defaultColor;
        }
    }

    public void SelectColor(int value) // chamado pelo circulo que seleciona cor
    {
        selectedColor = value;
    }

    public void SetSelectionPos(Transform t) // chamado pelo circulo que seleciona cor
    {
        selectionSprite.position = t.position;
    }

    public Color GetColor() // chamado pelo quardado quando ele eh pressionado
    {
        return colors[selectedColor];
    }

    public void SetColorOnBasket(int index)
    {
        materials[index].color = colors[selectedColor];
    }

}
