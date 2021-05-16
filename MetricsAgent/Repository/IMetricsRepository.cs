using System.Data.Common;

namespace MetricsAgent.Repository
{
    public interface IMetricsRepository
    {
        void CreateTable();
        void DropTable();
    }
}