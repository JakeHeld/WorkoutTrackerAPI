﻿using NUnit.Framework;
using StarterProject.Api.Data.Entites;
using StarterProject.Api.Features.Equipments.Dtos;
using StarterProject.Api.Features.Users;
using System.Linq;

namespace StarterProject.Api.Tests.Features.Equipments.Dtos
{
    [TestFixture]
    class EquipmentEditDtoTests : BaseTest
    {
        [Test]
        public void Validate_UserIdIsEmpty_ReturnsCorrectErrorMessage()
        {
            var request = new EquipmentEditDto();

            var validator = new EquipmentEditDtoValidator(Context);
            var result = validator.Validate(request);

            var hasCorrectErrorMessage = result.Errors.Any(x =>
                x.ErrorMessage == "'User Id' must not be empty."
                && x.PropertyName == nameof(EquipmentEditDto.UserId));

            Assert.IsTrue(hasCorrectErrorMessage);
        }

        [Test]
        public void Validate_UserIdDoesNotExist_ReturnsCorrectErrorMessage()
        {
            var request = new EquipmentEditDto { UserId = NextId };

            var validator = new EquipmentEditDtoValidator(Context);
            var result = validator.Validate(request);

            var hasCorrectErrorMessage = result.Errors.Any(x =>
                x.ErrorMessage == "The 'User' does not exist."
                && x.PropertyName == nameof(EquipmentEditDto.UserId));

            Assert.IsTrue(hasCorrectErrorMessage);
        }

        [Test]
        public void Validate_EquipmentIdIsEmpty_ReturnsCorrectErrorMessage()
        {
            var request = new EquipmentEditDto();

            var validator = new EquipmentEditDtoValidator(Context);
            var result = validator.Validate(request);

            var hasCorrectErrorMessage = result.Errors.Any(x =>
                x.ErrorMessage == "'Id' must not be empty."
                && x.PropertyName == nameof(EquipmentEditDto.Id));

            Assert.IsTrue(hasCorrectErrorMessage);
        }

        [Test]
        public void Validate_EquipmentIdDoesNotExist_ReturnsCorrectErrorMessage()
        {
            var request = new EquipmentEditDto { Id = NextId };

            var validator = new EquipmentEditDtoValidator(Context);
            var result = validator.Validate(request);

            var hasCorrectErrorMessage = result.Errors.Any(x =>
                x.ErrorMessage == "This 'Equipment' does not exist."
                && x.PropertyName == nameof(EquipmentEditDto.Id));

            Assert.IsTrue(hasCorrectErrorMessage);
        }

        [Test]
        public void Validate_UnableToAccessEquipmentNotOwned_ReturnsCorrectErrorMessage()
        {
            var user1 = new User { Id = NextId };
            var user2 = new User { Id = NextId };

            Context.Users.Add(user1);
            Context.Users.Add(user2);

            var equipment = new Equipment { Id = NextId, UserId = user1.Id };
            Context.Equipments.Add(equipment);
            Context.SaveChanges();

            var equipmentEditDto = new EquipmentEditDto { Id = equipment.Id, UserId = user2.Id };
            var validator = new EquipmentEditDtoValidator(Context);
            var result = validator.Validate(equipmentEditDto);

            var hasErrorMessage = result.Errors.Any(x =>
                x.ErrorMessage == "You cannot edit an equipment that you do not own."
                && x.PropertyName == "");

            Assert.IsTrue(hasErrorMessage);
        }

        [Test]
        public void Validate_EverythingIsWorking_ReturnsNoErrorMessage()
        {
            var request = new EquipmentEditDto { UserId = NextId, Id = NextId, Name = $"{NextId}" };

            var validator = new EquipmentEditDtoValidator(Context);
            var result = validator.Validate(request);

            var hasCorrectErrorMessage = result.Errors.Any(x =>
                x.ErrorMessage == "'Name' must not be empty."
                && x.PropertyName == "");

            Assert.IsTrue(!hasCorrectErrorMessage);
        }
    }
}
