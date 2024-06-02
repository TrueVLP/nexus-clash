using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscapeMenu : NetworkBehaviour
{
    void Update()
    {
        // Pr�fen, ob die Escape-Taste gedr�ckt wurde
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Spieler vom Server trennen und Hauptmen� laden
            DisconnectAndLoadMainMenu();
        }
    }

    void DisconnectAndLoadMainMenu()
    {
        if (Object.HasStateAuthority)
        {
            StartCoroutine(DisconnectAndLoadMainMenuCoroutine());
        }
    }

    private IEnumerator DisconnectAndLoadMainMenuCoroutine()
    {
        Debug.Log("Disconnecting local player and loading main menu...");

        // Den lokalen Spieler vom Server trennen
        Runner.Disconnect(Object.Runner.LocalPlayer);
        Debug.Log("Disconnecting local player...");

        // Warten, um sicherzustellen, dass die Trennung abgeschlossen ist
        yield return new WaitForSeconds(1f);

        // Den lokalen Spieler entfernen
        Runner.Despawn(Object);
        Debug.Log("Removing local player from the game...");

        // NetworkRunner-GameObject entfernen
        // Erhalten Sie eine Referenz auf das GameObject
        GameObject singleObject = GameObject.Find("Single");

        // �berpr�fen Sie, ob das GameObject existiert
        if (singleObject != null)
        {
            // Zerst�ren Sie das GameObject
            Destroy(singleObject);
        }
        else
        {
            Debug.Log("GameObject 'Single' wurde nicht gefunden");
        }


        // Cursor-Modus und Sichtbarkeit einstellen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Szene 0 laden (Hauptmen�)
        SceneManager.LoadScene(0);
    }
}
