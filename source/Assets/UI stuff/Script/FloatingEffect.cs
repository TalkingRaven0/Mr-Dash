
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

public class FloatingEffect : MonoBehaviour
{
    public float floatspeed = 1;
    public float range = 1;
    private Vector3 target_up;
    private Vector3 target_down;
    private float position = 0;
    private float sinpos;
    private void Awake()
    {
        target_up = new Vector3(transform.position.x, transform.position.y + range);
        target_down = new Vector3(transform.position.x, transform.position.y - range);
    }

    private void FixedUpdate()
    {
        sinpos = (1 + (math.sin(position))) / 2;
        transform.position = Vector3.Lerp(target_up, target_down,sinpos);
        _float();
    }

    void _float()
    {
        position += floatspeed * Time.deltaTime;
        if (position >= 2 * math.PI)
            position = 0;
    }

}
