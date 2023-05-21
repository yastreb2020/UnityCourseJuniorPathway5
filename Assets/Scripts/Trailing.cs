using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailing : MonoBehaviour
{
    private TrailRenderer trailEffect;
    // Start is called before the first frame update
    void Start()
    {
        trailEffect = GetComponent<TrailRenderer>();
        trailEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        trailEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnActive()
    {
        trailEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }


}
