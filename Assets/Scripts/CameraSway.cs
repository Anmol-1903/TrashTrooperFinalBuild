using UnityEngine;
public class CameraSway : MonoBehaviour
{
    [SerializeField] Transform _cameraClampLeft;
    [SerializeField] Transform _cameraClampRight;

    [SerializeField] Transform _cameraTarget;
 
    [SerializeField] Transform _dryClamp;
    [SerializeField] Transform _wetClamp;

    CameraShake _cs;

    float _percentage = 0.5f;
    float _cameraPercentage = 0.5f;

    [SerializeField] float _followSpeed = 1f;
 
    Transform _player;
    private void Awake()
    {
        _cs = GetComponentInParent<CameraShake>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _wetClamp = GameObject.FindGameObjectWithTag("WetClamp").transform;
        _dryClamp = GameObject.FindGameObjectWithTag("DryClamp").transform;
        _cameraClampLeft = GameObject.FindGameObjectWithTag("CamLeft").transform;
        _cameraClampRight = GameObject.FindGameObjectWithTag("CamRight").transform;
    }
    private void LateUpdate()
    {
        _percentage = CalculateLerpTime(_wetClamp.position, _dryClamp.position, _player.position);
        _cameraPercentage = Mathf.Lerp(_cameraPercentage, _percentage, Time.deltaTime * _followSpeed);
        transform.position = Vector3.Lerp(_cameraClampLeft.position, _cameraClampRight.position, _cameraPercentage);
        if (_cameraTarget != null && _cs != null)
        {

            if (!_cs.IsShaking())
            {
                Debug.Log("Target Locked");
                transform.LookAt(_cameraTarget);
            }
        }
    }
    public float CalculateLerpTime(Vector3 start, Vector3 end, Vector3 target)
    {
        float distance = Vector3.Distance(start, end);
        float targetDistance = Vector3.Distance(start, target);

        if (distance == 0f)
        {
            return 0f;
        }

        float targetPercentage = targetDistance / distance;

        return targetPercentage;
    }
}