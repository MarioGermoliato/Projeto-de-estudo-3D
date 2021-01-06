using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControl : MonoBehaviour
{
    public float minWind;
    public float maxWind;

    public WindZone windZone;
    public Terrain terrain;

    private AudioSource audioSource;

    public int loop;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        windZone.windMain = minWind;
        terrain.terrainData.wavingGrassStrength = 0.1f;
        StartCoroutine("WindController");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WindController()
    {
        yield return new WaitForSeconds(Random.Range(0, 20));

        loop = Random.Range(1, 4);

        if(loop > 1 )
        {
            audioSource.loop = true;
        }
        audioSource.Play();

        windZone.windMain = maxWind;


        for(int i =0; i <= 6; i++)
        {
            terrain.terrainData.wavingGrassStrength += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < loop; i++)
        {
            print("loop vento: " + i);
            yield return new WaitForSeconds(15);
        }
        windZone.windMain = minWind;

        for (int i = 0; i<= 6; i++)
        {
            terrain.terrainData.wavingGrassStrength -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        audioSource.loop = false;
        terrain.terrainData.wavingGrassStrength = 0.1f;
        yield return new WaitForSeconds(Random.Range(20, 60));

        StartCoroutine("WindController");

    }
}
