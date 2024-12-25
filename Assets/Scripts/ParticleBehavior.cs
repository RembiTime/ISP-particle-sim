using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    private TrailRenderer trail;
    public float direction;
    public string settingsObject;
    private SettingsBox settings;
    private float speed;
    private float size;
    public Color color;
    
    private float startTime;
    private bool fadingIn;
    private bool fadingOut;
    private float fadeTime;
    private float bounceOffsetAmount = 0.01f;
    
    private int colorIndex = 0;
    private float colorLerpTime = 0;
    private float timeUntilDeath = -1222;

    public enum ScreenEdgeBehavior {Loop, Bounce, Destroy};

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Particle";
        startTime = Time.time * 1000;
        settings = GameObject.Find(settingsObject).GetComponent<SettingsBox>();
        trail = GetComponent<TrailRenderer>();

        speed = Random.Range(-1 * settings.getSpeedRange(), settings.getSpeedRange()) + settings.getSpeed();
        size = settings.getSize();

        if (settings.getStartOnDifferentColors()) {
            colorIndex = Random.Range(0, settings.getParticleColors().Length);
            color = settings.getParticleColors()[colorIndex];
        } else {
            color = settings.getCurrentColor();
        }
        
        color.a = 0f;
        sprite.color = color;

        transform.localScale = new Vector3(size, size, size);
        if (rb.velocity.magnitude == 0) {
            rb.velocity = new Vector2(Mathf.Cos(direction * Mathf.PI / 180) * speed, Mathf.Sin(direction * Mathf.PI / 180) * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        size = settings.getSize();
        transform.localScale = new Vector3(size, size, size);
        trail.startWidth = size;
        
        if (settings.getStartOnDifferentColors()) {
            color = Color.Lerp(sprite.color, settings.getParticleColors()[colorIndex], settings.getColorSwitchTime() * Time.deltaTime);
            colorLerpTime = Mathf.Lerp(colorLerpTime, 1f, settings.getColorSwitchTime() * Time.deltaTime);
            if (colorLerpTime > 0.9f) {
                colorLerpTime = 0f;
                colorIndex++;
                colorIndex = (colorIndex >= settings.getParticleColors().Length) ? 0 : colorIndex;
            }
        } else {
            color = settings.getCurrentColor();
        }
        
        color.a = sprite.color.a;

        if (fadingIn && sprite.color.a + Time.deltaTime / fadeTime >= 1) {
            fadingIn = false;
            color = new Color(color.r, color.g, color.b, 1f);
        } else if (fadingIn) {
            color = new Color(color.r, color.g, color.b, color.a + (Time.deltaTime * 1000) / fadeTime);
        } 

        if (fadingOut && sprite.color.a - Time.deltaTime / fadeTime <= 0) {
            Destroy(gameObject);
        } else if (fadingOut) {
            color = new Color(color.r, color.g, color.b, color.a - (Time.deltaTime * 1000) / fadeTime);
        }

        trail.startColor = color;
        trail.endColor = new Color(color.r, color.g, color.b, 0);

        if (settings.getTrailLength() == 0) {
            trail.enabled = false;
        } else {
            trail.enabled = true;
            trail.time = settings.getTrailLength();
        }
        
        /*if (!GetComponent<Renderer>().isVisible && !fadingIn && gameObject.tag.Equals("Particle")) { // If the object is off screen
            //trail.time = 0;
            loopTime = Time.time;
            gameObject.tag = "Untagged";
            if ((ScreenEdgeBehavior)settings.getScreenEdgeBehavior() == ScreenEdgeBehavior.Loop) { // and the object loops the screen
                Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
                Vector3 bottomleftBounds = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
                Vector3 topRightBounds = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

                if (screenPos.x < 0) { // flip it to the other side of the screen
                    Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(topRightBounds.x + size/2, transform.position.y, transform.position.z), gameObject.transform.rotation).startFade(0);
                    //transform.position = new Vector3(topRightBounds.x + size/2, transform.position.y, transform.position.z);
                } else if (screenPos.x > 1) {
                    Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(bottomleftBounds.x - size/2, transform.position.y, transform.position.z), gameObject.transform.rotation).startFade(0);
                    //transform.position = new Vector3(bottomleftBounds.x - size/2, transform.position.y, transform.position.z);
                }

                if (screenPos.y < 0) {
                    Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(transform.position.x, topRightBounds.y + size/2, transform.position.z), gameObject.transform.rotation).startFade(0);
                    //transform.position = new Vector3(transform.position.x, topRightBounds.y + size/2, transform.position.z);
                } else if (screenPos.y > 1) {
                    Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(transform.position.x, bottomleftBounds.y - size/2, transform.position.z), gameObject.transform.rotation).startFade(0);
                    //transform.position = new Vector3(transform.position.x, bottomleftBounds.y - size/2, transform.position.z);
                }

                timeUntilDeath = settings.getTrailLength() * 1.5f;
            } else if ((ScreenEdgeBehavior)settings.getScreenEdgeBehavior() == ScreenEdgeBehavior.Destroy) { // otherwise, kill it
                Destroy(gameObject);
            }
        }*/

        Vector3 screenMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        Vector3 screenMin = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector3 pos = gameObject.transform.position;
        float radius = settings.getSize() / 2;

        if ((ScreenEdgeBehavior)settings.getScreenEdgeBehavior() == ScreenEdgeBehavior.Bounce) {

            if (pos.x + radius > screenMax.x) {
                rb.velocity = new Vector2(-1 * rb.velocity.x, rb.velocity.y);
                gameObject.transform.position = new Vector3(screenMax.x - radius - bounceOffsetAmount, pos.y, pos.z);
            }
            else if (pos.x - radius < screenMin.x) {
                rb.velocity = new Vector2(-1 * rb.velocity.x, rb.velocity.y);
                gameObject.transform.position = new Vector3(screenMin.x + radius + bounceOffsetAmount, pos.y, pos.z);
            }
            if (pos.y + radius > screenMax.y) {
                rb.velocity = new Vector2(rb.velocity.x, -1 * rb.velocity.y);
                gameObject.transform.position = new Vector3(pos.x, screenMax.y - radius - bounceOffsetAmount, pos.z);
            }
            else if (pos.y - radius < screenMin.y) {
                rb.velocity = new Vector2(rb.velocity.x, -1 * rb.velocity.y);
                gameObject.transform.position = new Vector3(pos.x, screenMin.y + radius + bounceOffsetAmount, pos.z);
            }
        } else if ((ScreenEdgeBehavior)settings.getScreenEdgeBehavior() == ScreenEdgeBehavior.Loop && gameObject.tag.Equals("Particle")) {

            if (pos.x - radius > screenMax.x) {
                ParticleBehavior particle = Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(screenMin.x - size/2, transform.position.y, transform.position.z), gameObject.transform.rotation);
                particle.startFade(0);
                particle.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                timeUntilDeath = settings.getTrailLength() * 1.5f;
                gameObject.tag = "Untagged";
            }
            else if (pos.x + radius < screenMin.x) {
                ParticleBehavior particle = Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(screenMax.x + size/2, transform.position.y, transform.position.z), gameObject.transform.rotation);
                particle.startFade(0);
                particle.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                timeUntilDeath = settings.getTrailLength() * 1.5f;
                gameObject.tag = "Untagged";
            }
            if (pos.y - radius > screenMax.y) {
                ParticleBehavior particle = Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(transform.position.x, screenMin.y - size/2, transform.position.z), gameObject.transform.rotation);
                particle.startFade(0);
                particle.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                timeUntilDeath = settings.getTrailLength() * 1.5f;
                gameObject.tag = "Untagged";
            }
            else if (pos.y + radius < screenMin.y) {
                ParticleBehavior particle = Instantiate(gameObject.GetComponent<ParticleBehavior>(), new Vector3(transform.position.x, screenMax.y + size/2, transform.position.z), gameObject.transform.rotation);
                particle.startFade(0);
                particle.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                timeUntilDeath = settings.getTrailLength() * 1.5f;
                gameObject.tag = "Untagged";
            }
        } else if ((ScreenEdgeBehavior)settings.getScreenEdgeBehavior() == ScreenEdgeBehavior.Destroy && gameObject.tag.Equals("Particle")) {
            if (pos.x - radius > screenMax.x || pos.x + radius < screenMin.x || pos.y - radius > screenMax.y || pos.y + radius < screenMin.y) {
                timeUntilDeath = settings.getTrailLength() * 1.5f;
                gameObject.tag = "Untagged";

                Vector3 spawnPos = new Vector3(0, 0, 0);
                Vector3 bottomleftBounds = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
                Vector3 topRightBounds = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
                spawnPos = new Vector3(Random.Range(bottomleftBounds.x, topRightBounds.x), Random.Range(bottomleftBounds.y, topRightBounds.y), 0);

                float dir = Random.Range(0, 359);
                ParticleBehavior particle = Instantiate(settings.getParticlePrefab(), spawnPos, transform.rotation);
                particle.startFade(settings.getFadeInTime());
                particle.setDirection(dir);
            }
        
        }

        if (rb.velocity.magnitude > speed) {
            rb.velocity = rb.velocity * settings.getSlowdownPercent();
        } else if (rb.velocity.magnitude < speed) {
            rb.velocity = rb.velocity * (2 - settings.getSlowdownPercent());
        }

        timeUntilDeath -= Time.deltaTime;
        if (timeUntilDeath > -1222 && timeUntilDeath <= 0) {
            Destroy(gameObject);
        }

        sprite.color = color;
    }

    public void startFade(float fadeTime) {
        color = new Color(color.r, color.g, color.b, 0f);
        this.fadingIn = true;
        this.fadeTime = fadeTime;
        
        sprite.color = color;
    }

    public void kill(float fadeTime) {
        gameObject.tag = "Untagged";
        fadingOut = true;
        fadingIn = false;
        this.fadeTime = fadeTime;
    }

    public void setDirection(float direction) {
        this.direction = direction;
        rb.velocity = new Vector2(Mathf.Cos(direction * Mathf.PI / 180) * speed, Mathf.Sin(direction * Mathf.PI / 180) * speed);
    }

    public void setSpeed(float speed) {
        this.speed = speed;
        rb.velocity = new Vector2(Mathf.Cos(direction * Mathf.PI / 180) * speed, Mathf.Sin(direction * Mathf.PI / 180) * speed);
    }

    public void setSize(float size) {
        this.size = size;
        transform.localScale = new Vector3(size, size, size);
    }

    public void setColor(Color color) {
        this.color = color;
        sprite.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (settings.getIfCollision() && (Time.time * 1000) > startTime + settings.getCollisionDelay()) {
            GameObject coll = other.gameObject;
            float angle = Mathf.Atan2(coll.transform.position.y - transform.position.y, coll.transform.position.x - transform.position.x);
            angle = angle * 180 / Mathf.PI;
            if (angle < 0) {
                angle += 360;
            }
            setDirection((direction+180) + (angle - direction));
        }
    }
}
