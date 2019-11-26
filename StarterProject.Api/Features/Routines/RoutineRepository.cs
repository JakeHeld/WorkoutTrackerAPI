using AutoMapper;
using StarterProject.Api.Data;
using StarterProject.Api.Data.Entites;
using StarterProject.Api.Features.Routines.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterProject.Api.Features.Routines
{
    public interface IRoutineRepository
    {
        RoutineGetDto GetRoutine(int routineId);
        List<RoutineGetDto> GetAllRoutines(int userId);
        RoutineGetDto CreateRoutine(RoutineCreateDto routineCreateDto);
        RoutineGetDto EditRoutine(int userId, RoutineEditDto routineUpdateDto);
        void DeleteRoutine(int routineId);
    }

    public class RoutineRepository : IRoutineRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoutineRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public RoutineGetDto CreateRoutine(RoutineCreateDto routineCreateDto)
        {
            var routine = _mapper.Map<Routine>(routineCreateDto);
            _context.Set<Routine>().Add(routine);
            _context.SaveChanges();
            var routineGetDto = _mapper.Map<RoutineGetDto>(routine);
            return routineGetDto;


            /*
            var routine = new Routine
            {
                Name = routineCreateDto.Name,
                UserId = routineCreateDto.UserId
            };

            _context.Set<Routine>().Add(routine);
            _context.SaveChanges();

            var routineGetDto = new RoutineGetDto
            {
                Id = routine.Id,
                Name = routine.Name,
                UserId = routine.UserId
            };*/
            //return routineGetDto;
        }

        public RoutineGetDto GetRoutine(int routineId)
        {
            return _context
                .Set<Routine>()
                .Select(x => new RoutineGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId
                })
                .FirstOrDefault(x => x.Id == routineId);
        }

        public RoutineGetDto EditRoutine(int id, RoutineEditDto routineEditDto)
        {
            var routine = _context.Set<Routine>().Find(id);
            _mapper.Map(routineEditDto, routine);
            //routine.Name = routineEditDto.Name;

            _context.SaveChanges();

            /*
             var routineGetDto = new RoutineGetDto
            {
                Id = routine.Id,
                Name = routine.Name,
                UserId = routine.UserId
            };*/
            return _mapper.Map<RoutineGetDto>(routine);
        }

        public List<RoutineGetDto> GetAllRoutines(int userId)
        {
            return _context
            .Set<Routine>()
            .Where(x => x.UserId == userId)
            .Select(x => new RoutineGetDto
            {
                Id = x.Id,
                Name = x.Name,
                UserId = x.UserId
            })
            .ToList();
        }

        public void DeleteRoutine(int routineId)
        {
            var routine = _context.Set<Routine>().Find(routineId);
            _context.Set<Routine>().Remove(routine);
            _context.SaveChanges();
        }
    }
}
