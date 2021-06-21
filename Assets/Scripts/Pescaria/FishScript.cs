using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter; // Spawnador

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject[] Peixes; // Prefab do peixe

    public AudioSource tickSource;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Speed; // Velocidade dos peixes
    public float r_position; // Posicao do spawnador da direita
    public float l_position; // Posicao do spawnador da esquerda
    public float MinRange; // Altura minima dos peixes
    public float MaxRange; // Altura maxima dos peixes
    public float Time_Interval; // Intervalo em segundos entre um peixe e outro
    public float Time_Destruction; // Tempo em segundas ate o peixe se auto destruir
    public float MaxOscillation;
    public float MinOscillation;
    public int QuantidadePeixes;
    private bool FirstTime = true; // Bool para gerenciar o primeiro peixe spawnado

    private void Start()
    {
        QuantidadePeixes = 11;
    }

    public void Set_Dificuldade(int qual)
    {
        switch (qual)
        {
            case 0: // Muito facil
                Speed = 10;
                Time_Interval = 2;
                Time_Destruction = 30;
                QuantidadePeixes = 11;
                PescariaScript.peixinhosObjetivo = 2;
                break;
            case 1: // Facil
                Speed = 40;
                Time_Interval = 1.5f;
                Time_Destruction = 20;
                QuantidadePeixes = 11;
                PescariaScript.peixinhosObjetivo = 2;
                break;
            case 2: // Regular
                Speed = 70;
                Time_Interval = 0.8f;
                Time_Destruction = 20;
                QuantidadePeixes = 11;
                PescariaScript.peixinhosObjetivo = 3;
                break;
            case 3: // Dificil
                Speed = 75;
                Time_Interval = 0.7f;
                Time_Destruction = 15;
                QuantidadePeixes = 11;
                PescariaScript.peixinhosObjetivo = 4;
                break;
            case 4: // Muito dificil
                Speed = 150;
                Time_Interval = 0.5f;
                Time_Destruction = 15;
                QuantidadePeixes = 11;
                PescariaScript.peixinhosObjetivo = 5;
                break;
        }
        IniciarJogo();
    }

    public void OpenCustomMode() // seta tudo para 1 ao abrir a janela do personalizado
    {
        SetVelocidade(1);
        SetQtdPeixes(1);
        SetIntervalo(1);
    }

    public void SetVelocidade(float valor)
    {
        tickSource.Play();
        switch ((int)(valor))
        {
            case 1:
                Speed = 5;
                break;
            case 2:
                Speed = 30;
                break;
            case 3:
                Speed = 50;
                break;
            case 4:
                Speed = 70;
                break;
            case 5:
                Speed = 100;
                break;
            case 6:
                Speed = 150;
                break;
            case 7:
                Speed = 250;
                break;
            case 8:
                Speed = 400;
                break;
            case 9:
                Speed = 600;
                break;
            case 10:
                Speed = 800;
                break;
        }
    }

    public void SetQtdPeixes(float valor)
    {
        tickSource.Play();
        PescariaScript.peixinhosObjetivo = (short)valor;
    }

    public void SetIntervalo(float valor)
    {
        tickSource.Play();
        switch ((int)(valor))
        {
            case 1:
                Time_Interval = 3;
                break;
            case 2:
                Time_Interval = 2.5f;
                break;
            case 3:
                Time_Interval = 2;
                break;
            case 4:
                Time_Interval = 1.5f;
                break;
            case 5:
                Time_Interval = 1;
                break;
            case 6:
                Time_Interval = 0.8f;
                break;
            case 7:
                Time_Interval = 0.6f;
                break;
            case 8:
                Time_Interval = 0.4f;
                break;
            case 9:
                Time_Interval = 0.2f;
                break;
            case 10:
                Time_Interval = 0.1f;
                break;
        }
    }

    public void IniciarJogo()
    {
        StartCoroutine(Spawnar_Function(r_position, Speed * -1, 0)); // Inicia o spawn dos peixes | Esse zero eh do indicador, representa um peixe da direita para a esquerda
    }

    IEnumerator Spawnar_Function(float position, float Velocity, int Indicator)
    {
        GameObject Temporary_Bullet_Handler; // Clone do peixe | The Bullet instantiation happens here.

        int NumPeixe = Random.Range(0, (QuantidadePeixes+1)); // Randomiza um peixe dentro do valor estimado
        if (NumPeixe == 11) NumPeixe = 0; // Caso seja sorteado o 12, sera o peixe objetivo, 0. Isso aumenta a probabilidade da vinda dele em 2x

        Temporary_Bullet_Handler = Instantiate(Peixes[NumPeixe], new Vector3(position, Random.Range(MinRange, MaxRange), 0), Bullet_Emitter.transform.rotation) as GameObject; // Instancia o prefab nas configuracoes pre-configuradas
        Temporary_Bullet_Handler.layer = Bullet_Emitter.layer; // Poe o peixe na mesma camada que o spawnador
        Temporary_Bullet_Handler.transform.position = new Vector3 (Temporary_Bullet_Handler.transform.position.x, Temporary_Bullet_Handler.transform.position.y, Random.Range(-20.0f,-5.0f));

        if (FirstTime) // Confere se eh o priemiro peixe a ser spawnado
        {
            Temporary_Bullet_Handler.transform.position = new Vector3(-2000,2000); // Spawna o primeiro peixe do jogo para longe
        }

        if (Indicator == 1) // Indicador zero eh um peixe da direita para a esquerda, gira a imagem dele
            Temporary_Bullet_Handler.GetComponent<Transform>().localScale *= new Vector2(-1,1);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody2D Temporary_RigidBody; // Variavel para armazenar o rigidbody do clone do peixe
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody2D>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.AddForce(transform.right * Velocity * Random.Range(MinOscillation, MaxOscillation), ForceMode2D.Force);
        StartCoroutine(SpeedFish(Temporary_Bullet_Handler, Temporary_RigidBody, Velocity));

        FirstTime = false;

        //Basic Clean Up, set the Bullets to self destruct after x Seconds
        //Destroy(Temporary_Bullet_Handler, Time_Destruction);
        yield return new WaitForSeconds(Time_Interval);
        if (Indicator == 0) // Aqui inicia-se novamente a funcao de spawn porem com o indicator contrario para revezar a direcao da vinda dos peixes
            StartCoroutine(Spawnar_Function(l_position, Velocity*-1, 1)); // A velocidade eh multiplicada por -1 para que ele va na direcao contraria
        else if (Indicator == 1)
            StartCoroutine(Spawnar_Function(r_position, Velocity*-1, 0));
    }

    IEnumerator SpeedFish(GameObject peixeGameobj, Rigidbody2D peixe, float Velocity)
    {
        float tempo = Random.Range(1, 6);
        yield return new WaitForSeconds(tempo);
        if (peixe == null) yield break;
        if (tempo % 2 == 0 && peixe.velocity.x < Velocity)
            peixe.AddForce(transform.right * Velocity * Random.Range(MinOscillation, MaxOscillation), ForceMode2D.Force);
        else if (peixe.velocity.x > Velocity)
            peixe.AddForce(transform.right * -Velocity/2 * Random.Range(MinOscillation, MinOscillation/2), ForceMode2D.Force);
        if(peixe.velocity.x < 0)
            peixe.AddForce(transform.right * Velocity * Random.Range(MinOscillation, MaxOscillation), ForceMode2D.Force);
        if (peixe.transform.position.x >= 15 || peixe.transform.position.x <= -15)
        {
            print("Destruindo peixe");
            Destroy(peixeGameobj);
            yield return null;
        } else
            StartCoroutine(SpeedFish(peixeGameobj, peixe, Velocity));
    }

}
