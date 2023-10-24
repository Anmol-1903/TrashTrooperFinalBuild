using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] bool _showShake;
    private void Start()
    {
        _showShake = false;
    }
    public void ShakeCamera()
    {
        if(!_showShake)
            StartCoroutine(Shake());
    }
    public bool IsShaking()
    {
        return _showShake;
    }
    IEnumerator Shake()
    {
        _showShake = true;
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
        _showShake = false;
    }
}
