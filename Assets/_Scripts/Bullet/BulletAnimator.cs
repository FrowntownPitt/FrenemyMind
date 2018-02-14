using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimator : MonoBehaviour {

    private enum State
    {
        small,
        large
    }
    

    private State currentState = State.small;
    public GameObject bulletSprite;
    private Transform t;

    public float scaleAmt = 1.1f;
    public float scaleTime = 0.2f;

	// Use this for initialization
	void Start () {
        t = bulletSprite.GetComponent<Transform>();
        StartCoroutine(Animate());
	}
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator Animate()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.small:
                    {
                        currentState = State.large;
                        t.localScale *= scaleAmt;
                        break;
                    }
                case State.large:
                    {
                        currentState = State.small;
                        t.localScale /= scaleAmt;
                        break;
                    }
            }
            yield return new WaitForSeconds(scaleTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("window"))
        {
            Destroy(gameObject);
            StopAllCoroutines();
        }
    }
}
