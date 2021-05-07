using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAsteroids : MonoBehaviour
{
	public Transform pos1, pos2;
	public float speed;
	public Transform startPos;
	private Vector3 nextPos;

	private void Start()
	{
		nextPos = startPos.position;
	}

	private void Update()
	{
		if (transform.position == pos1.position)
		{
			nextPos = pos2.position;
		}
		if (transform.position == pos2.position)
		{
			nextPos = pos1.position;
		}

		transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(pos1.position, pos2.position);
	}
}
