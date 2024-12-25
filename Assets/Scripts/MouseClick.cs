using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public SettingsBox settings;

    public enum ClickBehavior {Nothing, Spawn, Repel, Attract};

    private bool draggingMouse = false;
    private bool attracting = false;
    private float lastSpawnTime;
    private Vector3 mouseLastScreenPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {

            if ((ClickBehavior)settings.getClickBehavior() == ClickBehavior.Spawn && !settings.getPaused()) {

                if (!draggingMouse) {
                    draggingMouse = true;
                    lastSpawnTime = Time.time * 1000;

                    mouseLastScreenPos = Input.mousePosition;

                    Vector3 mouseScreenLoc = Input.mousePosition;
                    Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mouseScreenLoc);
                    
                    int dir = Random.Range(0, 359);
                    ParticleBehavior particle = Instantiate(settings.getParticlePrefab(), new Vector3(mouseLocation.x, mouseLocation.y, 0), transform.rotation);
                    particle.startFade(settings.getFadeInTime());
                    particle.setDirection(dir);
                    
                } else if (lastSpawnTime + settings.getMouseDragSpawnDelay() <= Time.time * 1000) {
                    lastSpawnTime = Time.time * 1000;

                    Vector3 mouseScreenLoc = Input.mousePosition;
                    Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mouseScreenLoc);
                    Vector3 lastMousePos = Camera.main.ScreenToWorldPoint(mouseLastScreenPos);

                    float dir = Mathf.Atan((mouseLocation.y - lastMousePos.y) / (mouseLocation.x - lastMousePos.x)) * (180/Mathf.PI);
                    if (float.IsNaN(dir)) { // If the mouse is still
                        dir = Random.Range(0, 359);
                    } else if (mouseLocation.x - lastMousePos.x < 0) {
                        dir += 180;
                    }

                    ParticleBehavior particle = Instantiate(settings.getParticlePrefab(), new Vector3(mouseLocation.x, mouseLocation.y, 0), transform.rotation);
                    particle.startFade(settings.getFadeInTime());
                    particle.setDirection(dir);

                    mouseLastScreenPos = Input.mousePosition;
                }



            } else if ((ClickBehavior)settings.getClickBehavior() == ClickBehavior.Repel && !settings.getPaused()) {
                Vector3 mouseScreenLoc = Input.mousePosition;
                Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mouseScreenLoc);

                Collider2D[] particlesInRange = Physics2D.OverlapCircleAll(new Vector2(mouseLocation.x, mouseLocation.y), settings.getAttractRepelRange());
                foreach (Collider2D particleCollider in particlesInRange) {
                    if (particleCollider.gameObject.tag == "Untagged") {
                        continue;
                    }

                    GameObject particle = particleCollider.gameObject;
                    Vector3 particlePos = particle.transform.position;
                    Rigidbody2D particleRB = particle.GetComponent<Rigidbody2D>();

                    float distanceAway = Mathf.Sqrt(Mathf.Pow(mouseLocation.x-particlePos.x, 2) + Mathf.Pow(mouseLocation.y-particlePos.y, 2));
                    float distanceForceMultiplier = 1 - (distanceAway / (settings.getAttractRepelRange() + (particle.transform.localScale.x/2)));
                    float forceMultiplier = distanceForceMultiplier * settings.getAttractRepelStrength();

                    float direction = Mathf.Atan((particlePos.y-mouseLocation.y)/(particlePos.x-mouseLocation.x));

                    if (particlePos.x - mouseLocation.x < 0) {
                        direction += Mathf.PI;
                    }

                    particleRB.AddForce(new Vector2(Mathf.Cos(direction) * forceMultiplier, Mathf.Sin(direction) * forceMultiplier));
                }



            } else if ((ClickBehavior)settings.getClickBehavior() == ClickBehavior.Attract && !settings.getPaused()) {
                attracting = true;
                Vector3 mouseScreenLoc = Input.mousePosition;
                Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mouseScreenLoc);

                Collider2D[] particlesInRange = Physics2D.OverlapCircleAll(new Vector2(mouseLocation.x, mouseLocation.y), settings.getAttractRepelRange());
                foreach (Collider2D particleCollider in particlesInRange) {
                    if (particleCollider.gameObject.tag == "Untagged") {
                        continue;
                    }

                    GameObject particle = particleCollider.gameObject;
                    Vector3 particlePos = particle.transform.position;
                    Rigidbody2D particleRB = particle.GetComponent<Rigidbody2D>();

                    float distanceAway = Mathf.Sqrt(Mathf.Pow(mouseLocation.x-particlePos.x, 2) + Mathf.Pow(mouseLocation.y-particlePos.y, 2));
                    float distanceForceMultiplier = 1 - (distanceAway / (settings.getAttractRepelRange() + (particle.transform.localScale.x/2)));
                    float forceMultiplier = distanceForceMultiplier * settings.getAttractRepelStrength();

                    float direction = Mathf.Atan((particlePos.y-mouseLocation.y)/(particlePos.x-mouseLocation.x));

                    if (particlePos.x - mouseLocation.x < 0) {
                        direction += Mathf.PI;
                    }

                    particleRB.AddForce(new Vector2(-1 * Mathf.Cos(direction) * forceMultiplier, -1 * Mathf.Sin(direction) * forceMultiplier));
                }
            }

        } else {
            draggingMouse = false;

            if (attracting) {
                attracting = false;
                Vector3 mouseScreenLoc = Input.mousePosition;
                Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mouseScreenLoc);

                Collider2D[] particlesInRange = Physics2D.OverlapCircleAll(new Vector2(mouseLocation.x, mouseLocation.y), settings.getAttractRepelRange());

                foreach (Collider2D particleCollider in particlesInRange) {
                    if (particleCollider.gameObject.tag == "Untagged") {
                        continue;
                    }
                    float dir = Random.Range(0, 359);
                    Rigidbody2D particleRB = particleCollider.gameObject.GetComponent<Rigidbody2D>();
                    particleRB.AddForce(new Vector2(Mathf.Cos(dir) * settings.getAttractReleaseBurstStrength(), Mathf.Sin(dir) * settings.getAttractReleaseBurstStrength()));
                }
            }
        }
    }
}
