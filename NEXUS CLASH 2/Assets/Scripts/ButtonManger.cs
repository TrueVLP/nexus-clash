using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //Gamemode 0 = Training
    //Gamemode 1 = Kampf
    public Button button1;
    public Button button2;
    public Button button3;
    public TextMeshProUGUI ChangeModeButton;
    public Button PlayButton;
    public Image PlayButtonTexture;
    public Image IconTexture;
    public Sprite ButtonTextur1;
    public Sprite ButtonTextur2;
    public Sprite IconTexure1;
    public Sprite IconTexure2;

    // Start wird vor dem ersten Frame-Aufruf einmalig aufgerufen
    void Start()
    {
        // Deaktiviert die Buttons beim Start
        DisableSelectButtons();
        ChangeTextures();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Methode zum Deaktivieren der Buttons
    public void DisableSelectButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        ChangeModeButton.text = "CHANGE";
        ChangeTextures();
    }

    // Methode zum Aktivieren der Buttons
    public void EnableSelectButtons()
    {
        if(button1.gameObject.active == false)
        {
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            ChangeModeButton.text = "BACK";
        }
        else
        {
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            ChangeModeButton.text = "CHANGE";
        }
        
    }

    public void SetTrainingMode()
    {
        PlayerPrefs.SetInt("Gamemode", 0);
        DisableSelectButtons();
        ChangeTextures();
    }

    public void SetFightMode()
    {
        PlayerPrefs.SetInt("Gamemode", 1);
        DisableSelectButtons();
        ChangeTextures();
    }

    public void StartGame()
    {
        int gameMode = PlayerPrefs.GetInt("Gamemode", 0);
        Debug.Log("GameMode: " + gameMode);

        if (gameMode == 0)
        {
            SceneManager.LoadScene(2);
        }
        else if (gameMode == 1)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ChangeTextures()
    {
        int gameMode = PlayerPrefs.GetInt("Gamemode", 0);

        if (gameMode == 0)
        {
            IconTexture.sprite = IconTexure1;
            PlayButtonTexture.sprite = ButtonTextur1;
        }
        else if (gameMode == 1)
        {
            IconTexture.sprite = IconTexure2;
            PlayButtonTexture.sprite = ButtonTextur2;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        // Suche nach allen Lichtquellen in der Szene
        Light[] lights = GameObject.FindObjectsOfType<Light>();
        if (lights.Length == 0)
        {
            Debug.Log("Keine Lichter gefunden.");
        }
        else
        {
            foreach (Light light in lights)
            {
                light.enabled = true; // Aktiviere alle Lichtquellen
                Debug.Log("Light enabled: " + light.name);
            }
        }
    }

    void OnDestroy()
    {
        // Entferne den Event-Handler, um Mehrfachzuweisungen zu vermeiden
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OpenYouTube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UC3D6i55ssWLqNso-DOjuTZQ");
    }

    public void OpenWhatsApp()
    {
        Application.OpenURL("https://www.google.com/url?q=https%3A%2F%2Fwww.whatsapp.com%2Fchannel%2F0029Va8eVgY6rsQnhkAIaN0X&sa=D&sntz=1&usg=AOvVaw041e84BRg1ugeKFbqgIDW5");
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.google.com/url?q=https%3A%2F%2Fwww.instagram.com%2Fviennaletsplay%2F&sa=D&sntz=1&usg=AOvVaw17fyvDKsCUdJ9XvZ7d7L9C");
    }

    public void OpenDiscord()
    {
        Application.OpenURL("https://www.google.com/url?q=https%3A%2F%2Fdiscord.com%2Finvite%2F2d22jPze62&sa=D&sntz=1&usg=AOvVaw0H-8BVmubqxfKbpQC6A232");
    }

    public void OpenTikTok()
    {
        Application.OpenURL("https://www.youtube.com/redirect?event=channel_description&redir_token=QUFFLUhqa1lVU1hRUW5hOUMtMG9DMVl0TGdWdmI1b05DQXxBQ3Jtc0tua01rN1cwYmtCV216VURmMUMzWVF2cHpGVVZYbWdWRWI4c0hDSlUtZkgyZjBQYnVIQ1hBLUI3Z3ZpNFVFcnFOWjdCRFVYaG82Zkpuc2dyTUVraWNSanhpUHVRLTRFQ1pVTTBHWjh0VFN1c00zekxVVQ&q=https%3A%2F%2Fwww.tiktok.com%2F%40truevlp");
    }

    public void OpenX()
    {
        Application.OpenURL("https://www.google.com/url?q=https%3A%2F%2Ftwitter.com%2FVLP_YT&sa=D&sntz=1&usg=AOvVaw0jZngkZ5EDS5zCwWcsrTnD");
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://www.vlp-entertainment.com/");
    }
}
