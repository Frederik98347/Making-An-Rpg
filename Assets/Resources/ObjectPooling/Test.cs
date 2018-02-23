using UnityEngine;

public class Test : MonoBehaviour {

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void FixedUpdate () {
        objectPooler.SpawnFromPool("Cube", transform.position, transform.rotation);
	}
}