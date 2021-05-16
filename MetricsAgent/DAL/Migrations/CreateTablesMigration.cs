using FluentMigrator;

namespace MetricsAgent.DAL.Migrations
{
    [Migration(20210516140000)]
    public class CreateTablesMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cpu_metrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            
            Create.Table("dot_net_metrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            
            Create.Table("hdd_metrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt64()
                .WithColumn("Time").AsInt64();
            
            Create.Table("network_metrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();
            
            Create.Table("ram_metrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt64()
                .WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
            Delete.Table("cpu_metrics");
            Delete.Table("dot_net_metrics");
            Delete.Table("hdd_metrics");
            Delete.Table("network_metrics");
            Delete.Table("ram_metrics");
        }
    }
}