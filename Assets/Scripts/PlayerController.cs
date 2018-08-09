using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public struct AnimationArray
    {
        public Direction direction;
        public Sprite[] sprites;
    }

    public AnimationArray[] playerAnimations = new AnimationArray[4];
    public float songBPM;
    public float speedMultiplier = 1;
    public float predelay;
    public SpriteRenderer[] arrows; //0 = Left, 1 = Up, 2 = Right, 3 = Down
    public enum Direction { Left, Up, Right, Down }
    public Direction[] directions;
    public Direction playerDirection;
    [HideInInspector] public int currentPosition;

    protected int activeDirection;
    protected int animationNumber = 0;
    protected float timer;
    protected float animationTimer;
    protected SpriteRenderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        animationTimer += songBPM / 240;
    }

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
        Move();
        Animate();
    }

    protected virtual void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeDirection == (int)directions[currentPosition])
            {
                activeDirection = (activeDirection + 1) % 4;
                ArrowUpdate();
                playerDirection = directions[currentPosition];
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

    private void Animate()
    {
        if (Time.time >= animationTimer)
        {
            animationTimer += songBPM / 240;
            animationNumber = (animationNumber + 1) % 4;
        }
        m_Renderer.flipX = playerDirection == Direction.Left;
        m_Renderer.sprite = GetAnimationArray(playerDirection).sprites[animationNumber];
    }

    private AnimationArray GetAnimationArray(Direction dir)
    {
        for (int i = 0; i < playerAnimations.Length; i++)
        {
            if (playerAnimations[i].direction == dir)
            {
                return playerAnimations[i];
            }
        }
        return new AnimationArray();
    }

    protected void ArrowUpdate()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].color = Color.white;
        }
        arrows[activeDirection].color = Color.yellow;
    }
}
