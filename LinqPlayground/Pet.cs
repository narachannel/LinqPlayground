using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPlayground
{
    public class Pet
    {
        public string Name { get; set; }
        public Animal Animal { get; set; }
        public int OwnerId { get; set; }
        public static List<Pet> GetPets()
        {
            return new List<Pet>()
            {
                new Pet() {Name = "ポチ", Animal = Animal.Dog, OwnerId = 3},
                new Pet() {Name = "タマ", Animal = Animal.Cat, OwnerId = 15},
                new Pet() {Name = "ピー助", Animal = Animal.Bird, OwnerId = 7},
                new Pet() {Name = "ピョン太", Animal = Animal.Rabbit, OwnerId = 12},
                new Pet() {Name = "出目金太郎", Animal = Animal.Fish, OwnerId = 18},
                new Pet() {Name = "トラ", Animal = Animal.Cat, OwnerId = 15},
                new Pet() {Name = "ハチ", Animal = Animal.Dog, OwnerId = 18}
            };
        }
    }
}
