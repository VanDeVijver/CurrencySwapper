# Currency Swapper

> Demo app to recap Unit Testing and learn Mocking with Moq

<br/>

## Use the branches

-   ### No separation, no tests
    Untestable version no separation of concerns.<br />
    Available as a MVC, WPF and Console application

    Branch: `demo/1-no-separation` 

-   ### Basic separation, basic Unit Tests
    Splits business logic form UI, making the logic testable.
    
    Branch: `demo/2-basic-separation` 

    
-   ### Mocked dependencies in Unit Tests
    Extends the conversion to retrieve (live) exchange rates from any provider.<br />
    This creates a dependency which will need to be mocked.

    Branch: `demo/3-extend-conversion` 

    - View [API documentation](https://apilayer.com/marketplace/exchangerates_data-api). 
    - [API key](https://apilayer.com/marketplace/exchangerates_data-api?preview=true#pricing) should be provided (appsettings.json, Visual Studio user secrets, ...)
    - Using Visual Studio user secrets

      Right click project **Howest.Prog.Cia.CurrencySwapper.Infrastructure**, *Manage User Secrets* 

        Provide key in *secrets.json*:

        ```json
        { 
            "Howest.Prog.Cia.CurrencySwapper.Infrastructure.Realtime.KeyConfig.ConfigurationOptions": {
            "ApiLayerApiKey": "YOUR KEY HERE"
            }
        }
        ```

