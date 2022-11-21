using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("References")]
    public PlayerManager playerManager;

    [Header("Sounds")]
    public AudioSource source; // audio que toca interacoes em geral
    public AudioSource footstepSource; // audio para o som de passos

    [Header ("Clips")]
    public AudioClip grassFootSound;
    public AudioClip woodFootSound;
    public AudioClip waterPouringSound;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void PlayPlantSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/PushPlant");
        source.PlayOneShot(clip);
    }

    public void PlayFootstepSound() // chamado pela animacao de movimento
    {
        //if(footstepSource.isPlaying) { return; }

        if (playerManager.GroundTag == "Wood") { footstepSource.PlayOneShot(woodFootSound); }
        else { footstepSource.PlayOneShot(grassFootSound); }
        wait();
    }

    IEnumerator wait()
    {
        footstepSource.Stop();
        yield return new WaitForSeconds(1);
    }

    public void PlayWaterPouringSound()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).speed == 1)
        {
            source.PlayOneShot(waterPouringSound, 0.3f);
        }
    }
}
