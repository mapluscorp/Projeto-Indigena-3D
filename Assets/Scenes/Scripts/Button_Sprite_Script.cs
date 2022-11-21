using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Sprite_Script : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    private Button pb;
    [Header("Controladores")]
    public bool TrocarSprite;
    public bool Aumentar;
    public bool Diminuir;
    public bool SoundOver;
    public bool SoundDown;

    [Header("Variaveis")]
    public Sprite[] Sprites; // 0 = normal | 1 = over | 2 = clicked
    public int SpriteAfterClick;
    public float ValorAposDiminuir = 0.9f;
    public float ValorAposAumentar = 1.1f;

    private AudioSource source;
    public AudioClip overClip;
    public AudioClip downClip;
    public AudioSource externalSource;

    void Start()
    {
        pb = GetComponent<Button>();
        pb.transform.localScale = new Vector2(1f, 1f);

        if (externalSource != null) // caso deseja-se utilizar um audioSource externo
            source = externalSource;
        else
            source = this.GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData) // Mouse sobre o botao. Sprite over
    {
        if ((Application.platform != RuntimePlatform.Android) && TrocarSprite) // Se a plataforma nao for mobile e seja para trocar os sprites
            pb.image.sprite = Sprites[1];

        if (Aumentar) // caso seja para aumentar o icone quando a seta estiver em cima
            pb.transform.localScale = new Vector2(ValorAposAumentar, ValorAposAumentar);

        if (SoundOver && Application.platform != RuntimePlatform.Android) // caso seja para reproduzir som quando o cursor estiver em cima
        {
            source.clip = overClip;
            source.Play();
        } 
        else if (SoundOver && Application.platform != RuntimePlatform.Android)
        {
            source.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData) // Mouse saiu do botao. Sprite volta ao normal
    {
        if ((Application.platform != RuntimePlatform.Android) && TrocarSprite)
            pb.image.sprite = Sprites[0];
        pb.transform.localScale = new Vector2(1f, 1f);
    }

    public  void OnPointerDown(PointerEventData eventData) // Mouse esta sendo pressionado. Sprite de clicado
    {
        if (TrocarSprite)
            pb.image.sprite = Sprites[2];
        if (Diminuir)
            pb.transform.localScale = new Vector2(ValorAposDiminuir, ValorAposDiminuir);
        if (SoundDown && Application.platform != RuntimePlatform.Android)
        {
            source.clip = downClip;
            source.Play();
        }
        else if (SoundDown && Application.platform != RuntimePlatform.Android)
        {
            source.Play();
        }
    }
    public void OnPointerUp(PointerEventData eventData) // Soltou o mouse. Sprite volta ao normal
    {
        if (TrocarSprite)
        {
            if (Application.platform != RuntimePlatform.Android)
                pb.image.sprite = Sprites[SpriteAfterClick];
            else pb.image.sprite = Sprites[0];
            pb.transform.localScale = new Vector2(1f, 1f);
        }
    }
}
