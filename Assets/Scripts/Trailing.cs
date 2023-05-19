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
    }

    // Update is called once per frame
    void Update()
    {
        trailEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
