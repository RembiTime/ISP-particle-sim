using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    public SettingsBox settings;

    // Start is called before the first frame update
    void Start()
    {   
        initialSpawnParticles();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("Particle");
        if (particles.Length > settings.getMaxNumParticles()) {
            for (int i = 0; i < (particles.Length - settings.getMaxNumParticles()); i++) {
                int toDelete = Random.Range(0, particles.Length);
                particles[toDelete].GetComponent<ParticleBehavior>().kill(settings.getFadeInTime());
            }
        }
    }

    public void initialSpawnParticles() {
        StartCoroutine(InitialSpawnParticles(settings.getSpawnDelay()));
    }

    IEnumerator InitialSpawnParticles(float delay) {
        for (int i = 0; i < settings.getNumParticles(); i++) {
            spawnParticle();
            yield return new WaitForSeconds(delay/1000);
        }
    }

    public void spawnParticle() {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        
        if (settings.getRandomLocations()) {
            Vector3 bottomleftBounds = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
            Vector3 topRightBounds = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
            spawnPos = new Vector3(Random.Range(bottomleftBounds.x, topRightBounds.x), Random.Range(bottomleftBounds.y, topRightBounds.y), 0);
        }
        float dir = Random.Range(0, 359);
        ParticleBehavior particle = Instantiate(settings.getParticlePrefab(), spawnPos, transform.rotation);
        particle.startFade(settings.getFadeInTime());
        particle.setDirection(dir);
    }
}
