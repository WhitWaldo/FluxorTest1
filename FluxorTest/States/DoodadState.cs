using System.Diagnostics;
using Fluxor;
using FluxorTest.Models;

namespace FluxorTest.States
{
    public record DoodadListState : GenericListState<Doodad>;

    [FeatureState]
    public class DoodadListFeature : Feature<DoodadListState>
    {
        public override string GetName() => "DoodadList";

        protected override DoodadListState GetInitialState()
        {
            return new DoodadListState
            {
                Initialized = false,
                Loading = false,
                Items = new List<Doodad>()
            };
        }
    }

    #region Actions

    public class DoodadListRetrieveDataAction {}

    public class DoodadListRetrieveDataResultAction 
    {
        public IEnumerable<Doodad> Doodads { get; }

        public DoodadListRetrieveDataResultAction(IEnumerable<Doodad> doodads)
        {
            Doodads = doodads;
        }
    }

    #endregion

    #region Effects

    public class DoodadListEffects
    {
        [EffectMethod]
        public async Task RetrieveDoodadListAsync(DoodadListRetrieveDataAction _, IDispatcher dispatcher)
        {
            var doodads = new List<Doodad>
            {
                new Doodad
                {
                    Id = Guid.NewGuid(),
                    Name = "test123"
                }
            };

            await Task.Delay(TimeSpan.FromSeconds(5));
            dispatcher.Dispatch(new DoodadListRetrieveDataResultAction(doodads));
        }
    }

    #endregion

    #region Reducers

    public static class DoodadReducers
    {
        [ReducerMethod]
        public static DoodadListState ReduceRetrieveDataAction(DoodadListState state,
            DoodadListRetrieveDataAction _)
        {
            Debug.WriteLine($"Executing {nameof(ReduceRetrieveDataAction)}");
            return state with
            {
                Loading = true,
                Items = new List<Doodad>()
            };
        }

        [ReducerMethod]
        public static DoodadListState ReduceRetrieveDataResultAction(DoodadListState state,
            DoodadListRetrieveDataResultAction action)
        {
            Debug.WriteLine($"Executing {nameof(ReduceRetrieveDataResultAction)}");
            return state with
            {
                Loading = false,
                Items = action.Doodads.ToList(),
                Initialized = true
            };
        }
    }

    #endregion
}
