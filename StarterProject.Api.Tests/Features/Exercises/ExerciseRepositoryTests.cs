using NUnit.Framework;
using StarterProject.Api.Features.Exercises;
using StarterProject.Api.Features.Exercises.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterProject.Api.Tests.Features.Exercises
{
    [TestFixture]
    class ExerciseRepositoryTests : BaseTest
    {
        [Test]
        public void CreateExercise_NameHasValue_ReturnsNameThatWasGiven()
        {
            // Arrange
            var name = $"{NextId}";
            var request = new ExerciseCreateDto { Name = name };

            // Act
            var repo = new ExerciseRepository(Context);
            var result = repo.CreateExercise(request);

            // Assert
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public void CreateExercise_NameHasValue_ContextHasNewExerciseWithCorrectName()
        {
            // Arrange
            var name = $"{NextId}";
            var request = new ExerciseCreateDto { Name = name };

            // Act
            var repo = new ExerciseRepository(Context);
            var result = repo.CreateExercise(request);
            var exerciseFromContext = Context.Exercises.Find(result.Id);

            // Assert
            Assert.AreEqual(name, exerciseFromContext.Name);
        }

        [Test]
        public void CreateExercise_MuscleGroupHasValue_ReturnsMuscleGroupThatWasGiven()
        {
            // Arrange
            var muscleGroup = $"{NextId}";
            var request = new ExerciseCreateDto {MuscleGroup = muscleGroup};

            // Act
            var repo = new ExerciseRepository(Context);
            var result = repo.CreateExercise(request);
            var exerciseFromContext = Context.Exercises.Find(result.Id);

            // Assert
            Assert.AreEqual(muscleGroup, exerciseFromContext.MuscleGroup);

        }
        [Test]
        public void CreateExercise_MuscleGroupHasValue_ContextHasNewExerciseWithCorrectMuscleGroup()
        {
            // Arrange
            var muscleGroup = $"{NextId}";
            var request = new ExerciseCreateDto { MuscleGroup = muscleGroup };

            // Act
            var repo = new ExerciseRepository(Context);
            var result = repo.CreateExercise(request);
            var exerciseFromContext = Context.Exercises.Find(result.Id);

            // Assert
            Assert.AreEqual(muscleGroup, exerciseFromContext.MuscleGroup);
        }

         [Test]
        public void CreateExercise_UserIdHasValue_ReturnsUserIdThatWasGiven()
        {
            // Arrange
            var UserId = NextId;
            var request = new ExerciseCreateDto {UserId = UserId};

            // Act
            var repo = new ExerciseRepository(Context);
            var result = repo.CreateExercise(request);

            // Assert
            Assert.AreEqual(UserId, result.UserId);
        }

        [Test]
        public void CreateExercise_UserIdHasValue_ContextHasNewExerciseWithCorrectUserId()
        {
            // Arrange
            var UserId = NextId;
            var request = new ExerciseCreateDto {UserId = UserId};

            // Act
            var repo = new ExerciseRepository(Context);
            var result = repo.CreateExercise(request);
            var exerciseFromContext = Context.Exercises.Find(result.Id);

            // Assert
            Assert.AreEqual(UserId, exerciseFromContext.UserId);
        }
    }
}
