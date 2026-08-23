# Flappy Bird Clone (Unity)

Simple Flappy Bird clone. Course assignment, Game Dev with Unity.

## Engine

Unity 6000.3.20f1 (Unity 6). URP 2D template.

## Controls

- **Space** — flap (apply upward velocity)

## Scripts (`Assets/Scripts`)

| Script | Attached to | Role |
|---|---|---|
| `FlyBehavior.cs` | Bird | Reads Space key, sets `Rigidbody2D.linearVelocity` upward. Rotates bird based on vertical velocity. Calls `GameManager.instance.GameOver()` on any 2D collision. |
| `GameManager.cs` | GameManager (singleton) | Holds game-over canvas reference. `GameOver()` shows canvas + freezes time (`Time.timeScale = 0`). `RestartGame()` reloads active scene. |
| `PipeSpawner.cs` | Spawner | Spawns pipe prefab on a timer (`_maxTime`), randomizing vertical spawn offset within `_heightRange`. Destroys each spawned pipe after 4s. |
| `MovePipe.cs` | Pipe prefab | Moves pipe left at constant speed. |
| `PipeIncreaseScore.cs` | Pipe's scoring trigger | On trigger enter by tagged `"Player"`, calls `Score.instance.UpdateScore()`. |
| `Score.cs` | Score UI (singleton) | Tracks current score, updates TMP text, persists high score via `PlayerPrefs`. |
| `LoopGround.cs` | Ground sprite | Grows sprite width over time to fake scrolling, resets to start size once past `_width`. |

## Scene

`Assets/Scenes/SampleScene.unity` — main gameplay scene.

## Architecture notes

- Two singletons: `GameManager.instance` and `Score.instance`, both set in `Awake()`.
- Game-over is a freeze (`Time.timeScale = 0`), not a scene reload — restart is a separate explicit call (`RestartGame`), presumably wired to a UI button in the scene.
- Pipes are self-destroying after a fixed 4s lifetime rather than on leaving camera bounds.
- High score is persisted locally via `PlayerPrefs` (per-machine, not cloud).

## Repo

Remote: https://github.com/jjlimin/myFlappyBird.git (read-only reference for now).

## Status

Work in progress course assignment. No gameplay/code changes made by tooling — review only.
