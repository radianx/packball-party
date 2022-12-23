using System;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    bool wasJustClicked;
    bool canMove;
    Vector2 playerSize;

    Rigidbody2D rigidBody;

    public Transform BoundaryHolder;

    Boundary playerBoundary;

    struct Boundary {
        public float Up, Down, Left, Right;

        public Boundary(float up, float down, float left, float right){
            Up = up; Down = down; Left = left; Right = right;
        }
    }

    // Start is called before the first frame update
    void Start(){
        playerSize = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        rigidBody  = GetComponent<Rigidbody2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButton(0)){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (wasJustClicked){
                wasJustClicked = false;
                bool isMouseInsideX = mousePos.x > transform.position.x - playerSize.x && mousePos.x < transform.position.x + playerSize.x;
                bool isMouseInsideY = mousePos.y > transform.position.y - playerSize.y && mousePos.y < transform.position.y + playerSize.y;

                if((isMouseInsideX) && (isMouseInsideY)){
                    canMove = true;
                } else {
                    canMove = false;
                }
            }

            if (canMove) {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right), 
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down, playerBoundary.Up));
                rigidBody.MovePosition(clampedMousePos);
            }

        } else {
            wasJustClicked = true;
        }
    } 
}
