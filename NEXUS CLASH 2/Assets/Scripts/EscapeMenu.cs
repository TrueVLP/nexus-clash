using UnityEngine;
using Fusion;

public class EscapeMenu : NetworkBehaviour
{
    void Update()
    {
        // Prüfen, ob die Escape-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Spieler vom Server disconnecten
            DisconnectAndLoadMainMenu();
        }
    }

    void DisconnectAndLoadMainMenu()
    {

    }
}
