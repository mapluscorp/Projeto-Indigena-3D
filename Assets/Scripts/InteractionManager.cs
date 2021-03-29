using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour // esse script detecta itens no cenario e ativa o botao de interacao
{
    [Header("References")]
    public Image collectableItemAnimation;
    public Transform boatTargetPos;
    public BoatManager boatManager;

    [Header("Tools")]
    public GameObject machete;
    public GameObject paddle;

    [Header("GameObjects")]
    public GameObject pari;

    [Header("Buttons")]
    public Button plantInteractionBtn;
    public Button macheteBtn;
    public Button pariBtn;
    public Button fruitInteractionBtn;
    public Button boatBtn;

    [Header("Audio")]
    public AudioSource source; // audio que toca interacoes em geral
    public AudioSource footstepSource; // audio que toca o som dos passos

    private PlayerManager playerManager;
    private Animator anim;
    private Transform taskGroup;
    private PlayerSoundManager soundManager;

    [SerializeField] private Identifier identifier; // identifier do objeto que esta em trigger no momento
    private AudioClip increaseSound;

    private void Start()
    {
        playerManager = this.GetComponentInParent<PlayerManager>();
        soundManager = this.GetComponent<PlayerSoundManager>();
        anim = this.GetComponentInChildren<Animator>();
        taskGroup = GameObject.Find("/Canvas/Task System/Task Group").transform;
        increaseSound = Resources.Load<AudioClip>("Audio/Pop");
    }

    #region Trigger Management

    private void OnTriggerEnter(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>(); // identificador do objeto
        if(identifier == null) { return; }

        if (identifier.name == "Boat" && !anim.GetBool("OnBoat") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PickingUp"))
        {
            boatBtn.gameObject.SetActive(true);
        }

        if (CheckForTaskExistence() == false) return; // confere se essa task esta em vigor

        if (identifier.type == "Plant") // confere se eh uma planta
        {
            plantInteractionBtn.gameObject.SetActive(true); // exibe o botao de interagir com plantas
        }
        else if (identifier.type == "Bamboo" && playerManager.CanUseMachete)
        {
            macheteBtn.gameObject.SetActive(true);
        }
        else if(identifier.type == "Pari")
        {
            pariBtn.gameObject.SetActive(true);
        }
        else if(identifier.name == "Corn")
        {
            fruitInteractionBtn.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>(); // identificador do objeto
        if (identifier == null) { other.GetComponent<Identifier>(); }
        if (identifier == null) { return; }
        if (identifier.name == "Boat" && !anim.GetBool("OnBoat") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PickingUp"))
        {
            boatBtn.gameObject.SetActive(true);
        }
        else
        {
            boatBtn.gameObject.SetActive(false);
        }

        if (CheckForTaskExistence() == false) return; // confere se essa task esta em vigor

        if (identifier.type == "Plant") // confere se eh uma planta
        {
            plantInteractionBtn.gameObject.SetActive(true); // exibe o botao de interagir com plantas
        }
        else if (identifier.type == "Bamboo" && playerManager.CanUseMachete)
        {
            macheteBtn.gameObject.SetActive(true);
        }
        else if (identifier.type == "Pari")
        {
            pariBtn.gameObject.SetActive(true);
        }
        else if (identifier.name == "Corn")
        {
            fruitInteractionBtn.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>();

        if (identifier.name == "Boat" && !anim.GetBool("OnBoat") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PickingUp"))
        {
            boatBtn.gameObject.SetActive(false);
        }

        if (identifier == null) { return; }

        if (identifier.type == "Plant")
        {
            plantInteractionBtn.gameObject.SetActive(false);
        }
        else if (identifier.type == "Bamboo" && playerManager.CanUseMachete)
        {
            macheteBtn.gameObject.SetActive(false);
        }
        else if (identifier.type == "Pari")
        {
            pariBtn.gameObject.SetActive(false);
        }
        else if (identifier.name == "Corn")
        {
            fruitInteractionBtn.gameObject.SetActive(false);
        }
        else if (identifier.name == "Harbor")
        {
            boatBtn.gameObject.SetActive(false);
        }
        identifier = null;
    }

    #endregion

    public void SetPaddleOn() // Chamado pela animacao
    {
        paddle.SetActive(true);
        boatManager.SetBoatPaddleOff();
        boatBtn.gameObject.SetActive(false);
    }

    public void Paddling()
    {
        anim.SetTrigger("PickPaddle");
        playerManager.IsGravityOn = false;
        playerManager.CanInteract = false;
        boatBtn.gameObject.SetActive(false);
        StartCoroutine(GetInBoat());
    }

    IEnumerator GetInBoat()
    {
        yield return new WaitForSeconds(5);
        this.transform.root.parent = boatManager.transform; // poe o player dentro do barco como filho*/
        anim.SetBool("OnBoat", true);
        while (Vector3.Distance(this.transform.position, boatTargetPos.position) > 0.1f)
        {
            this.transform.position = Vector3.Lerp(transform.position, boatTargetPos.position, Time.deltaTime * 4f);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, boatTargetPos.rotation, Time.deltaTime * 4f);
            yield return null;
        }
        boatManager.IsEnabled = true;
    }

    IEnumerator HoldMovement(float time) // impede o movimento por um determinado periodo de tempo
    {
        anim.SetFloat("PlayerSpeed", 0);
        playerManager.CanInteract = false;
        yield return new WaitForSeconds(time);
        playerManager.CanInteract = true;
        machete.SetActive(false);
    }

    #region Plant

    public void PullPlant() // chamado pelo botao de interacao na UI
    {
        if (!playerManager.CanInteract) { return; }
        anim.SetTrigger("PullPlant");
        StartCoroutine(HoldMovement(4.5f)); // impede o movimento do player enquanto ele a animacao de coletar esta em execucao
    }

    public void PickFruit() // chamado pelo botao de interacao na UI
    {
        if (!playerManager.CanInteract) { return; }
        anim.SetTrigger("PickFruit");
        StartCoroutine(HoldMovement(4.5f)); // impede o movimento do player enquanto ele a animacao de coletar esta em execucao
    }

    public void CollectFruit()
    {
        foreach (Transform task in taskGroup) // confere as tasks existentes
        {
            Identifier task_ID = task.GetComponent<Identifier>();
            if (task_ID.name.Contains(identifier.name))
            {
                TaskSlot taskSlot = task.GetComponent<TaskSlot>();
                if (taskSlot.isCompleted) { return; } // ja coletou tudo que precisava
                taskSlot.IncrementProgress(1); // incrementa o progresso da task
                collectableItemAnimation.transform.position = new Vector2(Screen.width / 2, Screen.height / 2); // posicao do jogador
                collectableItemAnimation.sprite = taskSlot.icon.sprite; // icone da planta sendo coletada
                collectableItemAnimation.gameObject.SetActive(true); // exibe
                StartCoroutine(CollectableAnimator(taskSlot.transform)); // percorre o trajeto ate a janelinha de tasks
            }
            if (task_ID.name == "Bamboo" || task_ID.name == "Pari") { return; } // para que nao desative o bamboo
        }
        soundManager.PlayPlantSound();
        Destroy(identifier.gameObject); // some com a planta que foi coletada
        plantInteractionBtn.gameObject.SetActive(false);
    }

    private bool CheckForTaskExistence()
    {
        foreach (Transform task in taskGroup) // confere as tasks existentes
        {
            Identifier task_ID = task.GetComponent<Identifier>(); // pega a referencia da task
            if (task_ID.name.Contains(identifier.name) && !task_ID.GetComponent<TaskSlot>().isCompleted)
            {
                return true; // task existe
            }
        }
        return false; // task nao existe
    }

    public void CollectPlant() // chamado pela animacao de coletar planta
    {
        foreach(Transform task in taskGroup) // confere as tasks existentes
        {
            Identifier task_ID = task.GetComponent<Identifier>();
            if (task_ID.name.Contains(identifier.name))
            {
                TaskSlot taskSlot = task.GetComponent<TaskSlot>();
                if(taskSlot.isCompleted) { return; } // ja coletou tudo que precisava
                taskSlot.IncrementProgress(1); // incrementa o progresso da task
                collectableItemAnimation.transform.position = new Vector2(Screen.width / 2, Screen.height / 2); // posicao do jogador
                collectableItemAnimation.sprite = taskSlot.icon.sprite; // icone da planta sendo coletada
                collectableItemAnimation.gameObject.SetActive(true); // exibe
                StartCoroutine(CollectableAnimator(taskSlot.transform)); // percorre o trajeto ate a janelinha de tasks
            }
            if(task_ID.name == "Bamboo" || task_ID.name == "Pari") { return; } // para que nao desative o bamboo
        }
        soundManager.PlayPlantSound();
        Destroy(identifier.gameObject); // some com a planta que foi coletada
        plantInteractionBtn.gameObject.SetActive(false);
    }

    #endregion

    IEnumerator CollectableAnimator(Transform targetPos) // animacao do item sendo coletado
    {
        float timeElapsed = 0;
        float lerpDuration = 0.5f;

        yield return new WaitForSeconds(0.1f); // espera um pouquinho antes de comecar

        Vector2 startPos = collectableItemAnimation.transform.position;
        Vector2 endPos = targetPos.position;
        Vector2 valueToLerp;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Vector2.Lerp(startPos, endPos, timeElapsed / lerpDuration);
            collectableItemAnimation.transform.position = valueToLerp;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        source.PlayOneShot(increaseSound); // icone da planta chegou ate a barra
        collectableItemAnimation.gameObject.SetActive(false);
    }

    public void Build(string obj)
    {
        if(obj == "Pari")
        {
            StartCoroutine(BuildPari());
        }
    }

    IEnumerator BuildPari()
    {
        StartCoroutine(HoldMovement(5));
        anim.SetTrigger("PullPlant");
        yield return new WaitForSeconds(3);
        AudioClip clip = Resources.Load<AudioClip>("Audio/Magic");
        source.PlayOneShot(clip);
        pari.SetActive(true);
    }

    public void UseMachete()
    {
        if (!playerManager.CanInteract) { return; }
        anim.SetTrigger("UseMachete");
        machete.SetActive(true);
        StartCoroutine(HoldMovement(1.4f));
    }

    public void CutBamboo() // metodo chamado pela animacao
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/Machete");
        source.PlayOneShot(clip);

        Rigidbody[] rb = identifier.transform.GetComponentsInChildren<Rigidbody>();

        identifier.transform.GetChild(1).gameObject.SetActive(false); // trigger do bamboo
        StartCoroutine(WaitToDestroy(identifier.gameObject, 2)); // retira os destrocos do bamboo do chao
        macheteBtn.gameObject.SetActive(false); // esconde o botao

        foreach (Rigidbody r in rb)
        {
            r.isKinematic = false;
        }

        CollectPlant();
    }

    IEnumerator WaitToDestroy(GameObject obj, float time) // destroi um objeto apos certo periodo de tempo
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

}
