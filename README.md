# 🎮 Anatomy Game: 3D Cellular Combat Simulation

![Status](https://img.shields.io/badge/Status-Completed-green)
![Engine](https://img.shields.io/badge/Engine-Unity-blue)
![Platform](https://img.shields.io/badge/Platform-Windows/Mac-green)
![Language](https://img.shields.io/badge/Language-C%23-purple)

## 💭 Development Story

Anatomy Game was created during the 2020-2023 pandemic, specifically in the summer of 2022. As the sole programmer and level designer, I partnered with an animator/designer to create a psychedelic and educational experience about the human body. The concept was born from the idea of illustrating, in a cartoonish way, what happens inside the human body in response to infections. The game follows a red blood cell aspiring to become a white blood cell to fight viruses and bacteria in different body systems.

The project was completed with 3 functional levels (heart, bone, and lungs), each with distinctive mechanics. My main technical motivation was to develop a highly flexible and scalable third-person combat system that could easily integrate various enemy types and advanced combat mechanics.

## 📸 Technical Showcase

### Dynamic Character Movement System
![Grabación 2025-05-06 103816 (1)](https://github.com/user-attachments/assets/3c07ac69-31b3-4685-b7d1-0277ed46a764)
- Differentiated ground/air control with dynamic sensitivity adjustments
- Multiple-jump mechanics with configurable physics parameters
- Smooth transition between states with realistic inertia and acceleration

### Advanced Combat Mechanics
![Grabación de pantalla 2025-05-06 104940](https://github.com/user-attachments/assets/5c1256bc-7ced-47af-a0b7-39dcd6c1d725)
- Progressive combo system with escalating power and effects
- Multiple attack types based on weapon state and combo progression
- Real-time damage and knockback effects with physics-based responses

### AI with Visual Debugging

![image](https://github.com/user-attachments/assets/7da72b1b-7ebe-42a5-a73f-0af88b34a9fe)
Enemy AI implementation showcasing:
- Concentric detection zones visualized with color-coded Gizmos
- Different behavior states based on distance to player
- NavMeshAgent pathfinding with dynamic target updating

## ✨ Key Features
- 🧩 **Progressive combat system**: Implementation of combos with sequential counter (AttacksCombo) that triggers special attacks when reaching specific thresholds, using SetFloat("AttackType", n) to select animations.
- 🔧 **Unified character architecture**: Base Character class shared by player and enemies that uses inheritance and polymorphism to maximize code reuse and facilitate the addition of new types.
- 💡 **Contextual information zones**: Interactive sign system that detects player presence through physical triggers and displays information interfaces that pause the game (Time.timeScale = 0).
- 🎯 **Streak and specialization system**: Cumulative bonuses (Streak += StreakMultiplier) based on continuity of attacks and enemy type, encouraging strategic combat.
- 🌐 **Intelligent mobile platforms**: Elevators with stations indexed in Hashtable, bidirectional control, and automatic player binding using SetParent().

## 🔬 Technical Deep Dive

The technical core of the game is a highly modular combat system with separation of responsibilities following SOLID principles. The architecture implements a base Character class shared between player and enemies, allowing both to use the same fundamental systems while maintaining specialized behaviors through inheritance and polymorphism.

The universal interaction system is based on an abstract Interactable class that provides a common interface for all interactive game objects (enemies, platforms, information signs). This allowed implementing a single raycast system from the camera that detects and activates any element in the game world without needing specific code for each type.

Reciprocal proximity detection between player and enemies optimizes performance by avoiding constant checks, establishing bidirectional communication where enemies notify the player when they enter their range (Player.AddEnemyToRange) and vice versa. This architecture allowed implementing efficient area attacks without redundant iterations.

For debugging visualization, a consistent Gizmos system with standardized color codes was implemented to show interaction, attack, and detection radii. This greatly facilitated level design and game balancing by providing immediate visual representation of critical parameters.

## 💻 Core Systems

### Movement and Combat System

```csharp
// Differentiated ground/air control system
Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
float multiplier = InGround() ? 1 : Air_Control;
velocity = ModifiedSmooth(velocity, direction * Max_Speed * multiplier, Acceleration * multiplier);

// Multiple jump mechanic
if (Input.GetButtonDown("Jump") && JumpCount < Allowed_Jumps) {
    float power = JumpCount == 0 ? Jump_Impulse : Jump_Impulse * Double_Jump_Multiplier;
    velocity.y = Mathf.Sqrt(power * Gravity * 2);
    JumpCount++;
}
```

Technical challenges overcome:
- Implementation of custom physics to create a sense of inertia and precise control.
- Integration of the orientation system with the camera to maintain consistent controls regardless of view angle.
- Combo system that scales with continuity and type of enemy attacked, encouraging specific strategies.

### Enemy System

```csharp
// Navigation and player detection system
void Update() {
    float distance = Vector3.Distance(transform.position, Player.transform.position);
    if (distance <= Attack_Radius) {
        Nav.SetDestination(Player.transform.position);
        if (distance <= Interaction_Radius && Delay <= 0) {
            Attack();
            Delay = thisEnemy.Attack_Delay;
        }
    }
    Delay -= Time.deltaTime;
}

// Bidirectional notification
void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
        Player.AddEnemyToRange(thisEnemy);
    }
}
```

Technical challenges overcome:
- Creation of a detection system with multiple behavior zones.
- Implementation of bidirectional communication between player and enemies to optimize performance.
- Encapsulated statistics system that allows easy customization of new enemy types.

### Mobile Platform System

```csharp
// Automatic player binding to platforms
void OnTriggerStay(Collider other) {
    if (other.CompareTag("Player")) {
        other.transform.SetParent(transform);
    }
}

// Elevator system with indexed stations
public void MoveTo(int targetStation) {
    destination = (Vector3)stations[targetStation];
    currentPosition = targetStation;
}

void Update() {
    transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
}
```

Technical challenges overcome:
- Development of a modular system that separates the user interface (panels) from movement logic.
- Implementation of Hashtable to index positions and facilitate bidirectional navigation.
- Dynamic coupling of the character that maintains coherent physics during movement.

## 🌱 Technical Learnings

- Design of highly scalable architectures based on inheritance and polymorphism that maximize code reuse.
- Implementation of universal interaction systems that unify the behavior of diverse game elements.
- Optimization of proximity detection through bidirectional notification between entities.
- Development of visual feedback systems during the design phase through extensive use of Gizmos.
- Implementation of stratified combat mechanics that encourage different play styles through cumulative multipliers.

## 🔮 Future Development

- Expansion of the enemy attribute absorption system to create RPG-like progression.
- Implementation of a visual combo editor that allows designing sequences without programming.
- Scene-based save system to maintain progression between sessions.
- Expansion to new body levels with system-specific mechanics.
- Optimization of the dynamic instantiation system through object pooling to improve performance.

### Basic Controls

|Key/Input|Action|
|---|---|
|WASD|Movement|
|Space|Jump (multiple)|
|Left Click|Basic attack|
|Right Click|Special attack/Interaction|
|E|Interact with objects|
|Tab|Sheath/unsheath weapon|

---

```
  ___                  _                             ______                       
 / _ \                | |                           |  ____|                      
/ /_\ \_ __   __ _ ___| |_ ___  _ __ ___  _   _    | |  __  __ _ _ __ ___   ___ 
|  _  | '_ \ / _` / __| __/ _ \| '_ ` _ \| | | |   | | |_ |/ _` | '_ ` _ \ / _ \
| | | | | | | (_| \__ \ || (_) | | | | | | |_| |   | |__| | (_| | | | | | |  __/
\_| |_/_| |_|\__,_|___/\__\___/|_| |_| |_|\__, |   |_____/ \__,_|_| |_| |_|\___|
                                           __/ |                                 
                                          |___/                                  
```

*Project developed as a technical challenge during the pandemic (2022). An experiment in scalable game architectures and extensible combat systems.*
