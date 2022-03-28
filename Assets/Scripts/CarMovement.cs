using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private bool isCarOne;
    [SerializeField] private float switchSpeed = 1.0f;
    [SerializeField] private GameManager GM;

    private void Start()
    {
        leftLane = Screen.width * (isCarOne? 0.125f : 0.625f);
        rightLane = Screen.width * (isCarOne? 0.375f : 0.875f);
        
        leftLane = Camera.main.ScreenToWorldPoint(new Vector3(leftLane, 0, 0)).x;
        rightLane = Camera.main.ScreenToWorldPoint(new Vector3(rightLane, 0, 0)).x;
    }

    private void Update()
    {
        if (!GM.IsPlaying()) return;
        if ((isRight && transform.position.x < rightLane) ||
            (!isRight && transform.position.x > leftLane))
        {
            transform.Translate(transform.right * (isRight ? 1f : -1f) * switchSpeed * Time.deltaTime);
        }
        else
        {
            var pos = transform.position;
            pos.x = isRight ? rightLane : leftLane;
            transform.position = pos;

            if (Input.touchCount <= 0) return;
            foreach (var touch in Input.touches)
            {
                if(touch.phase != TouchPhase.Began) return;
                if ((touch.position.x <= Screen.width / 2 && isCarOne) ||
                    (touch.position.x > Screen.width / 2 && !isCarOne))
                {
                    MoveCar();
                }
            }
        }
    }

    private void MoveCar()
    {
        isRight = !isRight;
    }

    private bool isRight = true;
    private float rightLane;
    private float leftLane;

}
