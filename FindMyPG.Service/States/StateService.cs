using FindMyPG.Core.Data;
using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FindMyPG.Service.States
{
    public class StateService : IStateService
    {
        private readonly IRepository<State> _stateRepository;
        public StateService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public List<State> GetAllActiveStates()
        {
            return _stateRepository.Table.
                Where(a => a.IsActive)
                .ToList();
        }

        public List<State> GetAllStates()
        {
            return _stateRepository.Table.ToList();
        }

        public State GetStateById(int id)
        {
            return _stateRepository.GetById(id);
        }

        public State GetStateByName(string name)
        {
            return _stateRepository.Table
                .Where(s => s.Name.ToLower() == name.ToLower())
                .FirstOrDefault();
        }

        public void InsertState(State state)
        {
            _stateRepository.Insert(state);
        }

        public void InsertStates(List<State> states)
        {
            _stateRepository.Insert(states);
        }

        public void UpdateState(State state)
        {
            _stateRepository.Update(state);
        }
    }
}
