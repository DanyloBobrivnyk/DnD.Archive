using DnD.Archive.Api.Models;

namespace DnD.Archive.Api.Helpers.DB
{
    public static class DnDArchiveInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<DnDArchiveContext>();
                try
                {
                    context.Database.EnsureCreated();

                    if (!context.Specializations.Any() && !context.Skills.Any() && !context.Characters.Any())
                    {
                        // Seed Specializations
                        var specializations = new[]
                        {
                            new Specialization { Name = "Warrior", Description = "Strong fighter, defeats enemies in close combat." },
                            new Specialization { Name = "Mage", Description = "Masters of arcane arts, casting powerful spells." },
                            new Specialization { Name = "Rogue", Description = "Stealthy and dexterous, excels in surprise attacks." },
                            new Specialization { Name = "Healer", Description = "Skilled in healing magic, keeps allies alive." }
                        };

                        context.Specializations.AddRange(specializations);

                        // Seed Skills
                        var skills = new[]
                        {
                            new Skill { Id = Guid.NewGuid(), Name = "Slash", Description = "A powerful sword attack.", Manacost = 10, HealthImpact = -15, AllowedSpecialization = new List<Specialization> { specializations[0] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Fireball", Description = "Throws a fireball at the enemy.", Manacost = 25, HealthImpact = -30, AllowedSpecialization = new List<Specialization> { specializations[1] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Stealth", Description = "Become invisible to enemies.", Manacost = 15, HealthImpact = 0, AllowedSpecialization = new List<Specialization> { specializations[2] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Heal", Description = "Heals an ally.", Manacost = 20, HealthImpact = 25, AllowedSpecialization = new List<Specialization> { specializations[3] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Charge", Description = "Charges at the enemy, dealing damage.", Manacost = 20, HealthImpact = -20, AllowedSpecialization = new List<Specialization> { specializations[0] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Ice Blast", Description = "Casts a blast of ice, freezing the enemy.", Manacost = 30, HealthImpact = -25, AllowedSpecialization = new List<Specialization> { specializations[1] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Backstab", Description = "A critical hit from behind.", Manacost = 20, HealthImpact = -35, AllowedSpecialization = new List<Specialization> { specializations[2] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Revive", Description = "Revives a fallen ally.", Manacost = 50, HealthImpact = 100, AllowedSpecialization = new List<Specialization> { specializations[3] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Berserk", Description = "Increases attack power at the cost of defense.", Manacost = 30, HealthImpact = 0, AllowedSpecialization = new List<Specialization> { specializations[0] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Lightning Bolt", Description = "Strikes the enemy with a bolt of lightning.", Manacost = 35, HealthImpact = -40, AllowedSpecialization = new List<Specialization> { specializations[1] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Poison Dart", Description = "Shoots a dart that poisons the enemy.", Manacost = 15, HealthImpact = -10, AllowedSpecialization = new List<Specialization> { specializations[2] } },
                            new Skill { Id = Guid.NewGuid(), Name = "Shield", Description = "Creates a protective barrier.", Manacost = 25, HealthImpact = 0, AllowedSpecialization = new List<Specialization> { specializations[3] } }
                        };

                        context.Skills.AddRange(skills);

                        // Seed Characters
                        var characters = new[]
                        {
                            new Character { Id = Guid.NewGuid(), Name = "Aragorn", Bio = "A brave warrior.", Hitpoints = 100, Manapool = 50, SpecializationName = "Warrior", Skills = new List<Skill> { skills[0], skills[4] } },
                            new Character { Id = Guid.NewGuid(), Name = "Gandalf", Bio = "A wise mage.", Hitpoints = 80, Manapool = 150, SpecializationName = "Mage", Skills = new List<Skill> { skills[1], skills[5] } },
                            new Character { Id = Guid.NewGuid(), Name = "Legolas", Bio = "An agile rogue.", Hitpoints = 90, Manapool = 60, SpecializationName = "Rogue", Skills = new List<Skill> { skills[2], skills[6] } },
                            new Character { Id = Guid.NewGuid(), Name = "Elrond", Bio = "A powerful healer.", Hitpoints = 85, Manapool = 100, SpecializationName = "Healer", Skills = new List<Skill> { skills[3], skills[7] } },
                            new Character { Id = Guid.NewGuid(), Name = "Boromir", Bio = "A fierce warrior.", Hitpoints = 110, Manapool = 40, SpecializationName = "Warrior", Skills = new List<Skill> { skills[0], skills[8] } },
                            new Character { Id = Guid.NewGuid(), Name = "Saruman", Bio = "A powerful mage.", Hitpoints = 75, Manapool = 160, SpecializationName = "Mage", Skills = new List<Skill> { skills[1], skills[9] } },
                            new Character { Id = Guid.NewGuid(), Name = "Frodo", Bio = "A stealthy rogue.", Hitpoints = 70, Manapool = 50, SpecializationName = "Rogue", Skills = new List<Skill> { skills[2], skills[10] } },
                            new Character { Id = Guid.NewGuid(), Name = "Arwen", Bio = "A skilled healer.", Hitpoints = 90, Manapool = 120, SpecializationName = "Healer", Skills = new List<Skill> { skills[3], skills[11] } },
                            new Character { Id = Guid.NewGuid(), Name = "Gimli", Bio = "A tough warrior.", Hitpoints = 120, Manapool = 30, SpecializationName = "Warrior", Skills = new List<Skill> { skills[0], skills[4], skills[8] } },
                            new Character { Id = Guid.NewGuid(), Name = "Radagast", Bio = "A nature-loving mage.", Hitpoints = 85, Manapool = 140, SpecializationName = "Mage", Skills = new List<Skill> { skills[1], skills[5], skills[9] } }
                        };

                        context.Characters.AddRange(characters);

                        // Save changes to the database
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    throw new Exception("An error occurred while seeding the database.", ex);
                }
            }
            return app;
        }
    }
}
