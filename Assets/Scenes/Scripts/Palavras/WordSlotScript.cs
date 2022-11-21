using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSlotScript : MonoBehaviour
{
    private int offset = (int)((Screen.width * Screen.height)/30000); // seta o offset de acordo com a resolucao 
    private Image minhaBarrinha;

    private Color defaultColor = new Color32(0xFF, 0xE0, 0x05, 0xFF);
    private Color overColor = new Color32(0xFF, 0x97, 0x05, 0xFF);
    private Color greenColor = new Color32(0x58, 0xC8, 0x0D, 0xFF);

    [HideInInspector]
    public bool activated;
    [HideInInspector]
    public bool blinking;

    void Start()
    {
        minhaBarrinha = this.transform.GetChild(0).GetComponent<Image>(); // barrinha embaixo deste slot
        minhaBarrinha.color = defaultColor; // barrinha cor default
    }

    /*private void OnEnable()
    {
        minhaBarrinha = this.transform.GetChild(0).GetComponent<Image>(); // barrinha embaixo deste slot
        minhaBarrinha.color = defaultColor; // barrinha cor default
        print("Enabled: " + this.gameObject.name);
    }*/

    void Update()
    {
        if (WordGameScript.selected != null) // confere se ha alguma letra proxima a este slot
            CheckNear();
    }

    private void CheckNear() // confere se ha alguma letra proxima a este slot
    {
        if (Vector3.Distance(this.transform.position, WordGameScript.selected.transform.position) < offset && !activated) // peca esta proxima
        {
            WordGameScript.overSlot = this.gameObject; // guarda o slot que esta em over
            minhaBarrinha.color = overColor; // barrinha cor de over
        } else if (!activated && !blinking) // caso nao esteja correto e nem piscando
        {
            minhaBarrinha.color = defaultColor; // barrinha cor default
        }
        else if (activated) // caso esteja correto
        {
            minhaBarrinha.color = greenColor; // barrinha cor de correto
        }
    }

    public void ResetarBarrinha()
    {
        activated = false;
        minhaBarrinha.color = defaultColor;
    }

}
