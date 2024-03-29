﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : RaycastController
{

	public float maxJumpHeight = 20;
	public float minJumpHeight = 5;
	public float timeToJumpApex = 4f;
	float accelerationTimeAirborne = .20f;
	float accelerationTimeGrounded = .10f;
	float moveSpeed = 60;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 30;
	public float wallStickTime = .250f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

	private SpriteRenderer sr;
	private Animator anim;


	void Start()
	{
		transform.localScale = new Vector3(1, 1, 0);
		controller = GetComponent<Controller2D>();
		anim = GetComponentInChildren<Animator>();
		sr = GetComponentInChildren<SpriteRenderer>();
		gravity = -(20 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}

	void Update()
	{
		UpdateRaycastOrigins();
		CalculateVelocity();
		HandleWallSliding();
		FlipSpriteOnInput();
		ChangeAnimationFromInput();
		controller.Move(velocity * Time.deltaTime, directionalInput);

		sr.transform.localRotation = Quaternion.FromToRotation(transform.up, controller.collisions.slopeNormal);

		if (wallSliding)
		{
			anim.SetBool("onWall", true);
		}
		else
		{
			anim.SetBool("onWall", false);
		}
		if (controller.collisions.below) {
			anim.SetBool("isJumping", false);
		}
		if (controller.collisions.climbingSlope) {
			
			
		}
		if (controller.collisions.above || controller.collisions.below)
		{
			if (controller.collisions.slidingDownMaxSlope)
			{
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			}
			else
			{
				velocity.y = 0;
			}
		}
	}

	public void Shrink() {
		//transform.localScale -= new Vector3(0.1f, 0.1f, 0);

		print("working");
	}
	
	public void PlayerDeath() {
		print("player died");		
	}
	void ChangeAnimationFromInput()
	{
		if (controller.collisions.below)
		{
			if (Input.GetAxis("Horizontal") != 0)
			{
				anim.SetBool("isRunning", true);
				anim.SetBool("isIdle", false);
			}
			else
			{
				anim.SetBool("isRunning", false);
				anim.SetBool("isIdle", true);
			}
		}
		else {
			anim.SetBool("isRunning", false);
			anim.SetBool("isJumping", true);
		}


	}
	void FlipSpriteOnInput()
	{
		if (Input.GetAxis("Horizontal") > 0 && !wallSliding)
		{
			sr.flipX = false;
		}
		else if (Input.GetAxis("Horizontal") < 0 && !wallSliding)
		{
			sr.flipX = true;
		}
	}
	public void SetDirectionalInput(Vector2 input)
	{
		directionalInput = input;
	}

	public void OnJumpInputDown()
	{
		if (wallSliding)
		{
			if (wallDirX == directionalInput.x)
			{
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0)
			{
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else
			{
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below)
		{
			//isgrounded
			
			anim.SetBool("isJumping", true);

			if (controller.collisions.slidingDownMaxSlope)
			{
				if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
				{ // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			}
			else
			{
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp()
	{
		if (velocity.y > minJumpVelocity)
		{
			velocity.y = minJumpVelocity;
		}
	}


	void HandleWallSliding()
	{
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
		{
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax)
			{
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0)
			{
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0)
				{
					timeToWallUnstick -= Time.deltaTime;
				}
				else
				{
					timeToWallUnstick = wallStickTime;
				}
			}
			else
			{
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity()
	{
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}
}