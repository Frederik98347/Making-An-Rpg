using UnityEngine;
using UnityEngine.UI;

public class FloatingTextControllerv1 : MonoBehaviour {

    Text mytext;

    [SerializeField] float moveAmt;
    [SerializeField] float moveSpeed;

    Vector3[] moveDirs;
    Vector3 myMoveDir;

    bool canMove = false;

    private void Start()
    {
        moveDirs = new Vector3[]
        {
            transform.up,
            (transform.up + transform.right),
            (transform.up + -transform.right)
        };

        myMoveDir = moveDirs[Random.Range(0, moveDirs.Length)];
    }

    private void Update()
    {
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + myMoveDir, moveAmt * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetTextAndMove(string textStr, Color textColour)
    {
        mytext = GetComponentInChildren<Text>();
        mytext.color = textColour;
        mytext.text = textStr;
        canMove = true;
    }
}
