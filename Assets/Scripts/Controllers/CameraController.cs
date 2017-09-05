using UnityEngine;
using System.Collections;

public class CameraController : Singleton<CameraController> {
	public Vector2 focusAreaSize;

	[Header("Vertical Movement")]
	public float verticalOffset;
	public float verticalSmoothTime;
	public float minimumYPosition;

	[Header("Horizontal Movement")]
	public float horizontalOffset;

	FocusArea focusArea;

	float smoothVelocityY;

	bool isUpdating;

	Collider2D target;

	public void Init() {
		target = GameObject.FindWithTag("Player").GetComponent<Collider2D>();

		focusArea = new FocusArea(target.bounds, focusAreaSize);
		isUpdating = true;
	}

	void LateUpdate() {
		if (isUpdating) {
			focusArea.Update(target.bounds);

			Vector2 focusPos = focusArea.center + Vector2.up * verticalOffset + Vector2.right * horizontalOffset;

			focusPos.y = Mathf.SmoothDamp(transform.position.y, focusPos.y, ref smoothVelocityY, verticalSmoothTime);

			//Make sure we do not go below the specified minimum y position
			if (focusPos.y < minimumYPosition)
				focusPos.y = minimumYPosition;

			transform.position = (Vector3)focusPos + Vector3.forward * -10f;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube(focusArea.center, focusAreaSize);
	}

	struct FocusArea {
		public Vector2 center;
		public Vector2 velocity;
		float left, right;
		float top, bottom;

		public FocusArea(Bounds targetBounds, Vector2 size) {
			left = targetBounds.center.x - size.x / 2f;
			right = targetBounds.center.x + size.x / 2f;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			center = new Vector2((left + right) / 2f, (top + bottom) / 2f);
		}

		public void Update(Bounds targetBounds) {
			//Is the target moving against the left or right edge?
			float shiftX = 0;
			if (targetBounds.min.x < left)
				shiftX = targetBounds.min.x - left;
			else if (targetBounds.max.x > right)
				shiftX = targetBounds.max.x - right;

			//If it is shift the camera
			left += shiftX;
			right += shiftX;

			//Is the target moving against the bottom or top edge?
			float shiftY = 0;
			if (targetBounds.min.y < bottom)
				shiftY = targetBounds.min.y - bottom;
			else if (targetBounds.max.y > top)
				shiftY = targetBounds.max.y - top;

			//If it is shift the camera
			bottom += shiftY;
			top += shiftY;

			//Make sure we update the center position and update the camera's velocity
			center = new Vector2((left + right) / 2f, (top + bottom) / 2f);
			velocity = new Vector2(shiftX, shiftY);
		}

		public void Reset(Bounds targetBounds, Vector2 size) {
			left = targetBounds.center.x - size.x / 2f;
			right = targetBounds.center.x + size.x / 2f;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			center = new Vector2((left + right) / 2f, (top + bottom) / 2f);
		}
	}
}
