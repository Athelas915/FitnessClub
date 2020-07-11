using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public abstract class ViewModel<T> where T : DataEntity, new()
    {
        protected readonly T model;
        public T Model { get => model; }
        public ViewModel()
        {
            model = new T();
        }
        public ViewModel(T model)
        {
            this.model = model;
        }
    }
}
