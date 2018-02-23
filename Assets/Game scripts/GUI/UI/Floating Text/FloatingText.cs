using UnityEngine;

public class FloatingTextv1 : MonoBehaviour {

    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject enemyMarker;
    [SerializeField] Color[] textColor;
    [SerializeField] float textKillTime;

    public void HitNow()
    {
        GameObject newText = Instantiate(enemyMarker, playerObject.transform.position, Quaternion.identity);
        newText.SetActive(true);
        newText.GetComponent<FloatingTextControllerv1>().SetTextAndMove(Random.Range(0, 101).ToString(),
            textColor[Random.Range(0, textColor.Length)]);
        Destroy(newText.gameObject, textKillTime);
    }
}