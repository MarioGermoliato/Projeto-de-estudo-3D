using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    public AudioSource owlSource;
    public AudioClip[] owlSongs;
    public int singChance;
    // Start is called before the first frame update
    void Start()
    {
        owlSource = GetComponent<AudioSource>();
        StartCoroutine("OwlSing");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OwlSing()
    {
        yield return new WaitForSeconds(Random.Range(10, 31));

        int i = Random.Range(1, 101);
            if(i <= singChance)
            {
            owlSource.PlayOneShot(owlSongs[Random.Range(0, owlSongs.Length)]);
            }

        StartCoroutine("OwlSing");
    }
}
