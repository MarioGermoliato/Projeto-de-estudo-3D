using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStep : MonoBehaviour
{
    public AudioClip[] woodFloor;
    public AudioClip[] grass;
    public AudioSource audioSorce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StepSound()
    {
        RaycastHit hit;
        Vector3 start = transform.position;
        Vector3 direction = Vector3.down;

        Debug.DrawRay(start, direction, Color.red, 0.5f);

        if(Physics.Raycast(start, direction, out hit, 1.3f))
        {
            if(hit.collider.name == "Terrain")
            {
                audioSorce.PlayOneShot(grass[Random.Range(0, grass.Length)]);
            }
            else
            {
                audioSorce.PlayOneShot(woodFloor[Random.Range(0, woodFloor.Length)]);
            }
        }
    }
}
