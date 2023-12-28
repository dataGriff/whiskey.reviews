
dotnet add package Microsoft.Extensions.Caching.Memory

## Endpoints

Let the HTTP Verbs do the job of describing the action, just describe the entity in the URI path.

| Behaviour  | HTTP Verb  | URI |
|---|---|---|
|  Get distilleries |  GET |  https://api.myurl/v1/distilleries |
|  Get whiskeys|  GET |  https://api.myurl/v1/whiskeys |
|  Whiskey review created |  POST |  https://api.myurl/v1/whiskey/reviews |
|  User gets whiskey reviews |  GET |  https://api.myurl/v1/users/{userId}/whiskeys |
|  User gets a whiskey review |  GET |  https://api.myurl/v1/users/{userId}/whiskeys/{whiskeyId} |
|  User deletes a whiskey review |  DELETE |  https://api.myurl/v1/users/{userId}/whiskeys/{whiskeyId} |
|  User updates whiskey review |  PUT | https://api.myurl/users/{userId}/whiskeys/{whiskeyId} |

## Whiskey Data

* [Distilleries](https://www.thewhiskybarrel.com/distilleries#section-A)
* [Irish Whiskey Distilleries](https://westmeathwhiskeyworld.wordpress.com/irish-whiskey-distilleries/)
* [Scotch Whiskey List](https://www.scotch-whisky.org.uk/media/2144/list-of-current-operating-scotch-whisky-distilleries-for-public-website-october-2023-1.pdf)
* [List of Whiskey Brands!](https://en.wikipedia.org/wiki/List_of_whisky_brands#)
* [Whiskey Distilleries in Scotland](https://en.wikipedia.org/wiki/List_of_whisky_distilleries_in_Scotland)
* [Whiskey Distilleries in Ireland](https://en.wikipedia.org/wiki/Irish_whiskey#Current_distilleries)
* [Whiskey Distilleries in Japan](https://en.wikipedia.org/wiki/Japanese_whisky#Distilleries)
* [Whiskey Distilleries in USA](https://en.wikipedia.org/wiki/Japanese_whisky#Distilleries)

https://localhost:3001/swagger/index.html
http://localhost:8000/swagger/v1/swagger.json
http://localhost:8000/v1/distilleries

```json
{
    "id": "example_id",
    "name": "Example Distillery",
    "wikiLink": "/wiki/Example_Distillery",
    "country": "Country",
    "type": ["Single Malt", "Blended", "Single Pot Still", ...],
    "region": "Region",
    "founded": Year,
    "owner": "Owner Name",
    "coordinates": {
        "latitude": 00.0000,
        "longitude": 00.0000
    }
}

```

distillery type
Single Malt," "Blended," "Single Pot Still," "Grain," "Blended Malt," "Blended Grain," "Single Grain,"
```

## Steps

```bash
mkdir api
cd api
dotnet new webapi -o WhiskeyAPI
```

added to.vscode/launch.json for https

```json
           "launchBrowser": {
                "enabled": true,
                "args": "${auto-detect-url}/swagger",
                "windows": {
                    "command": "cmd.exe",
                    "args": "/C start ${auto-detect-url}/swagger"
                }
            },
```

amended vscode gitignore so include launch settings etc for other devs

```yaml
# Created by https://www.gitignore.io/api/visualstudiocode
# Edit at https://www.gitignore.io/?templates=visualstudiocode

### VisualStudioCode ###
.vscode/*      # Maybe .vscode/**/* instead - see comments
!.vscode/settings.json
!.vscode/tasks.json
!.vscode/launch.json
!.vscode/extensions.json

### VisualStudioCode Patch ###
# Ignore all local history of files
**/.history
# End of https://www.gitignore.io/api/visualstudiocode
```

added docker details using extension
amended launch file to include dockerServerReadyAction so goes to swagger page on launch

```json
        {
            "name": "Docker .NET Launch",
            "type": "docker",
            "request": "launch",
            "preLaunchTask": "docker-run: debug",
            "netCore": {
                "appProject": "${workspaceFolder}/api/WhiskeyAPI/WhiskeyAPI.csproj"
            },
            "dockerServerReadyAction": {                
                "uriFormat": "%s://localhost:%s/swagger"
            } 
        }
```

## Docker

```
docker tag whiskeyapi griff182uk/whiskeyapi
docker push griff182uk/whiskeyapi
```
