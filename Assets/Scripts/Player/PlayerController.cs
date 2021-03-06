﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Animator))]
	public class PlayerController : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField] private float jumpForce = 400f;
		[SerializeField] [Range(0, .3f)] private float movementSmoothing = .05f;
		[SerializeField] [Range(0, 20.0f)] private float speed = 10.0f;
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] private Transform groundCheck;
		
		[Header("Dialogue")]
		public YarnProgram[] yarnScripts;
		
		[Header("Events")] 
		public UnityEvent onLandEvent;

		private const float GroundedRadius = .2f;
		private bool _grounded;
		private bool _facingRight = true;
		private string _currentAnimatorState;
		private Vector3 _velocity = Vector3.zero;
		private DialogueRunner _dialogueRunner;
		private Rigidbody2D _rigidbody2D;
		private Animator _animator;
		private Dictionary<string, string> _animations;


		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			
			if (yarnScripts.Length > 0)
			{
				_dialogueRunner = FindObjectOfType<DialogueRunner>();

				foreach (YarnProgram yarnScript in yarnScripts)
					_dialogueRunner.Add(yarnScript);
			}
			
			_animator = GetComponent<Animator>();
			AnimationClip[] animationClips = _animator.runtimeAnimatorController.animationClips;
			_animations = new Dictionary<string, string>()
			{
				["Idle"] = animationClips[0].name,
				["Walk"] = animationClips[1].name,
				["Jump"] = animationClips[2].name,
			};

			if (onLandEvent == null)
				onLandEvent = new UnityEvent();
		}

		private void Start()
		{
			ChangeAnimationState("Idle");
		}

		private void FixedUpdate()
		{
			bool wasGrounded = _grounded;
			_grounded = false;

			Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, groundLayer);
			foreach (var t in colliders)
			{
				if (t is CompositeCollider2D)
				{
					_grounded = true;
					if (!wasGrounded)
						onLandEvent.Invoke();
				}
			}
		}

		public void Move(float move)
		{
			Vector2 velocity = _rigidbody2D.velocity;
			Vector3 targetVelocity = new Vector2(move * 10 * speed, velocity.y);

			_rigidbody2D.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);

			if (_grounded)
				ChangeAnimationState(Math.Abs(move) > 0 ? "Walk" : "Idle");
			else
				ChangeAnimationState("Jump");

			if (move > 0 && !_facingRight || move < 0 && _facingRight)
				Flip();
		}

		public void Jump()
		{
			if (_grounded)
			{
				_grounded = false;
				_rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			}
		}

		private void Flip()
		{
			_facingRight = !_facingRight;

			Transform newTransform = transform;
			Vector2 theScale = newTransform.localScale;
			
			theScale.x *= -1;
			newTransform.localScale = theScale;
		}

		private void ChangeAnimationState(string newState)
		{
			if (_currentAnimatorState == newState) return;

			_animator.Play(_animations[newState], -1);
			_currentAnimatorState = newState;
		}
	}
}