using StarWars.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Data.EntityFramework
{
    public static class Seed
    {
        public static void Run(StarWarsContext context)
        {
            var newhope = Episode.NewHope;
            var empire = Episode.Empire;
            var jedi = Episode.Jedi;
            // episodes
            if (!context.Episodes.Any())
            {
                context.Episodes.AddRange(new[] { newhope, empire, jedi });
                context.SaveChanges();
            }

            // planets
            var tatooine = new Planet { Id = 1, Name = "Tatooine" };
            var alderaan = new Planet { Id = 2, Name = "Alderaan" };
            var planets = new List<Planet>
            {
                tatooine,
                alderaan
            };
            if (!context.Planets.Any())
            {
                context.Planets.AddRange(planets);
                context.SaveChanges();
            }

            // humans
            var luke = new Human
            {
                Id = 1000,
                Name = "Luke Skywalker",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                HomePlanet = tatooine
            };
            var vader = new Human
            {
                Id = 1001,
                Name = "Darth Vader",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                HomePlanet = tatooine
            };
            var han = new Human
            {
                Id = 1002,
                Name = "Han Solo",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                HomePlanet = tatooine
            };
            var leia = new Human
            {
                Id = 1003,
                Name = "Leia Organa",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                HomePlanet = alderaan
            };
            var tarkin = new Human
            {
                Id = 1004,
                Name = "Wilhuff Tarkin",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope }
                },
            };
            var humans = new List<Human>
            {
                luke,
                vader,
                han,
                leia,
                tarkin
            };
            if (!context.Humans.Any())
            {
                context.Humans.AddRange(humans);
                context.SaveChanges();
            }

            // droids
            var threepio = new Droid
            {
                Id = 2000,
                Name = "C-3PO",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                PrimaryFunction = "Protocol"
            };
            var artoo = new Droid
            {
                Id = 2001,
                Name = "R2-D2",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                PrimaryFunction = "Astromech"
            };
            var droids = new List<Droid>
            {
                threepio,
                artoo
            };
            if (!context.Droids.Any())
            {
                context.Droids.AddRange(droids);
                context.SaveChanges();
            }

            // update character's friends
            luke.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = threepio },
                new CharacterFriend { Friend = artoo }
            };
            vader.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = tarkin }
            };
            han.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = artoo }
            };
            leia.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = threepio },
                new CharacterFriend { Friend = artoo }
            };
            tarkin.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = vader }
            };
            threepio.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = artoo }
            };
            artoo.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia }
            };
            var characters = new List<Character>
            {
                luke,
                vader,
                han,
                leia,
                tarkin,
                threepio,
                artoo
            };

            if (!context.CharacterFriends.Any())
            {
                context.Characters.UpdateRange(characters);
            }

            newhope.Hero = artoo;
            empire.Hero = luke;
            jedi.Hero = artoo;

            context.SaveChanges();
        }
    }
}
