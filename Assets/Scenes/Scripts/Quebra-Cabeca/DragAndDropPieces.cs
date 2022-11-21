using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropPieces : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject item; //itemBeingDragged
    public static bool Acertou;
    private bool Itsok;
    private Vector2 tamOriginal;

    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        tamOriginal = GetComponent<Image>().rectTransform.sizeDelta;

        GetComponent<Image>().rectTransform.sizeDelta = new Vector2(PuzzleGameScript.tamanhoSlot, PuzzleGameScript.tamanhoSlot);

        Vector3 Mouse = Input.mousePosition;

        if (Mouse.x > PuzzleGameScript.Left_reference.position.x && Mouse.x < PuzzleGameScript.Right_reference.position.x && Mouse.y < PuzzleGameScript.Left_reference.position.y)
            Itsok = false;
        else Itsok = true;

        item = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;

        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Itsok)
            transform.position = Input.mousePosition;
        else transform.position = startPosition;
        Acertou = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        item = null;

        if (transform.parent == startParent || transform.parent == transform.root)
        {
            if (PuzzleGameScript.Check == false)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
                Acertou = false;
                GetComponent<Image>().rectTransform.sizeDelta = tamOriginal;
            }
            else
            {
                transform.position = PuzzleGameScript.SlotTransform.position;
                transform.SetParent(startParent);
                Acertou = true;
            }

        }
    }
}
