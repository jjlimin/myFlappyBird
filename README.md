# Flappy Bird Clone (Unity)
A 2D Flappy Bird clone built as a course assignment for **Game Development with Unity**. The project focuses on core 2D gameplay mechanics, state-driven game loops, parallax background rendering, and dynamic physics-based controls.

<p align="center">
  <img width="500" height="426" alt="giphy" src="https://github.com/user-attachments/assets/364fcad9-d68a-4e9b-ac01-7225e0028e86" />
</p>

---

## Tech Stack & Engine

* **Engine:** Unity 6 (`6000.3.20f1`)
* **Render Pipeline:** Universal Render Pipeline (URP 2D)

---

## Controls

* **`Space`** or **`Left Mouse Click`**: Flap (applies upward impulse and tilts the bird). The initial input also transitions the game from the start screen into active gameplay.

---

## Game Loop & State Management

The core gameplay flow is managed by a centralized state machine via `GameManager.State` (`GameState` enum):

* **`NotStarted`**  
  Displays the main start screen (animated floating logo and tap prompt). The bird hovers in place with gravity disabled, the background and ground scroll via seamless parallax, and pipe spawning remains inactive.
* **`Playing`**  
  The first flap input triggers `GameManager.StartGame()`, which transitions UI canvases, displays the active score counter, enables bird gravity, and starts the pipe spawner.
* **`GameOver`**  
  Triggered when the bird collides with obstacles or the ground. The game triggers a brief camera shake, reveals the Game Over screen, freezes time (`Time.timeScale = 0`), and locks player input and audio triggers. Pressing restart reloads the active scene.

---

## Architecture & Scripts (`Assets/Scripts`)

| Script | Attached To | Description |
|---|---|---|
| `FlyBehavior.cs` | Bird | Handles player input, applies upward impulse, tilts the sprite relative to vertical velocity, and triggers flap/collision audio. Alerts `GameManager` on collision and disables input post-game. |
| `GameManager.cs` | GameManager | Singleton controller managing `GameState`. Coordinates UI canvas transitions, triggers spawning routines, freezes time on death, and handles scene reloads. |
| `PipeSpawner.cs` | Spawner | Spawns pipe obstacles at randomized vertical offsets between `_minHeightOffset` and `_maxHeightOffset`. Spawning begins on game start; spawn X is recalculated every spawn from the camera's right edge (`orthographicSize * aspect` + `_spawnMargin`), so it stays correct across any aspect ratio. |
| `MovePipe.cs` | Pipe Prefab | Translates spawned pipe obstacles horizontally to the left at a constant speed, then self-destroys once it passes the camera's left edge (`_destroyMargin`). |
| `PipeIncreaseScore.cs` | Pipe Score Trigger | Trigger collider attached to pipe gaps. Detects the `Player` tag and updates the score counter. |
| `Score.cs` | Score Canvas | Singleton managing real-time score display with TextMeshPro, persisting high scores via `PlayerPrefs`, and playing a score sound (`_scoreClip`) through its own `AudioSource`. |
| `Paralax.cs` | Background / Ground | Shifts `MeshRenderer.material.mainTextureOffset` over time to achieve an infinite, seam-free scrolling effect independent of screen width or game state. |
| `LogoFloat.cs` | Start UI Logo | Creates a smooth vertical floating animation using a sine wave on the UI `RectTransform.anchoredPosition`. |
| `CameraShake.cs` | Main Camera | Singleton that briefly offsets the camera's local position by random jitter (`_magnitude`) over `_duration`, using unscaled time so it still plays through the `Time.timeScale = 0` freeze on death. Triggered from `FlyBehavior` on collision. |

---

## Scene Setup & Audio

* **Scene:** `Assets/Scenes/SampleScene.unity` contains the main environment, physical boundaries (ground + an invisible `Ceiling` collider preventing players from flying over pipes), and three dedicated UI canvases (Start Screen, Score Overlay, and Game Over) swapped at runtime by `GameManager`.
* **Audio:** The bird's `AudioSource` handles flap/collision sounds; `Score` has its own `AudioSource` for the scoring sound. Input guards ensure the bird's audio triggers halt immediately upon death.

---

## Features & Current Status

- [x] Responsive tap/flap physics with dynamic sprite tilt
- [x] Centralized state-driven game loop (`NotStarted` → `Playing` → `GameOver`)
- [x] Infinite parallax scrolling for background and foreground layers
- [x] Procedural pipe spawning with randomized height offsets and auto-cleanup
- [x] Real-time score tracking and persistent high-score saving via `PlayerPrefs`
- [x] Sound effects and animated UI elements (sine-wave logo floating)
- [x] Camera shake on collision/death
- [x] Camera-relative pipe spawn/despawn, correct across all aspect ratios
- [x] Invisible ceiling boundary closing the fly-over-pipe skip exploit
