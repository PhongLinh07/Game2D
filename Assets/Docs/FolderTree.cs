//using System.Diagnostics.Tracing;
//using Unity.VisualScripting;
//using UnityEditor.PackageManager;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//Assets /
//│
//├── _Project /
//│   ├── Core /
//│   │   ├── BaseClasses /
//│   │   │   -EntityBase.cs
//│   │   │   -ManagerBase.cs
//│   │   │   -Singleton.cs
//│   │   ├── Managers /
//│   │   │   -GameManager.cs
//│   │   │   -AudioManager.cs
//│   │   │   -InputManager.cs
//│   │   │   -SceneManagerEX.cs
//│   │   ├── Helpers /
//│   │   │   -MathHelper.cs
//│   │   │   -TransformExtension.cs
//│   │   │   -CoroutineRunner.cs
//│   │   ├── Events /
//│   │   │   -GameEvent.cs
//│   │   │   -EventBus.cs
//│   │   │   -EventListener.cs
//│   │   └── Pooling /
//│   │       -ObjectPool.cs
//│   │       -Poolable.cs
//│
//├── Modules /
//│   ├── Entities /
//│   │   ├── Dynamic /
//│   │   │   ├── Player /
//│   │   │   │   ├── Scripts /
//│   │   │   │   │   -PlayerController.cs
//│   │   │   │   │   -PlayerStats.cs
//│   │   │   │   │   -PlayerInventory.cs
//│   │   │   │   │   -PlayerAnimationHandler.cs
//│   │   │   │   ├── Prefabs /
//│   │   │   │   │   -Player.prefab
//│   │   │   │   ├── Sprites /
//│   │   │   │   │   -player_idle.png
//│   │   │   │   │   -player_run.png
//│   │   │   │   └── Data /
//│   │   │   │       -PlayerStats.asset
//│   │   │   ├── Enemy /
//│   │   │   │   ├── Scripts /
//│   │   │   │   │   -EnemyController.cs
//│   │   │   │   │   -EnemyAI.cs
//│   │   │   │   ├── Prefabs /
//│   │   │   │   │   -Chick.prefab
//│   │   │   │   │   - Boa.prefab
//│   │   │   │   │   - Orc.prefab
//│   │   │   │   ├── Sprites/
//│   │   │   │   │   - chick_idle.png
//│   │   │   │   │   - boa_idle.png
//│   │   │   │   │   - orc_idle.png
//│   │   │   │   └── Data/
//│   │   │   │       - ChickData.asset
//│   │   │   │       - BoaData.asset
//│   │   │   │       - OrcData.asset
//│   │   │   ├── NPC/
//│   │   │   │   ├── Scripts/
//│   │   │   │   │   - QuestGiver.cs
//│   │   │   │   └── Prefabs/
//│   │   │   │       - QuestGiver.prefab
//│   │   │   └── Pet/
//│   │   │       ├── Scripts/
//│   │   │       │   - PetFollow.cs
//│   │   │       └── Prefabs/
//│   │   │           - Dog.prefab
//│   │   │           - Dragon.prefab
//│   │   │
//│   │   └── Static/
//│   │       └── Props/
//│   │           ├── Prefabs/
//│   │           │   - Tree.prefab
//│   │           │   - Rock.prefab
//│   │           ├── Sprites/
//│   │           │   - tree.png
//│   │           │   - rock.png
//│   │           └── Scripts/
//│   │               - InteractableProp.cs
//│
//│   ├── World/
//│   │   ├── Maps/
//│   │   │   ├── Scenes/
//│   │   │   │   - Village.unity
//│   │   │   │   - Dungeon01.unity
//│   │   │   │   - Castle.unity
//│   │   │   ├── Tilemaps/
//│   │   │   │   - VillageTilemap.asset
//│   │   │   │   - Dungeon01Tilemap.asset
//│   │   │   └── Data/
//│   │   │       - VillageData.asset
//│   │   │       - Dungeon01Data.asset
//│   │   │       - CastleData.asset
//│   │   ├── EnvironmentEffects/
//│   │   │   - Rain.prefab
//│   │   │   - Fog.prefab
//│   │   │   - Fireflies.prefab
//│   │   └── Audio/
//│   │       - ambient_forest.wav
//│   │       - bgm_village.mp3
//│
//│   ├── Item/
//│   │   ├── Equipment/
//│   │   │   ├── Weapons/
//│   │   │   │   - Sword.prefab
//│   │   │   │   - Axe.prefab
//│   │   │   │   - Bow.prefab
//│   │   │   ├── Armor/
//│   │   │   │   - Helmet.prefab
//│   │   │   │   - Chestplate.prefab
//│   │   │   └── Data/
//│   │   │       - SwordData.asset
//│   │   │       - ArmorData.asset
//│   │   ├── Consumable/
//│   │   │   - Potion.prefab
//│   │   │   - Scroll.prefab
//│   │   └── Material/
//│   │       - Wood.prefab
//│   │       - Iron.prefab
//│
//│   └── Systems/
//│       ├── CombatSystem/
//│       │   ├── Scripts/
//│       │   │   - DamageHandler.cs
//│       │   │   - Hitbox.cs
//│       │   └── Data/
//│       │       - PhysicalDamage.asset
//│       │       - FireDamage.asset
//│       └── InventorySystem/
//│           ├── Scripts/
//│           │   - InventoryLogic.cs
//│           └── Data/
//│               - InventoryConfig.asset
//│
//├── UI/
//│   ├── Inventory/
//│   │   ├── Scripts/
//│   │   │   - InventoryUI.cs
//│   │   ├── Prefabs/
//│   │   │   - InventoryPanel.prefab
//│   │   ├── Sprites/
//│   │   │   - inventory_slot.png
//│   │   └── Data/
//│   │       - InventoryLayout.asset
//│   └── HUD/
//│       ├── Scripts/
//│       │   - HealthBar.cs
//│       └── Prefabs/
//│           - HUDPanel.prefab
//│
//└── Scenes/
//    - Boot.unity
//    - MainMenu.unity
//    - Login.unity
//    - Loading.unity
//    - Credits.unity
