using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFxEntity : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem myParticleSystem;


    public float lifetime;

    public  virtual void Awake() {
        lifetime = myParticleSystem.main.duration;
        if(lifetime != 0.0f)
            Destroy(gameObject, lifetime);   
    }
}
