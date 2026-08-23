# Flappy Bird Clone (Unity)

Simple Flappy Bird clone. Course assignment, Game Dev with Unity.

## Engine

Unity 6000.3.20f1 (Unity 6). URP 2D template.

## Controls

- **Space** or **Left Mouse Click** — flap (apply upward velocity). First press also starts the game from the start screen.

## Game flow

Three states, tracked by `GameManager.State` (`GameState` enum: `NotStarted` → `Playing` → `GameOver`):

- **NotStarted** — start screen showing logo + tap prompt. Bird floats in place (gravity disabled), background/ground parallax scroll, no pipes spawn.
- **Playing** — first flap input triggers `GameManager.StartGame()`: hides start screen, shows score UI, re-enables bird gravity, starts pipe spawning.
- **GameOver** — triggered on bird collision. Shows game-over canvas, freezes gameplay (`Time.timeScale = 0`), flap/sound input disabled. `RestartGame()` reloads the scene.

## Scripts (`Assets/Scripts`)

| Script | Attached to | Role |
|---|---|---|
| `FlyBehavior.cs` | Bird | Reads Space/Left-click. First press starts the game and re-enables gravity; every press applies upward velocity, rotates bird by vertical speed, plays flap sound. On collision, plays collision sound and calls `GameManager.instance.GameOver()`. Disabled once game-over. |
| `GameManager.cs` | GameManager (singleton) | Owns `GameState`. `StartGame()` swaps start-screen canvas for score canvas and starts the pipe spawner. `GameOver()` shows game-over canvas and freezes time. `RestartGame()` reloads active scene. |
| `PipeSpawner.cs` | Spawner | No longer auto-spawns on scene load — `StartSpawning()` (called by `GameManager.StartGame()`) kicks off `InvokeRepeating` on `_spawnInterval`, randomizing vertical spawn offset between `_minHeightOffset`/`_maxHeightOffset`. Each pipe self-destructs after `_pipeLifetime`. |
| `MovePipe.cs` | Pipe prefab | Moves pipe left at constant speed. |
| `PipeIncreaseScore.cs` | Pipe's scoring trigger | On trigger enter by tagged `"Player"`, calls `Score.instance.UpdateScore()`. |
| `Score.cs` | Score UI (singleton) | Tracks current score, updates TMP text, persists high score via `PlayerPrefs`. |
| `Paralax.cs` | Background / Ground | Scrolls by shifting `MeshRenderer.material.mainTextureOffset` over time (`animationSpeed`) — infinite tiled scroll, no size/width tuning needed. Runs independent of `GameState`, so it keeps scrolling on the start screen too. |
| `LogoFloat.cs` | Start-screen logo (UI) | Bobs the logo up/down in a sine loop via `RectTransform.anchoredPosition` (`_amplitude`, `_speed`). |

## Scene

`Assets/Scenes/SampleScene.unity` — main gameplay scene. Three UI canvases: start-screen (logo + tap prompt), score, and game-over — swapped via `GameManager`.

## Audio

Bird's `AudioSource` plays `_flapClip` on flap input and `_collisionClip` on collision, both muted implicitly once game-over (input guarded in `FlyBehavior`).

## Repo

Remote: https://github.com/jjlimin/myFlappyBird.git (read-only reference for now).

## Status

Working: start screen, parallax background/ground, flap + collision sound, scoring/high score, game-over/restart flow.
