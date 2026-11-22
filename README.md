# Profiling Lab

The Week 10 practice project for **MOD010307 Mobile Game Programming**: profiling and optimisation in Unity 6.3.

It starts from the Week 9 project (the microgame framework, MainMenu, PracticeTap, the Camera Lab and the Device Lab) and adds one practice scene, the **Profiling Lab** (`Assets/Practice/Week10`): a reef of 400 fish with deliberately slow code, and an on-screen readout of frame time, script time, garbage per frame, draw calls and SetPass calls. Each step of the Week 10 tutorial is one commit, so you can compare your work with any step.

> **Practice, not assessment.** The problems in the Lab were planted to be found. Your LO5 performance investigation must be on one of your own microgames or your framework.

## The starter

`Starter/ProfilingLab_Starter.unitypackage` is the Lab as it is at step P0. Import it into your own microgame project with **Assets > Import Package > Custom Package…**; everything goes into `Assets/Practice/Week10`.

## Open this project

Clone it (or **Code > Download ZIP**), then in Unity Hub choose **Add > Add project from disk**. It uses **Unity 6.3** (6000.3.17f1). Open `Assets/Practice/Week10/ProfilingLab`, set the Game view to a portrait size such as 1080 × 1920, and press Play.

## What each step changes

| Step | Change | What it shows |
|---|---|---|
| P0 | — | The starter: `ShoalSpawner`, `ShoalFish`, `ShoalBubble` and `PerfReadout` (ProfilerRecorder) |
| P1 | `ShoalFish`: two `ProfilerMarker`s | Labelling your own code in the Profiler: AvoidNeighbours is nearly the whole frame |
| P2 | `ShoalFish.All` list (OnEnable / OnDisable) instead of `FindObjectsByType` | No more searching the scene or garbage arrays; still O(n²) |
| P3 | Check neighbours every 0.25 s, staggered; `sqrMagnitude`; cached `SpriteRenderer` | Doing the work less often, and spreading it across frames |
| P4 | `ObjectPool<T>` for fish and bubbles; reset in `OnEnable` | Pooling instead of Instantiate / Destroy, and judging whether it was worth it |
| P5 | Bubble prefab material: Sprite-Unlit-Default → Sprite-Lit-Default | SetPass calls and SRP Batches: interleaving two shaders in sorting order breaks batches |
| P6 | LightRay Mesh Type Full Rect → Tight; six beams → two | Overdraw, seen in the Scene view's Overdraw draw mode |

## Our measurements

Test PC, Editor Play mode, 1080 × 1920 Game view, 400 fish, no Profiler window open; the middle of three runs. Your numbers will differ: compare your own rows.

| Row | Change | Frame (ms) | Game (ms) | Scripts (ms) | Garbage (KB/frame) | Draw calls | SetPass calls |
|---|---|---|---|---|---|---|---|
| P0 | Starting point (baseline) | 54 | 47 | 45 | 2,500 | 920 | 197 |
| P2 | List instead of FindObjectsByType | 30 | 22 | 20 | 85 | 910 | 199 |
| P3 | Check less often | 19 | 6.4 | 1.5 | 62 | 940 | 203 |
| P4 | Object pools | 9.8 | 5.1 | 1.0 | 22 | 950 | 202 |
| P5 | Same material for bubbles | 10.4 | 5.1 | 1.0 | 22 | 960 | 7 |
| P6 | Less overdraw | 10.0 | 4.7 | 1.1 | 22 | 960 | 7 |

*Frame* includes the Editor's own work; *Game* (PlayerLoop) is only the game. Pooling (P4) cut Game by about 1.3 ms; most of the bigger drop in Frame was the Editor no longer redrawing hundreds of new objects in the Hierarchy window.

## Commit map

| Commit | Step |
|---|---|
| `9a1e3e1` | Starting point: the Week 9 project (microgame framework, MainMenu, PracticeTap, Camera Lab and Device Lab) |
| `4b9efd1` | P0: add the Profiling Lab starter: a reef of 400 fish with deliberately slow code, and an on-screen performance readout |
| `d849385` | P1: label AvoidNeighbours and Swim with ProfilerMarkers |
| `1aa921a` | P2: keep a list of every fish instead of searching the scene with FindObjectsByType every frame |
| `add0b3e` | P3: check neighbours four times a second, spread over frames; compare squared distances; keep the SpriteRenderer |
| `0ecc04c` | P4: pool the fish and bubbles with ObjectPool<T> instead of Instantiate and Destroy |
| `c9ef8b2` | P5: give the bubbles the same material as the fish, so they batch (SetPass calls about 200 to 7) |
| `904465e` | P6: cut overdraw: Tight mesh for the light rays, and two beams instead of six |

The README and the starter package are the last commit.

## Credits

Fish, bubble, rock, seaweed and sand sprites: [Kenney Fish Pack](https://kenney.nl/assets/fish-pack) (CC0); the licence is in `Assets/Practice/Week10/Sprites`. `LightRay.png` was made for this project.
