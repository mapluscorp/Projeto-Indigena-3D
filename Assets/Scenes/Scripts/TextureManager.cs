using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextureManager : MonoBehaviour
{
    public void CutTexture(int tam, GameObject[] pieces, string desenho)
    {
        Texture2D source = Resources.Load<Texture2D>(desenho);

        float dist = GetDist(tam);

        int offset = 0;

        for (int i = 0; i < tam; i++)
        {
            for (int j = 0; j < tam; j++)
            {
                Sprite newSprite = Sprite.Create(source, new Rect(i * dist, j * dist, dist, dist), new Vector2(1, 1));
                GameObject n = new GameObject();
                SpriteRenderer sr = n.AddComponent<SpriteRenderer>();
                sr.sprite = newSprite;
                n.transform.position = new Vector3(i * 2, j * 2, 0);

                if (offset < pieces.Length)
                {
                    pieces[offset].GetComponent<Image>().sprite = newSprite;
                    pieces[offset].GetComponent<Image>().color = Color.white;
                }
                offset++;
            }
        }
    }

    private float GetDist(int tam)
    {
        switch (tam)
        {
            case 3:
                return 341.33f;
            case 4:
                return 256.0f;
            case 5:
                return 204.8f;
            case 6:
                return 170.66f;
            default:
                Debug.LogError("Ocorreu um erro com o tamanho do grid");
                return -1;
        }
    }

}