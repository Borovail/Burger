using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseMovement : MonoBehaviour
{
    public readonly int poleSizeX = 40;
    public readonly int poleSizeY = 40;
    public float moveSpead = 5f;
    public int positionX = 0;
    public int positionY = 0;
    public bool heal = true;
    private Vector3 _targetPosition;

    private void PositionMauseRandom()
    {
        positionX = UnityEngine.Random.Range(-poleSizeX, poleSizeX);  
        positionY = UnityEngine.Random.Range(-poleSizeY, poleSizeY);
        _targetPosition = new Vector3(positionX, 0f, positionY);
    }

    public bool Kill(bool heal) => heal;
    public void Start() => PositionMauseRandom();

    public void RaycastsBlock()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0); 
            Ray ray = new Ray(transform.position, transform.forward + offset);
            Debug.DrawRay(transform.position, (transform.forward + offset) * 20f, Color.red);
        }
    }

    private void OnColisionEnter(Collision collision)
    {
        PositionMauseRandom();
    }

    public void Update()
    {
        // RaycastsBlock();
        // Vector3 targetPosition = new Vector3(positionX, 0f, positionY);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpead * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            PositionMauseRandom();
        }

        if (!Kill(heal))
        {
            Destroy(gameObject);
        }
    }
}
