using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithFlip : MonoBehaviour
{
	public Transform pos1, pos2;
	public float speed;
	public Transform startPos;
	private Vector3 nextPos;

	void Start()
	{
		nextPos = startPos.position;
	}

	void Update()
	{
		if (transform.position == pos1.position)
		{
			FlipObjectfromPos1();
			nextPos = pos2.position;
		}
		if (transform.position == pos2.position)
		{
			FlipObjectfromPos2();
			nextPos = pos1.position;
		}

		transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(pos1.position, pos2.position);
	}

	private void FlipObjectfromPos1()
	{
		transform.Rotate(0, -180, 0);
	}

	private void FlipObjectfromPos2()
    {
		transform.Rotate(0, 180, 0);
	}


}
