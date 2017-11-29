using UnityEngine;
using System.Collections;

public static class InputManager {
	public static bool turnAssist = true;

	public static Vector3 GetFlightControl() {
		if (Input.GetButtonDown ("TurnAssist")) {
			turnAssist = !turnAssist;
		}

		float yaw = (turnAssist) ? Input.GetAxis ("Yaw") : 0f;
		float pitch = Input.GetAxis ("Pitch");
		float roll = (turnAssist) ? 0f : Input.GetAxis ("Roll");

		// TODO: Ailerons + Rudder?

		return new Vector3(pitch, yaw, roll);
	}

	public static float GetThrottle () {
		return Input.GetAxis ("Throttle");
	}

	public static float GetBrake() {
		return Input.GetAxis ("Brake");
	}

	public static bool GetPrimaryFire () {
		return Input.GetButton ("Weapon_Primary");
	}

	public static bool GetSecondaryFire() {
		return Input.GetButton ("Weapon_Secondary");
	}
}

