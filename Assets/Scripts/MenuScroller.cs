using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScroller : MonoBehaviour
{
    [SerializeField] float moveSpeedx;

    Vector2 offset;
    Material material;
    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new(moveSpeedx * Time.deltaTime, material.mainTextureOffset.y);
        material.mainTextureOffset += offset;
    }
}
