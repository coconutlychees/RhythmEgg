using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

    public float speedMultiplier = 1;
    public SpriteRenderer renderererer;

    private PlayerController player { get { return FindObjectOfType<PlayerController>(); } }
    private int currentPosition;
    private float timer;
    private bool active = false;

    private void Update()
    {
        if (Time.timeSinceLevelLoad < 4)
        {
            return;
        }
        if (!active)
        {
            active = true;
            timer = Time.time + ((player.songBPM / 240) / speedMultiplier);
            renderererer.enabled = true;
        }
        if (Time.time >= timer)
        {
            timer = Time.time + ((player.songBPM / 240) / speedMultiplier);
            switch (player.directions[currentPosition])
            {
                case PlayerController.Direction.Down:
                    transform.Translate(0, -1.5f, 0);
                    break;
                case PlayerController.Direction.Left:
                    transform.Translate(-1.5f, 0, 0);
                    break;
                case PlayerController.Direction.Up:
                    transform.Translate(0, 1.5f, 0);
                    break;
                case PlayerController.Direction.Right:
                    transform.Translate(1.5f, 0, 0);
                    break;
            }
            currentPosition++;
            //Check if player and enemy are at same position
            if (player.currentPosition == currentPosition)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
