using StarWars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Core.Data
{
    public interface ICharacterRepository
    {
        Task<Human> GetHuman(int id);
        Task<Droid> GetDroid(int id);
        Task<List<Character>> GetFriends(int id);
        Task<List<Episode>> GetEpisodes(int id);
        Task<Character> GetHero(Episode episode);
        Task<Character> GetHeroOfTheWholeSaga();
    }
}
