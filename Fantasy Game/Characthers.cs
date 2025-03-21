using System;

namespace Fantasy_Game
{
    internal class Characters
    {
        public abstract class CharacterBase
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Armour { get; set; }
            public int Gold { get; set; }
            public string Story { get; set; }
            public Ability Ability1 { get; set; }
            public Ability Ability2 { get; set; }
            public Ability Ability3 { get; set; }
            public Ability Ability4 { get; set; }
            public int Damage { get; set; }


            protected CharacterBase(string name, int health, int armour, string story)
            {
                Name = name;
                Health = health;
                Armour = armour;
                Gold = 100;
                Story = story;
            }
        }

        public class Warrior : CharacterBase
        {
            public int Stamina { get; set; }

            public Warrior(string name) : base(name, 100, 35, "Once, the Warrior was a noble knight, sworn to protect the kingdom’s borders from the horrors lurking beyond the walls. But that was before the world was consumed by a plague of darkness. One fateful night, the kingdom fell—overrun by demonic forces and cursed creatures. The Warrior's family was slaughtered, and the land was left in ruin. Now, they are but a shadow of their former self, a wandering fighter haunted by the ghosts of those they failed to protect. Their armor, battered and stained with blood, is all that remains of their past life. Driven by guilt and a thirst for vengeance, the Warrior plunges into the dungeons and cursed lands, facing nightmarish creatures to atone for their failures. They fight for redemption, their body forged into steel, unyielding even in the darkest of battles.")
            {
                Stamina = 50;
                Ability1 = new Ability("Normal Strike", "Basic physical attack", 15);
                Ability2 = new Ability("Heal", "Restores some health", 0, 20, isHealing: true,20);
                Ability3 = new Ability("Heavy Strike", "A powerful strike that reduces stamina", 30, 10);
                Ability4 = new Ability("Roar", "Increases damage temporarily, consumes stamina",15, isBuffing: true);
                Gold = 100;
            }
        }

        public class Archer : CharacterBase
        {
            public int Stamina { get; set; }

            public Archer(string name) : base(name, 100, 35, "The Archer was once a member of a secretive order of elven hunters, skilled in tracking and eliminating the monstrous beasts that roamed the deep forests. But when the world was overtaken by darkness, their homeland was swallowed by an ancient curse, leaving only ruins behind. Their people were wiped out, and they alone survived, now wandering the abyssal dungeons and forsaken ruins in search of a way to undo the curse. Silent and deadly, the Archer moves like a shadow, striking with precision from the shadows. The bow they carry was crafted by their ancestors and is imbued with a magic that resonates with the land’s dying will. Their only desire now is vengeance against those responsible for their people's destruction, but they are also haunted by whispers of the ancient forest spirits calling them back to their homeland.")
            {
                Stamina = 50;
                Ability1 = new Ability("Normal Shot", "Basic ranged attack", 15);
                Ability2 = new Ability("Heal", "Restores some health", 0, 20, isHealing: true,20);
                Ability3 = new Ability("Headshot", "A precise shot that reduces stamina", 35, 10);
                Ability4 = new Ability("Focus", "Increases damage temporarily, consumes stamina",15, isBuffing: true);
                Gold = 100;

            }
        }

        public class Mage : CharacterBase
        {
            public int Mana { get; set; }

            public Mage(string name) : base(name, 70, 25, "Once a scholar of the arcane, the Mage’s curiosity led them down paths that no mortal should ever tread. Their desire for forbidden knowledge took them deep into the ancient, cursed dungeons, where they unearthed powers beyond comprehension. What they found, however, was not knowledge but madness. The dark arts they sought twisted their mind and soul, transforming them into something unnatural. Now, they are a being of both great power and terrible insanity, with every spell they cast draining a piece of their humanity. The Mage seeks to undo the horrors they have wrought, but the dungeons they wander are filled with creatures who crave the very same dark powers. They are both feared and hunted, not only by enemies but by the fragments of their own shattered mind. They seek redemption, though they know it may be impossible to escape their own descent into madness.")
            {
                Mana = 100;
                Ability1 = new Ability("Normal Spell", "Basic magic attack", 20);
                Ability2 = new Ability("Heal", "Restores some health", 0, 20, isHealing: true,20);
                Ability3 = new Ability("Fireball", "A fireball attack that consumes mana", 40, 20);
                Ability4 = new Ability("Nirvana", "Increases magic damage temporarily, consumes mana",30, isBuffing: true);
                Gold = 100;

            }
        }

        public class Paladin : CharacterBase
        {
            public int Mana { get; set; }

            public Paladin(string name) : base(name, 120, 30, "The Paladin was once the proud protector of the Holy Citadel, an ancient fortress built to defend against the forces of darkness that plague the world. Their order was one of the last strongholds of light in a world swallowed by shadow. But the citadel fell, and with it, the last hope of salvation. The Paladin’s comrades were slain, and the once-glorious halls were consumed by demonic corruption. However, the Paladin survived, though broken and weary, their heart torn by the loss of their sacred home. Bearing the weight of their fallen order, they now journey through the cursed dungeons and darkened lands, hoping to find remnants of the Light and to banish the darkness once and for all. Their shield and sword are the only things left that stand between the world and utter destruction, and they are prepared to die in the depths, if it means bringing even a glimmer of hope to the world once more.")
            {
                Mana = 80;
                Ability1 = new Ability("Blade of Justice", "Basic physical attack", 15);
                Ability2 = new Ability("Heal", "Restores some health", 0, 20, isHealing: true,20);
                Ability3 = new Ability("Judgement", "A powerful holy strike that consumes mana", 50, 25);
                Ability4 = new Ability("Battle Cry", "Increases damage temporarily, consumes mana",20, isBuffing: true);
                Gold = 100;

            }
        }

        public class Ability
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Damage { get; set; }
            public int ResourceCost { get; set; }
            public bool IsHealing { get; set; }
            public bool IsBuffing { get; set; }

            public Ability(string name, string description, int damage, int resourceCost = 0, bool isHealing = false, int healAmaount = 0, bool isBuffing = false)
            {
                Name = name;
                Description = description;
                Damage = damage;
                ResourceCost = resourceCost;
                IsHealing = isHealing;
                IsBuffing = isBuffing;
            }
        }

        public class Enemy
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Armour { get; set; }
            public int Damage { get; set; }
            public Ability Ability1 { get; set; }
            public Ability Ability2 { get; set; }

            public Enemy(string name, int health, int armour, int damage, Ability ability1, Ability ability2)
            {
                Name = name;
                Health = health;
                Armour = armour;
                Damage = damage;
                Ability1 = ability1;
                Ability2 = ability2;
            }

            public void enemy_UseAbility(int abilityNumber, out string abilityName, out int abilityDamage)
            {
                if (abilityNumber == 1)
                {
                    abilityName = Ability1.Name;
                    abilityDamage = Ability1.Damage;
                }
                else if (abilityNumber == 2)
                {
                    abilityName = Ability2.Name;
                    abilityDamage = Ability2.Damage;
                }
                else
                {
                    throw new ArgumentException("Invalid ability number!");
                }
            }
        }

        public static class EnemyRepository
        {
            public static Enemy WeakGoblin { get; } = new Enemy(
                "Weak Goblin",
                50,
                10,
                5,
                new Ability("Slash", "Basic attack", 10),
                new Ability("Screech", "", 0)
            );

            public static Enemy AngryOrc { get; } = new Enemy(
                "Angry Orc",
                75,
                15,
                10,
                new Ability("Smash", "Powerful attack", 20),
                new Ability("Roar", "", 0)
            );

            public static Enemy DarkElf { get; } = new Enemy(
                "Dark Elf",
                100,
                20,
                15,
                new Ability("Poison Arrow", "Deals damage over time", 25),
                new Ability("Shadow Strike", "Deals heavy damage", 30)
            );

            public static Enemy CursedKnight { get; } = new Enemy(
                "Cursed Knight",
                150,
                25,
                20,
                new Ability("Shield Bash", "Stuns the enemy", 15),
                new Ability("Dark Slash", "High damage attack", 40)
            );

            public static Enemy AncientDragon { get; } = new Enemy(
                "Ancient Dragon",
                200,
                35,
                30,
                new Ability("Fire Breath", "Burns everything", 50),
                new Ability("Tail Swipe", "Area damage", 40)
            );

            public static Enemy DemonKing { get; } = new Enemy(
                "Demon King",
                300,
                50,
                50,
                new Ability("Hellfire", "Ultimate attack", 100),
                new Ability("Soul Drain", "Steals health", 30)
            );

            public static Enemy GetEnemyByFloor(int floor)
            {
                switch (floor)
                {
                    case 0:
                        return WeakGoblin;
                    case 1:
                        return AngryOrc;
                    case 2:
                        return DarkElf;
                    case 3:
                        return CursedKnight;
                    case 4:
                        return AncientDragon;
                    case 5:
                        return DemonKing;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(floor), "Invalid floor number!");
                }
            }
        }
    }
}
