# Gravity Shift

A 2D endless-runner built in Unity as part of the *Create With Code* learning track. The camera scrolls forward on its own and steadily speeds up — survive as long as you can by flipping gravity to land on procedurally spawned platforms, grabbing coins for bonus score, and avoiding spikes and the edges of the screen.

## Gameplay

- The **camera auto-scrolls right** and accelerates the longer you survive, so the pressure ramps up over time.
- **Flip gravity** to jump between platforms above and below you.
- **Collect coins** for bonus points (each coin is worth 5× a second of survival).
- **Instant death** — there are no lives. Touching a spike, or leaving the camera's view (falling off the left edge, top, or bottom), ends the run immediately with an explosion.
- Your score is a single combined number: `survival time + coins × 5`.

### Controls

| Action | Input |
| --- | --- |
| Move left / right | A / D or ← / → (Horizontal axis) |
| Flip gravity | Spacebar |

## How it works

The game is driven by a handful of focused scripts in `Assets/Scripts`, each with a single responsibility.

### Core / state
- **`GameManager`** — a singleton (`GameManager.Instance`) that owns the game's state: whether the game is over, the survival timer, the coin count, and the combined score. `GameOver()` is the single chokepoint for death — it shows the UI, spawns the explosion at the player, and destroys the player. Everything that can kill the player calls this one method.

### Player & camera
- **`gravityShift`** — player controller. Handles horizontal movement and the gravity flip (flips `gravityScale`, rotates the player 180°, and applies a short cooldown so it can't be spammed). The player's move speed scales with score so it can keep up with the accelerating camera.
- **`CameraScroll`** — moves the camera right every frame and ramps `scrollSpeed` based on the current score. Stops scrolling once the game is over.
- **`BoundaryCheck`** — lives on the camera and ends the game if the player leaves the visible area, using the camera's real orthographic size (half-width / half-height) plus a grace buffer rather than hard-coded numbers.

### Hazards & rewards
- **`HazardBehaviour`** — on spikes/enemies. If the player collides with it, it calls `GameManager.GameOver()`.
- **`TargetBehaviour`** — on coins. When the player collides, it adds to the score and destroys itself.

### Procedural platforms
- **`PlatformSpawner`** — distance-based spawner. Each time the camera travels a set gap, it instantiates a platform just off the right edge of the screen at a random height. Spawning is anchored to the camera's real view edge and seeded from the last hand-placed starting platform so the world flows seamlessly from designed into random.
- **`PlatformCleanup`** — on the platform prefab. Each platform destroys itself once it's far enough behind the camera, so dead platforms don't pile up.
- **`PlatformDecorator`** — on the platform prefab. Rolls independent dice to optionally place a spike and/or a coin on the platform. High platforms place items on their underside (with the sprite flipped) for flipped-gravity play; low platforms place them on top. A retry loop keeps a coin from spawning too close to a spike.

### UI
- **`UiManager`** — a singleton that owns all UI: the live score readout during play, and the Game Over panel (hidden until death) showing the final score. Also provides `RestartGame()`, which reloads the active scene.

## Key design decisions

- **No lives — instant death.** Simpler and tenser; one mistake ends the run.
- **Score stored as separate parts** (survival time and coin count), combined only when displayed. Keeps the door open for reweighting, separate stats, and achievements later.
- **Difficulty scales with score, not just time.** Both the camera and player speed are computed as `baseSpeed + score × factor` (assignment, not accumulation, to avoid exponential runaway). The camera and player keep separate base speeds so the player always stays a step ahead.
- **Separation of concerns.** Each script does one thing; behaviour that belongs to an object (cleanup, decoration) lives on that object's prefab so every spawned copy carries it automatically.

## Project setup notes

- Unity 2D project. Built originally from a 3D template, so the **2D Sprite** (`com.unity.2d.sprite`) and **Unity UI** (`com.unity.ugui`, which includes TextMeshPro) packages were added manually.
- UI uses the classic **uGUI** Canvas system with TextMeshPro for text.

## Roadmap

Planned next features:

- [ ] Persistent **high score** (`PlayerPrefs`)
- [ ] **Main menu** scene
- [ ] In-game **pause menu** (Continue / Restart / Quit)
- [ ] Support for a **second spike** on a single platform
- [ ] **Sprites and a background** art pass
