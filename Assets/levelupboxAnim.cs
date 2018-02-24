using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelupboxAnim : MonoBehaviour {
    [SerializeField] float timeBeforeFade;
    [SerializeField] float fadeSpeed;
    
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//set active before fade & set active false after fade
	}

    void StartAnim()
    {

    }

    void Fade()
    {
        
    }
}