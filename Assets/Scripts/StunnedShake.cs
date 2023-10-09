using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedShake : MonoBehaviour
{
    [SerializeField] float _duration , amplitude , range;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] bool _showShake;

    private void Update()
    {
        if (_showShake)
        {
            _showShake = false;
            StartCoroutine(StunShake());
        }
    }

    IEnumerator StunShake()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {

            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _duration);
            //range = Random.onUnitSphere.x * amplitude;
            transform.position = startPos.normalized *  Random.insideUnitCircle  * strength * Time.timeScale;
            yield return null;
        }
        Time.timeScale = 1f;
        transform.position = startPos;
    }
}
