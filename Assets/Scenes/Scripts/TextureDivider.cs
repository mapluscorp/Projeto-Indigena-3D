using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureDivider : MonoBehaviour
{
    public Texture2D source;
    public Image[] pieces;

    // Use this for initialization
    void Start()
    {
        pieces = GameObject.Find("Pieces").GetComponentsInChildren<Image>();
        print("Tam: " + pieces.Length);

        GameObject spritesRoot = GameObject.Find("SpritesRoot");

        int offset = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Sprite newSprite = Sprite.Create(source, new Rect(i * 341.3f, j * 341.3f, 341.3f, 341.3f), new Vector2(1, 1));
                GameObject n = new GameObject();
                SpriteRenderer sr = n.AddComponent<SpriteRenderer>();
                sr.sprite = newSprite;
                n.transform.position = new Vector3(i * 2, j * 2, 0);
                n.transform.parent = spritesRoot.transform;

                if(offset < pieces.Length)
                {
                    pieces[offset].sprite = newSprite;
                }
                else
                {
                    print("Qtd pecas: " + offset);
                }
                offset++;
            }
        }
    }
}