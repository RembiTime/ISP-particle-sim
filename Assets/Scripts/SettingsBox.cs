using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBox : MonoBehaviour
{
    public enum ScreenEdgeBehavior {Loop, Bounce, Destroy};
    
    [Header("Particle Behavior")]
    public float speed;
    public float speedRange;
    public float size;
    public Color[] particleColors;
    [Range(0f, 1f)] public float colorSwitchTime;
    public bool startOnDifferentColors;
    public ScreenEdgeBehavior screenEdgeBehavior;
    public bool collision;
    public int collisionDelay;
    public int maxNumParticles;
    public float slowdownPercent;

    [Header("Spawn Settings")]
    public int numParticlesSpawned;
    public int spawnDelay;
    public ParticleBehavior particlePrefab;
    public bool randomLocations;
    public float fadeInTime;

    public enum ClickBehavior {Nothing, Spawn, Repel, Attract};
    [Header("User Interaction Settings")]
    public ClickBehavior clickBehavior;
    public float mouseDragSpawnDelay;
    public float attractRepelRange;
    public float attractRepelStrength;
    public float attractReleaseBurstStrength;

    [Header("Effects Settings")]
    public float trailLength;
    public Color[] backgroundColors;
    [Range(0f, 1f)] public float bgColorSwitchTime;
    public Camera cameraObject;

    [Header("Other")]
    public bool paused = false;
    public SpawnerBehavior spawner;

    private int particleColorIndex = 0;
    private float particleColorLerpTime = 0;
    private Color particleColor;
    
    private int backgroundColorIndex = 0;
    private float backgroundColorLerpTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        particleColor = Color.Lerp(particleColor, particleColors[particleColorIndex], colorSwitchTime * Time.deltaTime);
        particleColorLerpTime = Mathf.Lerp(particleColorLerpTime, 1f, colorSwitchTime * Time.deltaTime);
        if (particleColorLerpTime > 0.9f) {
            particleColorLerpTime = 0f;
            particleColorIndex++;
            particleColorIndex = (particleColorIndex >= particleColors.Length) ? 0 : particleColorIndex;
        }

        cameraObject.backgroundColor = Color.Lerp(cameraObject.backgroundColor, backgroundColors[backgroundColorIndex], bgColorSwitchTime * Time.deltaTime);
        backgroundColorLerpTime = Mathf.Lerp(backgroundColorLerpTime, 1f, bgColorSwitchTime * Time.deltaTime);
        if (backgroundColorLerpTime > 0.9f) {
            backgroundColorLerpTime = 0f;
            backgroundColorIndex++;
            backgroundColorIndex = (backgroundColorIndex >= backgroundColors.Length) ? 0 : backgroundColorIndex;
        }
    }

    public void massChangeSettings(float speed, float speedRange, float size, Color[] particleColors, float colorSwitchTime, bool startOnDifferentColors, ScreenEdgeBehavior screenEdgeBehavior, bool collision, int collisionDelay, int maxNumParticles, float slowdownPercent, int numParticlesSpawned, int spawnDelay, bool randomLocations, float fadeInTime, ClickBehavior clickBehavior, float mouseDragSpawnDelay, float attractRepelRange, float attractRepelStrength, float attractReleaseBurstStrength, float trailLength, Color[] backgroundColors, float bgColorSwitchTime) {
        this.speed = speed;
        this.speedRange = speedRange;
        this.size = size;
        this.particleColors = particleColors;
        this.colorSwitchTime = colorSwitchTime;
        this.startOnDifferentColors = startOnDifferentColors;
        this.screenEdgeBehavior = screenEdgeBehavior;
        this.collision = collision;
        this.collisionDelay = collisionDelay;
        this.maxNumParticles = maxNumParticles;
        this.slowdownPercent = slowdownPercent;
        this.numParticlesSpawned = numParticlesSpawned;
        this.spawnDelay = spawnDelay;
        this.randomLocations = randomLocations;
        this.fadeInTime = fadeInTime;
        this.clickBehavior = clickBehavior;
        this.mouseDragSpawnDelay = mouseDragSpawnDelay;
        this.attractRepelRange = attractRepelRange;
        this.attractRepelStrength = attractRepelStrength;
        this.attractReleaseBurstStrength = attractReleaseBurstStrength;
        this.trailLength = trailLength;
        this.backgroundColors = backgroundColors;
        this.bgColorSwitchTime = bgColorSwitchTime;

        killAllParticles();
        particleColorIndex = 0;
        backgroundColorIndex = 0;
    }
    
    public void killAllParticles() {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("Particle");
        foreach (GameObject particle in particles) {
            Destroy(particle);
        }
        spawner.initialSpawnParticles();
    }

    public void setPaused(bool paused) {
        this.paused = paused;
    }

    public bool getPaused() {
        return paused;
    }

    public Color[] getBackgroundColors() {
        return backgroundColors;
    }

    public float getBgColorSwitchTime() {
        return bgColorSwitchTime;
    }

    public float getTrailLength() {
        return trailLength;
    }

    public Color getCurrentColor() {
        return particleColor;
    }

    public bool getStartOnDifferentColors() {
        return startOnDifferentColors;
    }

    public float getColorSwitchTime() {
        return colorSwitchTime;
    }

    public float getAttractReleaseBurstStrength() {
        return attractReleaseBurstStrength;
    }

    public float getSlowdownPercent() {
        return slowdownPercent;
    }

    public float getSpeedRange() {
        return speedRange;
    }

    public float getAttractRepelStrength() {
        return attractRepelStrength;
    }

    public float getAttractRepelRange() {
        return attractRepelRange;
    }

    public int getMaxNumParticles() {
        return maxNumParticles;
    }

    public float getMouseDragSpawnDelay() {
        return mouseDragSpawnDelay;
    }

    public ClickBehavior getClickBehavior() {
        return clickBehavior;
    }

    public int getNumParticles() {
        return numParticlesSpawned;
    }

    public int getSpawnDelay() {
        return spawnDelay;
    }

    public ParticleBehavior getParticlePrefab() {
        return particlePrefab;
    }

    public bool getRandomLocations() {
        return randomLocations;
    }

    public float getFadeInTime() {
        return fadeInTime;
    }

    public float getSpeed() {
        return speed;
    }

    public float getSize() {
        return size;
    }

    public Color[] getParticleColors() {
        return particleColors;
    }

    public ScreenEdgeBehavior getScreenEdgeBehavior() {
        return screenEdgeBehavior;
    }

    public bool getIfCollision() {
        return collision;
    }

    public int getCollisionDelay() {
        return collisionDelay;
    }
}
