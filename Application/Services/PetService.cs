using Application.Interfaces;
using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PetService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Add(Pet pet)
        {
            _unitOfWork.GetRepository<Pet>().Insert(pet);
            var success = await _unitOfWork.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteById(int id)
        {
            var currentPet = GetById(id);

            if (currentPet == null) return false;

            _unitOfWork.GetRepository<Pet>().Delete(currentPet);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public List<Pet> GetAll()
        {
            var pets = _unitOfWork.GetRepository<Pet>().GetPagedList().Items.ToList();

            return pets;
        }

        public Pet? GetById(int id)
        {
            var petFounded = _unitOfWork.GetRepository<Pet>().GetFirstOrDefault(predicate: x => x.Id == id);

            return petFounded;
        }

        public async Task<bool> Update(Pet pet, int idPet)
        {
            var currentPet = _unitOfWork.GetRepository<Pet>().GetFirstOrDefault(predicate: x => x.Id == idPet);

            if (currentPet == null) return false;

            currentPet = pet;
            currentPet.Id = idPet;

            _unitOfWork.GetRepository<Pet>().Update(currentPet);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
