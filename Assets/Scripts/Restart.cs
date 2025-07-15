using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Game");
    }
}
