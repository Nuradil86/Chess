using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// scenario for move the yellow plate
public class MovePlate : MonoBehaviour
{
    // reference to controller
    public GameObject controller;

    // reference moving chess piece with plate moving
    GameObject reference = null;

    int matrixX;
    int matrixY;

    // false: movement, true = attacking
    public bool attack = false;

    public void Start()
    {
        // this figure attacks or not
        if (attack)
        {
            // chage to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    // cursore mouse 
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // if it's attack figure previous chess piece should changes
        if (attack)
        {
            // (15-16)
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
        }

        // doing controller location empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        // when move platet in moving
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        // give coordinates
        reference.GetComponent<Chessman>().SetCoords();


        // tracking reference
        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        // destroy move plate
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    // move plate
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    // reference = to object
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }


    // ability public reference to any chess pieces 
    public GameObject GetReference()
    {
        return reference;
    }
}