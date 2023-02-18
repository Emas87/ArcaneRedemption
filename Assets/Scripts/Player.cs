using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int _speed = 5;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float nextFire = 0;
    [SerializeField]
    private int _lives = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && canJump())
        {
            Jump();
        }
    }

    void FireLaser()
    {
        nextFire = Time.time + _fireRate;
        Instantiate(_bullet, transform.position + new Vector3(0, (float)0.8, 0), Quaternion.identity);
    }

    void Jump()
    {

    }

    bool canJump()
    {
        return true;
    }

    public void Damage()
    {
        _lives--;
        if (_lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticallInput = Input.GetAxis("Vertical");
        transform.Translate(_speed * horizontalInput * Time.deltaTime * Vector3.right);
        transform.Translate(_speed * Time.deltaTime * verticallInput * Vector3.up);
    }
}
