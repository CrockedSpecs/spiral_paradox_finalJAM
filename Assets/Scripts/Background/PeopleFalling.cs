using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PeopleFalling : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> people;

    [SerializeField] private List<GameObject> pooledPeople;

    // Start is called before the first frame update
    void Start()
    {
        InstancePool(10);
        TurnOffPooledPeople();

        InvokeRepeating("SpawnPeople", 2, 2);
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
            GameObject spawnObj = Instantiate(people[SelectPeople()], transform.position, transform.rotation);
            //añadimos dicho clon a la lista
            pooledPeople.Add(spawnObj);
            spawnObj.transform.SetParent(this.transform);
        }
    }

    void TurnOffPooledPeople()
    {
        foreach (var clon in pooledPeople)
        {
            //para cada clon dentro de la lista, su estado pasara a inactivo (se apagará) y se volverá hijo del GameObject que contenga este script
            clon.SetActive(false);
        }
    }

    private void SpawnPeople()
    {
        GameObject ObjectToActivate = GetOffObject();
        ObjectToActivate.transform.position = RandomPosition();
        ObjectToActivate.transform.rotation = RandomRotation();
        GetOffObject().SetActive(true);
    }

    private GameObject GetOffObject()
    {
        for (int i = 0; i < pooledPeople.Count; i++)
        {
            //usamos una condición donde preguntamos si el clon actual NO está activado
            if (!pooledPeople[i].activeInHierarchy)
            {
                //si NO está activado, lo va a dar como resultado (lo retorna)
                return pooledPeople[i];
            }
        }

        //aquí se instancia nuevamente 1 elemento adicional (para no causar errores)
        //y retorna ese nuevo objeto sabiendo que es el último en la lista.
        InstancePool(1);
        return pooledPeople.Last<GameObject>();
    }

    private int SelectPeople()
    {
        return Random.Range(0, people.Count);
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-70, 71), Random.Range(20, 41), Random.Range(60, 80));
    }

    private Quaternion RandomRotation()
    {
        return new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }
}
