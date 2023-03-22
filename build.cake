#addin nuget:?package=Cake.Docker&version=1.2.0
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var dockerComposePath = "./docker-compose.yaml";

var dockerComposeSettings = new DockerComposeUpSettings
{
    Files = new string[] { dockerComposePath  },
    DetachedMode = true,
    ProjectName = "stocktalk"
};

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////


Task("Database")
.Does(() => {
   Information("Creating SqlServer container...");
   DockerComposeUp(
        dockerComposeSettings,
        new string[]{ "sqlserver" });
   Information("Container created!");
});

Task("RabbiMq")
.Does(() => {
   Information("Creating RabbitMq container...");
   DockerComposeUp(
        dockerComposeSettings,
        new string[]{ "rabbitmq" });
   Information("Container created!");
});

Task("WebApi")
.Does(() => {
   Information("Creating StockTalk webapi container...");
   DockerComposeUp(
        dockerComposeSettings,
        new string[]{ "webapi" });
   Information("Container created!");
});

Task("Worker")
.Does(() => {
   Information("Creating StockTalk worker container...");
   DockerComposeUp(
        dockerComposeSettings,
        new string[]{ "worker" });
   Information("Container created!");
});

Task("Infra")
.IsDependentOn("Database")
.IsDependentOn("RabbiMq")
.Does(() => {
    Information("Creating StockTalk infra containers...");
});

Task("App")
.IsDependentOn("WebApi")
.IsDependentOn("Worker")
.Does(() => {
    Information("Creating StockTalk infra containers...");
});

RunTarget(target);
