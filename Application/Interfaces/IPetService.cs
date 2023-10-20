using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPetService
    {
        public List<Pet> GetAll();
        public Pet? GetById(int id);
        public Task<bool> Add(Pet pet);
        public Task<bool> Update(Pet pet, int idPet);
        public Task<bool> DeleteById(int id);
    }
}
