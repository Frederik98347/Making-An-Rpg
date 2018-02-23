using UnityEngine;

public class CombatText : MonoBehaviour {

    float speed;
    Vector3 dir;
    float fadeTime;

    public float FadeTime
    {
        get
        {
            return fadeTime;
        }

        set
        {
            fadeTime = value;
        }
    }

    public Vector3 Dir
    {
        get
        {
            return dir;
        }

        set
        {
            dir = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Update is called once per frame
    void Update () {
        float translation = Speed * Time.deltaTime;

        transform.Translate(Dir * translation);

	}

    public void Initialize(float speed, Vector3 dir) 
    {
        this.speed = Speed;
        this.dir = Dir;
    }
}
