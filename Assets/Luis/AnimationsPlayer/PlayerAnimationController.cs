using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en el GameObject. Por favor, agrega un componente Animator.");
        }
    }

    // Método para actualizar el estado de movimiento
    public void SetMovementDirection(bool isMoving, bool isMovingForward, bool isMovingBackward)
    {
        if (animator != null)
        {
            // Si está moviéndose, determinar la dirección
            if (isMoving)
            {
                if (isMovingForward)
                {
                    // Si se mueve hacia adelante, se activa la animación de caminar hacia adelante
                    animator.SetBool("isMovingForward", true);
                    animator.SetBool("isMovingBackward", false);
                }
                else if (isMovingBackward)
                {
                    // Si se mueve hacia atrás, se activa la animación de caminar hacia atrás
                    animator.SetBool("isMovingBackward", true);
                    animator.SetBool("isMovingForward", false);
                }
            }
            else
            {
                // Si no se mueve, se mantiene en idle
                animator.SetBool("isMovingForward", false);
                animator.SetBool("isMovingBackward", false);
            }
        }

        // Debug para verificar la dirección del movimiento
    }
}
