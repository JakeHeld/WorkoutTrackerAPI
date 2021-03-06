﻿using FluentValidation;
using Newtonsoft.Json;
using StarterProject.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterProject.Api.Features.Routines.Dtos
{
    public class RoutineDeleteDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }
    }

    public class RoutineDeleteDtoValidator : AbstractValidator<RoutineDeleteDto>
    {
        public RoutineDeleteDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(x => dataContext.Users.Any(y => y.Id == x))
                .WithMessage("'User' does not exist.");

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(id => dataContext.Routines.Any(x => x.Id == id))
                .WithMessage("'Routine' does not exist.")
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .Must(dto => dataContext.Routines.Any(routine => routine.Id == dto.Id && routine.UserId == dto.UserId))
                        .WithMessage("Unable to delete a 'Routine' you do not own.");
                });
        }
    }
}
