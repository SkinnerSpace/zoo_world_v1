# Zoo World

A small simulation of animals living on a field. Animals spawn over time, wander around, and interact when they touch each other.

- **Frog** (prey) jumps in a random direction every few seconds.
- **Snake** (predator) moves in a straight line, changing direction from time to time.
- A predator eats any animal it touches. When two predators meet, the one with the better attack angle wins. The winner shows a "Tasty!" label.
- Two prey bounce off each other.
- Animals that leave the screen turn back immediately.
- The counter in the top right corner shows how many prey and predators have died.

## Requirements

- Unity **6000.3.12f1**
- URP, uGUI, Localization, Test Framework
- Extenject for dependency injection
- R3 for events and reactive values

## Flexibility

Adding a new animal requires no new code: create a prefab from existing components, add an `AnimalDefinitionSO`, and register it in `GameInstaller` and the spawner's table. A jumping predator, for example, is just `JumpMovement` + `PredatorInteractionBehavior`.
