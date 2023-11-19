﻿using AutoMapper;
using FS.BLL.Entities;
using FS.BLL.Interfaces;
using FS.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FS.WebAPI.Controllers
{
    [Route("api/Actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private IActorService actorService;
        private IMapper _mapper;
        public ActorsController(IActorService actorService, IMapper mapper)
        {
            this.actorService = actorService;
            _mapper = mapper;
        }

        // GET: api/<ActorsController>
        [HttpGet("{id}")]
        public async Task<ActorViewModel> GetActor(int id)
        {
            var actor = _mapper.Map<Actor, ActorViewModel>(await actorService.GetActor(id));
            return actor;
        }

        // GET api/<ActorsController>/5
        [HttpGet]
        public async Task<List<ActorViewModel>> GetActors()
        {
            var actors = await actorService.GetActors();
            return _mapper.Map<List<Actor>, List<ActorViewModel>>(actors);
        }

        // POST api/<ActorsController>
        [HttpPost]
        public async Task<bool> Post(ActorViewModel actor)
        {
            bool result = await actorService.AddActor(_mapper.Map<ActorViewModel, Actor>(actor));
            return result;
        }

        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, ActorViewModel actor)
        {
            bool result = await actorService.PutActor(id, _mapper.Map<ActorViewModel, Actor>(actor));
            return result;
        }

        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            bool result = await actorService.DeleteActor(id);
            return result;
        }
    }
}
