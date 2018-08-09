using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

    private const float tile = 1.5f;

    /*[System.Serializable]
    public class Waypath
    {
        public Color color = Color.white;
        public Vector2[] checkpoint;
    }
    [System.Serializable]
    public struct Path
    {
        public Waypath path;
        public Waypath[] children;
    }*/

    public float speedMultiplier = 1;
    public SpriteRenderer renderererer;
    public bool lastLevel = false;
    //public Path[] paths;
    public LayerMask playerLayer = 8;

    private PlayerController player { get { return FindObjectOfType<PlayerController>(); } }
    private int currentPosition;
    private float timer;
    private bool active = false;
    //private Vector2 targetPosition;
    //private Vector2[] playerOccupiedCheckpoints;
    //private Vector2[] targetPath;

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
            if (lastLevel)
            {
                //FindPlayer();
                //GenerateTargetPath();
                Vector2 playerPosition = FindObjectOfType<LastLevel>().transform.position;
                if (transform.position.x > playerPosition.x)
                {
                    transform.Translate(-tile, 0, 0);
                    if (player.transform.position == transform.position)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    return;
                }
                if (transform.position.y < playerPosition.y)
                {
                    transform.Translate(0, tile, 0);
                    if (player.transform.position == transform.position)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    return;
                }
                if (transform.position.y > playerPosition.y)
                {
                    transform.Translate(0, -tile, 0);
                    if (player.transform.position == transform.position)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    return;
                }
                if (transform.position.x < playerPosition.x)
                {
                    transform.Translate(tile, 0, 0);
                    if (player.transform.position == transform.position)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    return;
                }
                if (player.transform.position == transform.position)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                return;
            }
            switch (player.directions[currentPosition])
            {
                case PlayerController.Direction.Down:
                    transform.Translate(0, -tile, 0);
                    break;
                case PlayerController.Direction.Left:
                    transform.Translate(-tile, 0, 0);
                    break;
                case PlayerController.Direction.Up:
                    transform.Translate(0, tile, 0);
                    break;
                case PlayerController.Direction.Right:
                    transform.Translate(tile, 0, 0);
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

    /*private void FindPlayer()
    {
        Vector2 playerPosition = FindObjectOfType<LastLevel>().transform.position;
        targetPosition = playerPosition;
    }

    private Vector2[] GenerateTargetPath()
    {
        Vector2[] generatedPath = new Vector2[0];
        //Get path which player is on
        Waypath point = paths[0].path;
        //Do a linecast between checkpoints to check if player is between them
        for (int i = 0; i < point.checkpoint.Length - 1; i++)
        {
            playerOccupiedCheckpoints = new Vector2[] { point.checkpoint[0], point.checkpoint[4] };
            if (Physics.Linecast(point.checkpoint[i], point.checkpoint[i + 1]))
            {
                //Found player! Set POC
                playerOccupiedCheckpoints = new Vector2[] { point.checkpoint[i], point.checkpoint[i + 1] };
                Debug.Log("Hello!");
                //Generate path from current position to player across available checkpoints

            }
        }
        return generatedPath;
    }

    private void OnDrawGizmos()
    {
        if (lastLevel)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                for (int j = 0; j < paths[i].path.checkpoint.Length - 1; j++)
                {
                    Gizmos.color = paths[i].path.color;
                    Gizmos.DrawLine(paths[i].path.checkpoint[j], paths[i].path.checkpoint[j + 1]);
                }
                for (int j = 0; j < paths[i].children.Length; j++)
                {
                    for (int k = 0; k < paths[i].children[j].checkpoint.Length - 1; k++)
                    {
                        Gizmos.color = paths[i].children[j].color;
                        Gizmos.DrawLine(paths[i].children[j].checkpoint[k], paths[i].children[j].checkpoint[k + 1]);
                    }
                }
            }
            Gizmos.DrawWireSphere(targetPosition, 0.2f);

            Gizmos.color = Color.red;
            try { Gizmos.DrawLine(playerOccupiedCheckpoints[0], playerOccupiedCheckpoints[1]); }
            catch { return; }
        }
    }*/
}
