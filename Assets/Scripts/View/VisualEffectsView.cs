using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsView : MonoBehaviour
{
    public string name = "";
    public float duration = 1.2f;
    public float maxDuration = 1.2f;

    private void OnEnable()
    {
        duration = maxDuration;
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;

        if(duration < 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
