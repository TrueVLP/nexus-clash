using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscapeMenu : NetworkBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisconnectAndLoadMainMenu();
        }
    }

    void DisconnectAndLoadMainMenu()
    {
        StartCoroutine(DisconnectAndLoadMainMenuCoroutine());
    }

    private IEnumerator DisconnectAndLoadMainMenuCoroutine()
    {
        Debug.Log("Disconnecting local player and loading main menu...");

        Runner.Disconnect(Object.Runner.LocalPlayer);
        Debug.Log("Disconnecting local player...");

        yield return new WaitForSeconds(1f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        SceneManager.LoadScene(0);
    }
}
