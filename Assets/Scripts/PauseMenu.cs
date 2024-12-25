using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public SettingsBox settings;
    public GameObject ui;
    public GameObject copyButton;

    public enum ClickBehavior {Nothing, Spawn, Repel, Attract};
    public enum ScreenEdgeBehavior {Loop, Bounce, Destroy};

    void Start() {
        RainbowSmall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (settings.getPaused()) {
                Resume();
            } else {
                Pause();
            }
        }

        Rect dimensions = copyButton.GetComponent<RectTransform>().rect;
        copyButton.transform.position = new Vector3(Screen.width - (dimensions.width / 2), dimensions.height / 2, copyButton.transform.position.z);
    }

    public void Resume() {
        settings.setPaused(false);
        ui.SetActive(false);
    }

    public void Pause() {
        settings.setPaused(true);
        ui.SetActive(true);
    }

    public void Quit() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ChangePreset(int id) {
        switch (id) {
            case 0:
                RainbowSmall(); break;
            case 1:
                RainbowMid(); break;
            case 2:
                BirthdayCake(); break;
            case 3:
                Bonk(); break;
            case 4:
                Jellyfish(); break;
            case 5:
                ShootingStars(); break;
            case 6:
                Tadpoles(); break;
            case 7:
                QuotePerfect(); break;
        }
        Resume();
    }

    public void RainbowSmall() {
        settings.massChangeSettings(2f, 0f, 0.1f, new Color[]{new Color(1f, 0.207797f, 0f, 1f), new Color(1f, 0.6979648f, 0f, 1f), new Color(0.9302142f, 1f, 0f, 1f), new Color(0.3990967f, 1f, 0f, 1f), new Color(0f, 0.745283f, 0.1178029f, 1f), new Color(0f, 0.9728527f, 1f, 1f), new Color(0f, 0.3158722f, 1f, 1f), new Color(0.6045533f, 0.2311321f, 1f, 1f), new Color(1f, 0.4386792f, 0.9750488f, 1f)}, 1f, false, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Loop, false, 100, 1000, 0.995f, 1000, 0, true, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Attract, 0f, 5f, 5f, 700f, 1f, new Color[]{new Color(0.09545212f, 0.2014136f, 0.3679245f, 1f), new Color(0.2593519f, 0.09411764f, 0.3686275f, 1f)}, 0.5f);
    }

    public void RainbowMid() {
        settings.massChangeSettings(2f, 0f, 0.3f, new Color[]{new Color(1f, 0.207797f, 0f, 1f), new Color(1f, 0.6979648f, 0f, 1f), new Color(0.9302142f, 1f, 0f, 1f), new Color(0.3990967f, 1f, 0f, 1f), new Color(0f, 0.745283f, 0.1178029f, 1f), new Color(0f, 0.9728527f, 1f, 1f), new Color(0f, 0.3158722f, 1f, 1f), new Color(0.6045533f, 0.2311321f, 1f, 1f), new Color(1f, 0.4386792f, 0.9750488f, 1f)}, 1f, false, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Loop, false, 100, 200, 0.998f, 200, 50, false, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Attract, 0f, 100f, 10f, 1000f, 1f, new Color[]{new Color(0.09545212f, 0.2014136f, 0.3679245f, 1f), new Color(0.2593519f, 0.09411764f, 0.3686275f, 1f)}, 0.5f);
    }

    public void BirthdayCake() {
        settings.massChangeSettings(2f, 0f, 0.5f, new Color[]{new Color(1f, 0.207797f, 0f, 1f), new Color(1f, 0.6979648f, 0f, 1f), new Color(0.9302142f, 1f, 0f, 1f), new Color(0.3990967f, 1f, 0f, 1f), new Color(0f, 0.745283f, 0.1178029f, 1f), new Color(0f, 0.9728527f, 1f, 1f), new Color(0f, 0.3158722f, 1f, 1f), new Color(0.6045533f, 0.2311321f, 1f, 1f), new Color(1f, 0.4386792f, 0.9750488f, 1f)}, 1f, true, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Loop, false, 100, 200, 0.998f, 100, 50, false, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Attract, 50f, 100f, 10f, 1000f, 1f, new Color[]{new Color(0.09545212f, 0.2014136f, 0.3679245f, 1f), new Color(0.2593519f, 0.09411764f, 0.3686275f, 1f)}, 0.5f);
    }

    public void Bonk() {
        settings.massChangeSettings(2f, 0f, 0.5f, new Color[]{new Color(1f, 0.207797f, 0f, 1f), new Color(1f, 0.6979648f, 0f, 1f), new Color(0.9302142f, 1f, 0f, 1f), new Color(0.3990967f, 1f, 0f, 1f), new Color(0f, 0.745283f, 0.1178029f, 1f), new Color(0f, 0.9728527f, 1f, 1f), new Color(0f, 0.3158722f, 1f, 1f), new Color(0.6045533f, 0.2311321f, 1f, 1f), new Color(1f, 0.4386792f, 0.9750488f, 1f)}, 1f, false, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Bounce, true, 100, 150, 0.999f, 100, 50, false, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Spawn, 50f, 100f, 10f, 1000f, 0f, new Color[]{new Color(0.09545212f, 0.2014136f, 0.3679245f, 1f), new Color(0.2593519f, 0.09411764f, 0.3686275f, 1f)}, 0.5f);
    }

    public void Jellyfish() {
        settings.massChangeSettings(2f, 0f, 0.3f, new Color[]{new Color(0.5490196f, 0.3960784f, 0.827451f, 1f), new Color(0f, 0.3215686f, 0.6470588f, 1f), new Color(0f, 0.6784314f, 0.8078431f, 1f), new Color(0f, 0.772549f, 0.5647059f, 1f)}, 0.3f, false, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Loop, false, 100, 300, 0.999f, 300, 0, true, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Repel, 50f, 3f, 5f, 1000f, 1f, new Color[]{new Color(0.1037736f, 0.1037736f, 0.1037736f, 1f)}, 0.5f);
    }

    public void ShootingStars() {
        settings.massChangeSettings(3f, 2f, 0.2f, new Color[]{new Color(0.9245283f, 0.9173812f, 0.6672303f, 1f)}, 1f, false, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Destroy, false, 100, 25, 0.999f, 20, 0, true, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Spawn, 1E+08f, 3f, 10f, 1000f, 1f, new Color[]{new Color(0.1044612f, 0.09910113f, 0.1603774f, 1f)}, 0.5f);
    }

    public void Tadpoles() {
        settings.massChangeSettings(2f, 1f, 0.4f, new Color[]{new Color(0.5058518f, 0.8113208f, 0.4018334f, 1f), new Color(0.1572624f, 0.5849056f, 0.2216116f, 1f)}, 1f, true, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Loop, true, 100, 100, 0.999f, 50, 0, true, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Repel, 50f, 3f, 2f, 1000f, 0.3f, new Color[]{new Color(0.1803922f, 0.3294118f, 0.2039216f, 1f), new Color(0.09019608f, 0.2313726f, 0.3294118f, 1f)}, 0.1f);
    }

    public void QuotePerfect() {
        settings.massChangeSettings(2f, 0f, 0.2f, new Color[]{new Color(0.5490196f, 0.3960784f, 0.827451f, 1f), new Color(0f, 0.3215686f, 0.6470588f, 1f), new Color(0f, 0.6784314f, 0.8078431f, 1f), new Color(0f, 0.772549f, 0.5647059f, 1f)}, 0.3f, false, (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior.Loop, false, 100, 300, 0.998f, 300, 0, true, 1000f, (SettingsBox.ClickBehavior)ClickBehavior.Attract, 50f, 25f, 5f, 750f, 1f, new Color[]{new Color(0.1037736f, 0.1037736f, 0.1037736f, 1f)}, 0.5f);
    }

    public void CopySettings() {
        Color[] particleColors = settings.getParticleColors();
        string particleColText = "new Color[]{";

        foreach (Color col in particleColors) {
            if (!particleColText.Equals("new Color[]{")) {
                particleColText += ", ";
            }
            particleColText += "new Color(" + col.r + "f, " + col.g + "f, " + col.b + "f, " + col.a + "f)";
        }
        particleColText += "}";


        Color[] backgroundColors = settings.getBackgroundColors();
        string backgroundColText = "new Color[]{";

        foreach (Color col in backgroundColors) {
            if (!backgroundColText.Equals("new Color[]{")) {
                backgroundColText += ", ";
            }
            backgroundColText += "new Color(" + col.r + "f, " + col.g + "f, " + col.b + "f, " + col.a + "f)";
        }
        backgroundColText += "}";
        
        Color[] test3 = {new Color(10, 10, 10, 10), new Color(10, 10, 10, 10)};

        TextEditor text = new TextEditor();
        text.text = settings.getSpeed() + "f, " + settings.getSpeedRange() + "f, " + settings.getSize() + "f, " + particleColText + ", " + settings.getColorSwitchTime() + "f, " + settings.getStartOnDifferentColors().ToString().ToLower() + ", (SettingsBox.ScreenEdgeBehavior)ScreenEdgeBehavior." + settings.getScreenEdgeBehavior() + ", " + settings.getIfCollision().ToString().ToLower() + ", " + settings.getCollisionDelay() + ", " + settings.getMaxNumParticles() + ", " + settings.getSlowdownPercent() + "f, " + settings.getNumParticles() + ", " + settings.getSpawnDelay() + ", " + settings.getRandomLocations().ToString().ToLower() + ", " + settings.getFadeInTime() + "f, (SettingsBox.ClickBehavior)ClickBehavior." + settings.getClickBehavior() + ", " + settings.getMouseDragSpawnDelay() + "f, " + settings.getAttractRepelRange() + "f, " + settings.getAttractRepelStrength() + "f, " + settings.getAttractReleaseBurstStrength() + "f, " + settings.getTrailLength() + "f, " + backgroundColText + ", " + settings.getBgColorSwitchTime() + "f";
        text.SelectAll();
        text.Copy();
    }
}
