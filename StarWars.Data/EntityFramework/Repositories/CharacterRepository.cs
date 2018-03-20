using Microsoft.EntityFrameworkCore;
using StarWars.Core.Data;
using StarWars.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Data.EntityFramework.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StarWarsContext context;

        public CharacterRepository(StarWarsContext context)
        {
            this.context = context;
        }
        public Task<Droid> GetDroid(int id)
        {
            return context.Droids.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Episode>> GetEpisodes(int id)
        {
            return context
                .Characters
                .Include("CharacterEpisodes.Episode")
                .Where(x => x.Id == id)
                .SelectMany(x => x.CharacterEpisodes.Select(y => y.Episode))
                .ToListAsync();
        }

        public Task<List<Character>> GetFriends(int id)
        {
            return context
                .Characters
                .Include("CharacterFriends.Friend")
                .Where(x => x.Id == id)
                .SelectMany(x => x.CharacterFriends.Select(y => y.Friend))
                .ToListAsync();
        }

        public Task<Human> GetHuman(int id)
        {
            return context
                .Humans
                .Include("HomePlanet")
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Character> GetHero(Episode episode)
        {
            var result = await context
                .Episodes
                .Include("Hero")
                .SingleOrDefaultAsync(x => x.Id == episode.Id);

            return result?.Hero;
        }

        public async Task<Character> GetHeroOfTheWholeSaga()
        {
            //var hero = await context
            //    .Characters
            //    .Join(
            //        context.Episodes,
            //        c => c.Id,
            //        e => e.HeroId,
            //        (c, e) => new 
                    
            var heroId = await context
                .Episodes
                .GroupBy(
                    e => e.HeroId,
                    e => e.Title,
                    (key, e) => new
                    {
                        key,
                        count = e.Count()
                    })
                .OrderByDescending(x => x.count)
                .FirstOrDefaultAsync();

            return null;
        }
    }
}
