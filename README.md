# NewsAggregator

## Prerequisistes

The following packages must be installed on your local machine:
* RabbitMQ (dashboard is accessible via this url : http://localhost:15672/, the login and password are `guest`).
* NodeJS
* DOTNET5.0

## Launch the application

1. Launch the website 'NewsAggregator.Website' : `npm run start` 
2. Launch the OPENID provider 'NewsAggregator.OpenId.Startup' : `dotnet run`
3. Launch the REST.API 'NewsAggregator.Api.Startup': `dotnet run`
4. Launch the hangfire job 'NewsAggregator.ML.Startup' : `dotnet run`