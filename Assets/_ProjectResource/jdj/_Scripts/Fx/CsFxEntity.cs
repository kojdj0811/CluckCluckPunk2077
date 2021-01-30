using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFxEntity : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem myParticleSystem;


    private float lifetime;

    private void Awake() {
        lifetime = myParticleSystem.main.duration;
        if(lifetime != 0.0f)
            Destroy(gameObject, lifetime);   
    }
}
