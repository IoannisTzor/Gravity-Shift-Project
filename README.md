# Gravity Shift
 
> A 2D gravity-flipping endless runner built in Unity (C#). **In active development.**
 
Survive an auto-scrolling, ever-accelerating course by flipping gravity to land on procedurally spawned platforms, grabbing coins for bonus score, and avoiding spikes and the edges of the screen.
 
<!-- TODO: add a gameplay GIF or screenshot here once the art pass is done — this is the single highest-impact thing you can add.
Example:
![Gravity Shift gameplay](docs/gameplay.gif)
-->
*A gameplay GIF will go here once the art pass is complete.*
 
<!-- TODO: once you've made a WebGL build and uploaded it to itch.io, link it here so people can play in-browser:
**[▶ Play in browser](https://your-itch-page.itch.io/gravity-shift)**
-->
 
---
 
## Gameplay
 
- The **camera auto-scrolls right** and accelerates the longer you survive, so the pressure ramps up over time.
- **Flip gravity** to jump between platforms above and below you.
- **Collect coins** for bonus points — grabbing them in a row builds a **combo multiplier**, but let one scroll past and the streak resets.
- **Instant death** — there are no lives. Touching a spike, or leaving the camera's view (falling off the left edge, top, or bottom), ends the run immediately with an explosion.
- **Forced-flip zones** — scrolling bands that flip your gravity for you as you pass through, throwing off your rhythm.
- **It gets harder as you go** — spikes (including double-sided ones) and flip zones grow more frequent the higher your score climbs.
- Your score is a single combined number: `survival time + coins × 5`.
### Controls
 
| Action | Input |
| --- | --- |
| Move left / right | A / D or ← / → (Horizontal axis) |
| Flip gravity | Spacebar |
 
---
 
## How to Run
 
This is a Unity project, so it currently runs from the editor:
 
1. Install **Unity 6000.0.10f1** (Unity 6) via Unity Hub.
2. Clone or download this repository.
3. In Unity Hub, click **Add → Add project from disk** and select the `ClassProject` folder.
4. Open the project, open the main scene in `Assets/Scenes`, and press **Play**.
> A standalone/WebGL build is planned so the game can be played without the editor — see the Roadmap below.
 
---
 
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
- **`TargetBehaviour`** — on coins. Collecting one adds to the score and builds a **combo multiplier**; letting a coin scroll off-screen uncollected resets the combo (a collected coin is destroyed on pickup, so it can never trigger the miss-check).
- **`ForcedFlipZone`** — on a trigger volume (a visible band the player can see coming). When the player enters, it calls the player's `FlipGravity()`, forcing a gravity flip whether they like it or not.
### Procedural platforms
- **`PlatformSpawner`** — distance-based spawner. Each time the camera travels a set gap, it instantiates a platform just off the right edge of the screen at a random height. Spawning is anchored to the camera's real view edge and seeded from the last hand-placed starting platform so the world flows seamlessly from designed into random.
- **`PlatformCleanup`** — on the platform prefab. Each platform destroys itself once it's far enough behind the camera, so dead platforms don't pile up.
- **`PlatformDecorator`** — on the platform prefab. Splits the platform into evenly-spaced **slots** and claims a distinct slot per object (so overlap is structurally impossible — no distance checks). Rolls dice to place up to two spikes and a coin; high platforms place items on the underside (sprite flipped) for flipped-gravity play, low platforms on top. With some chance it **mirrors** the same number of spikes onto the opposite surface (forcing flips to thread through). Spike/mirror chances ramp with score via a clamped offset.
- **`ForcedFlipZoneSpawner`** — distance-based like the platform spawner but on a much larger gap, spawning full-height flip zones occasionally. Each opportunity rolls a score-ramped chance, so zones grow more frequent as the run goes on. Zones reuse `PlatformCleanup` to self-destruct behind the camera.
### UI
- **`UiManager`** — a singleton that owns all UI: the live score readout during play, and the Game Over panel (hidden until death) showing the final score. Also provides `RestartGame()`, which reloads the active scene.
---
 
## Key design decisions
 
- **No lives — instant death.** Simpler and tenser; one mistake ends the run.
- **Score stored as separate parts** (survival time and coin count), combined only when displayed. Keeps the door open for reweighting, separate stats, and achievements later.
- **Difficulty scales with score, not just time.** Both the camera and player speed are computed as `baseSpeed + score × factor` (assignment, not accumulation, to avoid exponential runaway). The camera and player keep separate base speeds so the player always stays a step ahead.
- **Separation of concerns.** Each script does one thing; behaviour that belongs to an object (cleanup, decoration) lives on that object's prefab so every spawned copy carries it automatically.
---
 
## Project setup notes
 
- **Unity 6 (6000.0.10f1)**, 2D project. Built originally from a 3D template, so the **2D Sprite** (`com.unity.2d.sprite`) and **Unity UI** (`com.unity.ugui`, which includes TextMeshPro) packages were added manually.
- UI uses the classic **uGUI** Canvas system with TextMeshPro for text.
---
 
## Roadmap
 
Planned next features:
 
- [x] Persistent **high score** (`PlayerPrefs`)
- [x] **Main menu** scene
- [x] In-game **pause menu** (Continue / Restart / Quit)
- [x] Support for a **second spike** on a single platform (slot system)
- [x] **Ceiling + floor hazards** — chance to mirror spikes onto the opposite surface, forcing gravity flips to thread through
- [x] **Forced-flip zones** — trigger areas that flip the player's gravity automatically
- [x] **Coin combo multiplier** — consecutive coin pickups ramp a score multiplier (resets if you let a coin scroll past)
- [ ] **Sprites and a parallax background** art pass
- [ ] **WebGL build** hosted on itch.io for in-browser play
- [ ] *(post-launch)* **Coin shop / unlockable skins** — spend banked coins on cosmetics
- [ ] *(post-launch)* **Achievements / missions** — e.g. survive 60s, flip 30 times, collect 50 coins
---
 
## Background
 
This project began as a class exercise during Unity's *Create With Code* learning track. I took an earlier version and reworked the way it plays — moving to a gravity-flip endless-runner format — to turn it into something more fun and portfolio-worthy. Development is ongoing.
