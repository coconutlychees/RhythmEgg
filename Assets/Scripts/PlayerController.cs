using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float songBPM;
    public float speedMultiplier = 1;
    public float predelay;
    public SpriteRenderer[] arrows; //0 = Left, 1 = Up, 2 = Right, 3 = Down
    public enum Direction { Left, Up, Right, Down }
    public Direction[] directions;
    [HideInInspector] public int currentPosition;

    private int activeDirection;
    private float timer;

    private void Update()
    {
        if (Time.time < (songBPM / 240) * predelay)
        {
            return;
        }
        if (Time.time >= timer)
        {
            timer = Time.time + ((songBPM / 60) / speedMultiplier);
            activeDirection = (activeDirection + 1) % 4;
            ArrowUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeDirection == (int)directions[currentPosition])
            {
                activeDirection = (activeDirection + 1) % 4;
                ArrowUpdate();
                switch (directions[currentPosition])
                {
                    case Direction.Down:
                        transform.Translate(0, -1.5f, 0);
                        break;
                    case Direction.Left:
                        transform.Translate(-1.5f, 0, 0);
                        break;
                    case Direction.Up:
                        transform.Translate(0, 1.5f, 0);
                        break;
                    case Direction.Right:
                        transform.Translate(1.5f, 0, 0);
                        break;
                }
                currentPosition++;
                if (currentPosition == directions.Length)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    private void ArrowUpdate()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].color = Color.white;
        }
        arrows[activeDirection].color = Color.yellow;
    }
}
