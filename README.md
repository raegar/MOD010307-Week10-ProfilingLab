# Device Lab

The Week 9 practice project for **MOD010307 Mobile Game Programming**: mobile UI and device interaction in Unity 6.3.

It starts from the Week 8 project (the `MicrogameCourse.Framework` scripts, MainMenu, PracticeTap and the Camera Lab) and adds one practice scene, the **Device Lab** (`Assets/Practice/Week09`). Each step of the Week 9 tutorial is one commit, so you can compare your work with any step. The last step moves the general-purpose helpers into `Assets/Scripts/Mobile` and uses them across the framework.

> **Practice, not assessment.** The helpers in `Assets/Scripts/Mobile` are framework tools: if you use them, say in your README that they came from the Week 9 tutorial. They aren't Learning Outcome 3 evidence. Tilt, gyro and multi-touch *mechanics* in your microgames must be your own code.

## Open it

Clone it (or **Code > Download ZIP**), then in Unity Hub choose **Add > Add project from disk**. It uses **Unity 6.3** (6000.3.17f1). Open `Assets/Practice/Week09/DeviceLab` and press Play in the **Device Simulator** (Window > General > Device Simulator), or open `Assets/Scenes/MainMenu` and choose **Practice: Device Lab**.

## What each step adds

| Step | Scripts | What it shows |
|---|---|---|
| A0 | — | A 2D scene: Kenney sprites at 70 pixels per unit, a 9 × 16 arena with an Edge Collider 2D, a top-down Rigidbody 2D ball, and a Canvas with the framework's Canvas Scaler settings |
| A1 | `ScreenInfo`, `SafeArea`, `CameraFit` | Screen size, aspect ratio, DPI and safe area; a HUD panel anchored to `Screen.safeArea`; an orthographic camera sized so the whole play area fits any shape of screen |
| A2 | `TiltInput`, `TiltBall` | The accelerometer through the Input System: `EnableDevice`, readings in g, calibration, a dead zone, Lerp smoothing and a keyboard fallback |
| A3 | `GyroDial`, `OrientationLock` | The gyroscope's angular velocity integrated into an angle, drift and Recentre; per-scene `Screen.orientation` |
| A4 | `TouchTargetAudit`, `PressFeedback` | Every control measured in millimetres with `Screen.dpi`; bigger buttons; press feedback a finger can't hide |
| A5 | `PinchTwist`, `Haptics`, `HapticsToggle`; `TiltBall` v2 | Two-finger pinch and twist with Enhanced Touch; `Handheld.Vibrate` behind a static helper with a saved setting; a buzz only on hard knocks |
| A6 | `MicrogameSession`, `SceneNavigator` (one line each) | Helpers moved to `Assets/Scripts/Mobile`; SafeArea and the audit in PracticeTap; one buzz at the end of every game; Device Lab and Vibration on the MainMenu |

## Controls

| On a phone | In the Editor | Does |
|---|---|---|
| Tilt | Arrow keys or WASD | Rolls the green alien |
| Twist like a steering wheel | Q and E | Turns the pink alien the other way, so it stays upright in the room |
| Two-finger pinch and twist | Mouse wheel (size only) | Resizes and turns the star |
| Calibrate / Recentre | Click them | Treat the current tilt as level / put the dial upright |

## Commit map

| Commit | Step |
|---|---|
| `bfcd564` | Starting point: the Week 8 project (microgame framework, MainMenu, PracticeTap and the Camera Lab) |
| `9c7b93b` | A0: add the Device Lab scene |
| `2b22536` | A1: ScreenInfo, SafeArea, CameraFit |
| `c76e8cc` | A2: TiltInput, TiltBall |
| `2afb9f0` | A3: GyroDial, OrientationLock |
| `6bd16fb` | A4: TouchTargetAudit, PressFeedback |
| `716d9e3` | A5: PinchTwist, Haptics, HapticsToggle |
| `feef8dd` | A6: into the framework |

The README is the last commit.

## Manual tests

| Test | Expected |
|---|---|
| Play the Device Lab in the Device Simulator on a notched phone (A1) | Info shows the phone's size, aspect, DPI, safe area and orientation, below the notch |
| Switch the Simulator to an iPad, then a tall phone | The whole green arena is on screen on both |
| Hold the arrow keys (A2) | The ball rolls that way and bounces off all four walls |
| Hold Q, then press Recentre (A3) | The dial turns clockwise, then snaps upright |
| Play in the Device Simulator at A4 | The Console reports every control as OK (at least 9 mm) |
| Roll the ball gently, then hard, into a wall (A5) | Only the hard knock logs `Haptics: buzz` |
| Untick Vibration, stop, play again | Still unticked |
| MainMenu → Practice: Tap → play to the end | The timer stays clear of the notch; one `Haptics: buzz` at the result |
| On a phone: MainMenu → Practice: Device Lab → Menu | The Lab opens, tilt and twist work, Menu goes back |

## Credits

- Sprites: **Physics Assets** by Kenney (kenney.nl), CC0.
- Microgame framework: the MOD010307 framework pack.
