<h1 align="center">
    <br>
    <img src="bird.png" width="200">
    <br>
    FlappyBird
    <br>
</h1>

<h4 align="center">
    Unity remake of Flappy Bird based on the video from <a href="https://youtu.be/XtQMytORBmM?si=Wf6X1IIDuUTHqoXG" target="_blank">Game Maker's Toolkit</a> Youtube channel.<br>
    A project-based learning: I started from the tutorial and added features to practice and deepen my understanding of C# and Unity.
</h4>

<p align="center">
    This project lets you experience classic Flappy Bird gameplay with new mechanics and strategic choices. Play safe for high scores, or go hardcore to maximize points using shields and special coins, but beware: the game speeds up and gets tougher!
</p>

<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#game-objectives">Game Objectives</a> •
  <a href="#file-structure">File Structure</a> •
  <a href="#credits">Credits</a>
</p>

## Key Features

### Base Features

-   Classic Flappy Bird controls: tap to fly, avoid pipes, score points.
-   Procedural pipe spawning and movement.
-   Score tracking and display.
-   Simple UI for restarting.

### Personal Enhancements

-   **Red Pipes:** Special pipes that, when passed through, trigger increased speed (speed boost). Chance-based appearance after 10 seconds of gameplay.
-   **Shield Power-Up:** Grants 6 seconds of invincibility after collecting 10 coins. Allows passing through pipes without collision.
-   **Coin System:**
    -   Gold Coin (+1 point)
    -   Special Coin (+3 points)
-   **Speed Modes:**
    -   Slow (first 10 seconds)
    -   Normal (after 10 seconds)
    -   Fast (triggered by red pipes or shield)
-   **Audio Management:** Toggle sound effects and background music.
-   **Launcher UI:** GitHub link button with confirmation sound effect when starting the game.
-   **Strategic Gameplay:** Choose between safe play for max score or riskier play for rapid scoring using shields and special coins.

## Game Objectives

-   Survive as long as possible by avoiding pipes.
-   Collect coins to increase your score.
-   Use shields and special coins to maximize points, but beware: activating shields increases game speed and difficulty.
-   Decide your strategy: play safe for longevity or take risks for faster, higher scores.

## File Structure

```
Assets/
├── AudioManager.cs         # Handles all game audio logic
├── BirdScript.cs           # Controls bird movement and input
├── CoinScript.cs           # Manages coin behavior and scoring
├── Launcher.cs             # Main menu and launcher logic
├── LogicScript.cs          # Core game logic (score, game over)
├── OpenGitHubLink.cs       # Opens GitHub repo from launcher
├── PipeMiddleScript.cs     # Detects passing through pipes
├── PipeMoveScript.cs       # Moves pipes across the screen
├── PipeSpawnerScript.cs    # Spawns pipes at intervals
├── SpeakerManager.cs       # Manages speaker UI for sound toggle
```

## Credits

-   **Forest** - SLD Audio ([Unity Asset Store](https://assetstore.unity.com/packages/audio/music/free-casual-relaxing-game-music-pack-262740))
-   **Serenity** - Neko Legends ([Unity Asset Store](https://assetstore.unity.com/packages/audio/music/music-serenity-321727))
-   **Ultra Instinct Theme** - Kraftunes ([Newgrounds](https://www.newgrounds.com/audio/listen/1027982))
