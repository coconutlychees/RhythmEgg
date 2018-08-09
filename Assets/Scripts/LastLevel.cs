using UnityEngine;
using UnityEngine.SceneManagement;

public class LastLevel : PlayerController {

    protected override void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            switch (activeDirection)
            {
                case 0:
                    if (!Physics2D.Raycast(transform.position, Vector2.left, 2).collider)
                    {
                        transform.Translate(-1.5f, 0, 0);
                        activeDirection = (activeDirection + 1) % 4;
                        playerDirection = Direction.Left;
                        break;
                    }
                    Collider2D hitCol = Physics2D.Raycast(transform.position, Vector2.left, 1).collider;
                    if (hitCol && hitCol.CompareTag("Finish"))
                    {
                        transform.Translate(-1.5f, 0, 0);
                        activeDirection = (activeDirection + 1) % 4;
                        playerDirection = Direction.Left;
                    }
                    break;
                case 1:
                    if (!Physics2D.Raycast(transform.position, Vector2.up, 2).collider)
                    {
                        transform.Translate(0, 1.5f, 0);
                        activeDirection = (activeDirection + 1) % 4;
                        playerDirection = Direction.Up;
                    }
                    break;
                case 2:
                    if (!Physics2D.Raycast(transform.position, Vector2.right, 2).collider)
                    {
                        transform.Translate(1.5f, 0, 0);
                        activeDirection = (activeDirection + 1) % 4;
                        playerDirection = Direction.Right;
                    }
                    break;
                case 3:
                    if (!Physics2D.Raycast(transform.position, Vector2.down, 2).collider)
                    {
                        transform.Translate(0, -1.5f, 0);
                        activeDirection = (activeDirection + 1) % 4;
                        playerDirection = Direction.Down;
                    }
                    break;
            }
            ArrowUpdate();
            if (Physics2D.Raycast(transform.position, Vector2.zero, 0).collider && Physics2D.Raycast(transform.position, Vector2.zero, 1).collider.CompareTag("Finish"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}