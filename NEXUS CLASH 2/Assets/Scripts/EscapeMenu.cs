using UnityEngine;
using Fusion;

public class EscapeMenu : NetworkBehaviour
{
    void Update()
    {
        // Pr�fen, ob die Escape-Taste gedr�ckt wurde
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
