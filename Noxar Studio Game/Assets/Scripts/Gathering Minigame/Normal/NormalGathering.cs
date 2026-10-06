using UnityEngine;
using System.Collections;

public class NormalGathering : MonoBehaviour
{

    public RectTransform image;
    public float speed = 1f;

    private void Update()
    {
        float rotation = Mathf.Lerp(-28f, 45f, Mathf.PingPong(Time.time * speed, 1f));

        image.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
