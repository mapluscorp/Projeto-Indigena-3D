using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public Text titleText;
    public Text completeText;
    public void SetTitleText(string txt)
    {
        titleText.text = txt;
    }

    public void SetCompleteText(string txt)
    {
        completeText.text = txt;
    }
}
