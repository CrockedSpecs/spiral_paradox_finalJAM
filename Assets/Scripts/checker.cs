using UnityEngine;
using UnityEngine.AI;

public class NavMeshCheck : MonoBehaviour
{
    private bool isInNavMesh;

    void Update()
    {
        // Verificamos continuamente si el objeto está en el NavMesh
        isInNavMesh = IsObjectInNavMesh(transform.position);

        // Muestra el estado del objeto en el NavMesh
        //if (isInNavMesh)
        //{
        //    Debug.Log("El objeto está dentro del NavMesh.");
        //}
        //else
        //{
        //    Debug.Log("El objeto NO está dentro del NavMesh.");
        //}
    }

    // Función que devuelve si el objeto está dentro del NavMesh
    bool IsObjectInNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        // Usamos SamplePosition para verificar si la posición está sobre el NavMesh
        return NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas);
    }
}
