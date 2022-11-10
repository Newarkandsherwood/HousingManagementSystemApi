# HousingManagementSystemApi
Housing Management System API

## Configuration

If UNIVERSAL_HOUSING_CONNECTION_STRING is not set in Properties/launchSettings.json, 
Set environment variable `UNIVERSAL_HOUSING_CONNECTION_STRING` with a value of a connection string to Universal Housing database, i.e.
```
export UNIVERSAL_HOUSING_CONNECTION_STRING='Server=universal.lan;User Id=HousingRepairsOnline;Password=MyStrongPassword;Database=universalHousingDb'
```
This environment variable is mandatory and not setting it will cause the application to not start. 

The following environment variables should be set up in Properties/launchSettings.json:
``` 
        "AUTHENTICATION_IDENTIFIER": "<REPLACE WITH AUTHENTICATION_IDENTIFIER>",
        "JWT_SECRET" : "<REPLACE WITH A SECRET>",
        "UNIVERSAL_HOUSING_CONNECTION_STRING": "<REPLACE WITH A CONNECTION STRING TO UNIVERSAL HOUSING>",
        "SENTRY_DSN": "<REPLACE WITH SENTRY_DSN>",
        "COSMOS_TENANT_CONTAINER_ID": "<REPLACE WITH COSMOS_TENANT_CONTAINER_ID>",
        "COSMOS_COMMUNAL_CONTAINER_ID": "<REPLACE WITH COSMOS_COMMUNAL_CONTAINER_ID>",
        "COSMOS_AUTHORIZATION_KEY": "<REPLACE WITH COSMOS_AUTHORIZATION_KEY>",
        "COSMOS_DATABASE_ID": "<REPLACE WITH COSMOS_DATABASE_ID>",
```

Settings starting with `COSMOS_*` are used to access the address information from Azure CosmosDB. Ensure these values are populated from the correct values taken from the Azure dashboard.

## Running the API locally with other Services
In order to run the API locally, the following steps may need to be taken to exclude some functionality.

Only make the changes necessary to enable you to work on functionality locally and bear in mind the adjustments you make locally when diagnosing issues.
Take care to not commit any of these local-only changes.

`AUTHENTICATION_IDENTIFIER` should be to the same value as the front end and the Housing Management API.



Set applicationUrl to the local URL and ensure that it matches what is expected in the Housing Repairs Online API: 
```
"applicationUrl": "https://localhost:6001;http://localhost:6000",
```


1. In Program.cs, if you do not have Sentry set up, then comment out the Sentry section:
```
        // webBuilder.UseSentry(o =>
        // {
        //     o.Dsn = Environment.GetEnvironmentVariable("SENTRY_DSN");
        //
        //     var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //     if (environment == Environments.Development)
        //     {
        //         o.Debug = true;
        //         o.TracesSampleRate = 1.0;
        //     }
        // });
```

2. In Startup.cs, if you are not using HTTPS locally, disable redirection:
```
        //app.UseHttpsRedirection();
```

3. In Startup.cs, if you are not using Sentry, disable Sentry tracing:
```
        //app.UseSentryTracing();
```
