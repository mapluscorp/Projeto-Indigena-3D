using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CompassTasksObjects : MonoBehaviour
{
    private static float REFRESH_TIME_COMPASS = 0.01f;
    private Coroutine directionCoroutine;
    private GameObject tasks;


    private void Update()
    {
        tasks = tasks ?? GameObject.FindWithTag("Task Group");

        if (directionCoroutine == null && tasks != null)
        {
            directionCoroutine = StartCoroutine(ShowDirectionCoroutine(tasks));
        }
    }

    /// <summary>
    /// Mostra o objeto de missão mais próximo do jogador
    /// </summary>
    private IEnumerator ShowDirectionCoroutine(GameObject tasks)
    {
        string taskName = "";
        for (int i = 0; i < tasks.transform.childCount; i++)
        {
            TaskSlot ts = tasks.transform.GetChild(i).gameObject.GetComponent<TaskSlot>();
            if (!ts.isCompleted)
            {
                taskName = ts.GetComponent<Identifier>().name;
            } else
            {
                StopCoroutine(directionCoroutine);
                directionCoroutine = null;
            }
        }
        switch (taskName)
        {
            case "Nightshade":
            case "Carrot":
            case "Pari":
            case "Corn":
            case "Cabbage":
                EnableCompassVisibility();
                while (true)
                {
                    var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                    var compassArrow = GameObject.FindGameObjectWithTag("compass_arrow");

                    var list = GetObjectsDirection(name: taskName, playerPosition: playerPosition);
                    var objectTarget = list.First();

                    var relativePos = objectTarget.transform.position - compassArrow.transform.position;
                    var rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z));
                    compassArrow.transform.rotation = rotation;

                    #if UNITY_EDITOR
                    Debug.DrawRay(compassArrow.transform.position, compassArrow.transform.TransformDirection(Vector3.forward) * 100, Color.red);
                    #endif

                    yield return new WaitForSeconds(REFRESH_TIME_COMPASS);
                }
                case "":
                DisableCompassVisibility();
                break;
        }
    }

    /// <summary>
    /// Captura a localização de todos os objetos da missão na cena através da tag
    /// retornando uma lista de objetos;
    /// </summary>
    /// <returns></returns>
    private List<GameObject> GetObjectsDirection(string name, Vector3 playerPosition)
    {
        var objects = GameObject.FindGameObjectsWithTag(name);


        var listPositions = objects.OrderBy(x => Vector3.Distance(x.transform.position, playerPosition)).ToList();

        //listPositions.ForEach(x => Debug.Log(Vector3.Distance(x.transform.position, playerPosition)));

        return listPositions;
    }

    /// <summary>
    /// Habilita a visualização da mesh do seta que identifica o objeto próximo da missão
    /// </summary>
    public void EnableCompassVisibility()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    /// <summary>
    /// Desabilita a visualização da mesh do seta que identifica o objeto próximo da missão
    /// </summary>
    public void DisableCompassVisibility()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StopCoroutine(directionCoroutine);
        directionCoroutine = null;
    }
}
