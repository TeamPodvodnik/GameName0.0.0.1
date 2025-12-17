using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndController : MonoBehaviour
{
    [SerializeField] private float _delayToMenu = 10f;
    private float _timer = 0f;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _delayToMenu)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}