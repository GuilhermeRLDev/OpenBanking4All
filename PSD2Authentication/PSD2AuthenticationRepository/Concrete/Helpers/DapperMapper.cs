using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSD2AuthenticationRepository.Concrete.Helpers
{
    public class DapperMapper<TParent>
        where TParent : IParent
    {
        private readonly Dictionary<Guid, TParent> _entityDictionary;

        public DapperMapper()
        {
            _entityDictionary = new Dictionary<Guid, TParent>();
        }

        public TParent Mapper<TFirst>(TParent entity, TFirst first)
           where TFirst : IEntity
        {
            return Result.Success(entity)
                .Bind(entityEntry => IsAlreadyInTheDictionary(entity))
                .Tap(entityEntry => entityEntry.AddItem(first))
                .Finally(entityEntry => entityEntry.Value);
        }
        public TParent Mapper<TFirst, TSecond>(TParent entity, TFirst first, TSecond second)
            where TFirst : IEntity
            where TSecond : IEntity
        {
            return Result.Success(entity)
               .Bind(entityEntry => IsAlreadyInTheDictionary(entity))
               .Tap(entityEntry => entityEntry.AddItem(first))
               .Tap(entityEntry => entityEntry.AddItem(second))
               .Finally(entityEntry => entityEntry.Value);
        }
        public TParent Mapper<TFirst, TSecond, TThird>(TParent entity, TFirst first, TSecond second, TThird third)
            where TFirst : IEntity
            where TSecond : IEntity
            where TThird : IEntity
        {
            return Result.Success(entity)
               .Bind(entityEntry => IsAlreadyInTheDictionary(entity))
               .Tap(entityEntry => entityEntry.AddItem(first))
               .Tap(entityEntry => entityEntry.AddItem(second))
               .Tap(entityEntry => entityEntry.AddItem(third))
               .Finally(entityEntry => entityEntry.Value);
        }
        public TParent Mapper<TFirst, TSecond, TThird, TFourth>(TParent entity, TFirst first, TSecond second, TThird third, TFourth fourth)
            where TFirst : IEntity
            where TSecond : IEntity
            where TThird : IEntity
            where TFourth : IEntity
        {
            return Result.Success(entity)
               .Bind(entityEntry => IsAlreadyInTheDictionary(entity))
               .Tap(entityEntry => entityEntry.AddItem(first))
               .Tap(entityEntry => entityEntry.AddItem(second))
               .Tap(entityEntry => entityEntry.AddItem(third))
               .Tap(entityEntry => entityEntry.AddItem(fourth))
               .Finally(entityEntry => entityEntry.Value);
        }
        private Result<TParent> IsAlreadyInTheDictionary(TParent entity)
        {
            TParent entityEntry;
            if (!_entityDictionary.TryGetValue(entity.GetPrimaryKeyValue(), out entityEntry))
            {
                entityEntry = entity;
                _entityDictionary.Add(entity.GetPrimaryKeyValue(), entityEntry);
            }

            return Result.Success(entityEntry);
        }
    }
}
