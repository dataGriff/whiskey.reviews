
dotnet add package Microsoft.Extensions.Caching.Memory


## Endpoints

Let the HTTP Verbs do the job of describing the action, just describe the entity in the URI path.

| Behaviour  | HTTP Verb  | URI |
|---|---|---|
|  Get distilleries |  GET |  https://api.myurl/distilleries |
|  Whiskey review created |  POST |  https://api.myurl/whiskey/reviews |
|  User gets whiskey reviews |  GET |  https://api.myurl/users/{userId}/whiskeys |
|  User gets a whiskey review |  GET |  https://api.myurl/users/{userId}/whiskeys/{whiskeyId} |
|  User deletes a whiskey review |  DELETE |  https://api.myurl/users/{userId}/whiskeys/{whiskeyId} |
|  User updates whiskey review |  PUT | https://api.myurl/users/{userId}/whiskeys/{whiskeyId} |

## Whiskey Data

* [List of Whiskey Brands!](https://en.wikipedia.org/wiki/List_of_whisky_brands#)
* [Whiskey Distilleries in Scotland](https://en.wikipedia.org/wiki/List_of_whisky_distilleries_in_Scotland)
* [Whiskey Distilleries in Ireland](https://en.wikipedia.org/wiki/Irish_whiskey#Current_distilleries)
* [Whiskey Distilleries in Japan](https://en.wikipedia.org/wiki/Japanese_whisky#Distilleries)
* [Whiskey Distilleries in USA](https://en.wikipedia.org/wiki/Japanese_whisky#Distilleries)

https://localhost:3001/swagger/index.html