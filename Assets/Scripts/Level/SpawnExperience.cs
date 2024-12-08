using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnExperience : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject experience;

    [SerializeField] private List<GameObject> pooledExperience;

    // Start is called before the first frame update
    void Start()
    {
        InstancePool(10);
        TurnOffPooledExperience();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstancePool(int amount)
    {
        //hacemos un ciclo for que se va a ejecutar la cantidad de veces que queremos el clon. En este caso el código dentro
        //va a ejecutarse 5 veces ya que eso definimos en el Start()
        for (int i = 0; i < amount; i++)
        {
            //creamos un GameObject temporal que almacene la instancia (clon) del prefab
            GameObject spawnObj = Instantiate(experience, transform.position, transform.rotation);
            //añadimos dicho clon a la lista
            pooledExperience.Add(spawnObj);
            spawnObj.transform.SetParent(this.transform);
        }
    }

    void TurnOffPooledExperience()
    {
        foreach (var clon in pooledExperience)
        {
            //para cada clon dentro de la lista, su estado pasara a inactivo (se apagará) y se volverá hijo del GameObject que contenga este script
            clon.SetActive(false);
        }
    }

    public void ActivateExperience(Vector3 position, Quaternion rotation)
    {
        GameObject ObjectToActivate = GetOffObject();
        ObjectToActivate.transform.position = position;
        ObjectToActivate.transform.rotation = rotation;
        GetOffObject().SetActive(true);
    }

    private GameObject GetOffObject()
    {
        for (int i = 0; i < pooledExperience.Count; i++)
        {
            //usamos una condición donde preguntamos si el clon actual NO está activado
            if (!pooledExperience[i].activeInHierarchy)
            {
                //si NO está activado, lo va a dar como resultado (lo retorna)
                return pooledExperience[i];
            }
        }

        //aquí se instancia nuevamente 1 elemento adicional (para no causar errores)
        //y retorna ese nuevo objeto sabiendo que es el último en la lista.
        InstancePool(1);
        return pooledExperience.Last<GameObject>();
    }
}
