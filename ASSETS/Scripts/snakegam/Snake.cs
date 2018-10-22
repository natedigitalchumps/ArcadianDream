using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {

    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;
   public List<Transform> tail = new List<Transform>();
    // Did the snake eat something?
    bool ate = false;
    public static Snake instance;
    // Tail Prefab
    public GameObject tailPrefab;
    // Use this for initialization
    public enum snakeDirection { up,down,left,right,stop};
    public snakeDirection SnakeMovementDirection;
    public bool DirectionChanged = false;

    void Awake()
    {
        instance = this;
        SnakeMovementDirection = snakeDirection.right;
    }
    void Start()
    {
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    public void Update()
    {
#if UNITY_EDITOR_WIN

        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
#else

        DirectionChanged = false;
          switch (SnakeMovementDirection)
        {
            case snakeDirection.right:
                if(dir!=-Vector2.left)
                {
                    dir = Vector2.right;
                }
                
                break;
            case snakeDirection.left:
                if(dir!=Vector2.right)
                {
                    dir = -Vector2.right;
                }                
                break;
            case snakeDirection.up:
                if(dir!=-Vector2.up)
                {
                    dir = Vector2.up;
                }
                break;
            case snakeDirection.down:
                if(dir!=Vector2.up)
                {
                    dir = -Vector2.up;
                }                
                break;
        case snakeDirection.stop:
                dir = Vector2.zero;
                break;
        }

#endif

    }

    void Move()
    {
        if (SnakeMovementDirection != snakeDirection.stop)
        {

            // Save current position (gap will be here)
            Vector2 v = transform.position;

            // Move head into new direction (now there is a gap)
            transform.Translate(dir * 2);

            // Ate something? Then insert new Element into gap
            if (ate)
            {
                // Load Prefab into the world
                GameObject g = Instantiate(tailPrefab, v, Quaternion.identity);
                Vector3 vec = new Vector3(v.x, v.y, transform.position.z);
                g.transform.position = vec;

                // Keep track of it in our tail list
                tail.Insert(0, g.transform);

                // Reset the flag
                ate = false;
            }
            // Do we have a Tail?
            else if (tail.Count > 0)
            {
                // Move last Tail Element to where the Head was
                Vector3 vec = new Vector3(v.x, v.y, transform.position.z);
                tail.Last().position = vec;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.CompareTag("snakefood"))
        {
            // Get longer in next Move call
            ate = true;
            UIControl.instant.ScoreInc();
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
         //   print("touched something");
            SnakeMovementDirection = snakeDirection.stop;
            
            StartCoroutine(SnakeGameOver());
        }
    }

    IEnumerator SnakeGameOver()
    {
        yield return new WaitForSeconds(1f);
        UIControl.instant.overObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneFader.instance.FadeChoice();
        yield return new WaitForSeconds(3f);
        SnakeGameManager.manager.ChangeScene();
    }
}
