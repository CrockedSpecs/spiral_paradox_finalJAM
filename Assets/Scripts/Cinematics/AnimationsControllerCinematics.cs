using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsControllerCinematics : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public string targetAnimationTrigger = "NextAnimation"; // Nombre del Trigger en el Animator
    public float animationDelay = 5f; // Tiempo en segundos antes de cambiar la animaci�n

    public GameObject vfxObject; // Referencia al objeto del VFX
    public float vfxDelay = 3f; // Tiempo en segundos antes de activar el VFX

    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("No se asign� un Animator.");
        }

        if (vfxObject != null)
        {
            vfxObject.SetActive(false); // Aseg�rate de que el VFX est� desactivado al inicio
        }

        // Invoca los m�todos para cambiar la animaci�n y activar el VFX
        Invoke("ChangeAnimation", animationDelay);
        Invoke("ActivateVFX", vfxDelay);
    }

    private void ChangeAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(targetAnimationTrigger); // Activa el Trigger de animaci�n
        }
    }

    private void ActivateVFX()
    {
        if (vfxObject != null)
        {
            vfxObject.SetActive(true); // Activa el objeto del VFX
        }
    }
}