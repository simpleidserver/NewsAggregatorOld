using Microsoft.ML.Data;

namespace Microsoft.ML.Trainers
{
    public class HierarchicalClusteringPredictionTransformer : IPredictionTransformer<HierarchicalModelParameters>
    {
        public HierarchicalClusteringPredictionTransformer(HierarchicalModelParameters model)
        {
            Model = model;
        }

        public HierarchicalModelParameters Model { get; }
        public bool IsRowToRowMapper => false;

        public DataViewSchema GetOutputSchema(DataViewSchema inputSchema)
        {
            return null;
        }

        public IRowToRowMapper GetRowToRowMapper(DataViewSchema inputSchema)
        {
            return null;
        }

        public void Save(ModelSaveContext ctx)
        {

        }

        public IDataView Transform(IDataView input)
        {
            return null;
        }
    }
}
