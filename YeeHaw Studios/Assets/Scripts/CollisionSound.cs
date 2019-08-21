using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Everytime something hits something else, send out the overlap sphere thing
    }

    void SoundAreaOfEffect()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
        
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Guard")
            {

                
                Sentry guardScript = hitColliders[i].GetComponent<Sentry>();
                guardScript.SendMessage("HeardSound");
            }
        }
    }
}
