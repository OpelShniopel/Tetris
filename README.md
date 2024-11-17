# Unity Tetris Game

A modern implementation of the classic Tetris game built with Unity, featuring multiple game modes and customizable difficulty settings.

## Features

- **Multiple Game Modes**:
  - Classic Endless Mode
  - Life Mode (with health system)
  - Multiplayer Mode
  - Custom difficulty settings

- **Game Mechanics**:
  - Ghost piece preview
  - Score tracking system
  - High score persistence
  - Different colored tetrominoes
  - Smooth piece rotation and movement
  - Line clearing animations

- **Audio Features**:
  - Background music
  - Sound effects for various actions
  - Audio management system

- **UI Features**:
  - Clean, modern interface
  - Pause menu functionality
  - Game over screen
  - Score display
  - Health bar system (Life Mode)
  - High score tracking

## Technical Details

### Project Structure

- `Assets/Scripts/`:
  - `Audio/`: Audio management system
  - `Core/`: Core game mechanics and managers
  - `Pieces/`: Tetromino implementations
  - `UI/`: User interface components
  - `Utilities/`: Helper classes and utilities

### Key Components

- **Board System**: Handles game grid and piece placement
- **Piece System**: Manages tetromino movement and rotation
- **Score System**: Tracks points and high scores
- **Difficulty System**: Adjusts game speed and complexity
- **Audio System**: Manages sound effects and background music

## Requirements

- Unity 2021.3.20f1 or later
- Platform support:
  - Windows
  - WebGL

## Installation

1. Clone the repository
2. Open the project in Unity
3. Ensure TextMeshPro is properly imported
4. Build and run the game

## Controls

- Left/Right Arrow: Move piece horizontally
- Down Arrow: Soft drop
- Up Arrow: Rotate piece
- Space: Hard drop
- Escape: Pause game

## Development

The project uses the following Unity packages:
- TextMeshPro for UI text rendering
- Unity's 2D features
- Built-in audio system
