# netcore2-Auth0
This is a .NET Core 2 sample project showing how to secure Web APIs access by using Auth0 services.

The project defines a Web API application whose authorization is based on Auth0 authorization services.


## Running the project ##


The solution contains a _Test_ project with three integration tests validating the application behaviour.
You can run the tests from Visual Studio 2017 or by typing `dotnet test` in a command window.

If you want to interactively test the application, you can use [Postman](https://www.getpostman.com/) or any other Http client.

1. Run the project from Visual Studio 2017 or by typing `dotnet run` in a command window
2. Launch _Postman_ and make a GET request as follows:

```
    GET http://localhost:63939/api/books HTTP/1.1
    cache-control: no-cache
    Accept: */*
    Host: localhost:63939
    accept-encoding: gzip, deflate
    Connection: keep-alive
```

This should return a 401 HTTP status code (_Unauthorized_)

3. Make a POST request like the following:

```
    POST http://localhost:63939/api/token HTTP/1.1
    cache-control: no-cache
    Content-Type: application/json
    Accept: */*
    Host: localhost:63939
    accept-encoding: gzip, deflate
    Connection: keep-alive
    
{"client_id":"YOUR_CLIENT_ID","client_secret":"YOUR_CLIENT_SECRET","audience":"http://localhost:63939/","grant_type":"client_credentials"}
```

It returns a JSON object like the following:

```
{
    "access_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlEwRXhOamMzUXpCRE9VSkZSRVJCUTBNME1UY3dRekl6TkRkQ1F6WTVRVGMzUXpBNFJrUkVOZyJ9.eyJpc3MiOiJodHRwczovL2FuZHljaGlhcmUuZXUuYXV0aDAuY29tLyIsInN1YiI6InFBUjBjckRGTWhXdTI2T2hmU2M5eTNIQ2pzU1RBUEdNQGNsaWVudHMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYzOTM5LyIsImlhdCI6MTUxNjE3MTA5MywiZXhwIjoxNTE2MjU3NDkzLCJhenAiOiJxQVIwY3JERk1oV3UyNk9oZlNjOXkzSENqc1NUQVBHTSIsInNjb3BlIjoicHJvZmlsZSBvcGVuaWQiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.eyT7tEJluAz3Pb1h64kgvYtGpmi81BFBB5UvsirPfkoCtOzJNgMFmH-nk8kTi5n_IxQH3GexJM5qUdlvvfSc5QY_-fZ0JHX7tCYfBVf0xTtWz5gyiSgIkk8HMfBQqh2juQLWcxVImz79LmCULzDa5j3uYgyBYiPPr0vv5-gRjfMmuLtvpQu329oL8-7FXS2Bun6t7q6MSSL7jfTp_yRIF4T3cOTvtK6n9KtP7854Gks00ecnIbIqdmD_IoYYDGoxzxCAYFU5mI7TjjtP5avMy-uKoz05fR-XIsMsYJJYin3xgsMqk15aybwuJy1wOHJcLPS3J4kCKmP-XgVSEAxDgA",
    "expires_in": 86400,
    "token_type": "Bearer"
}
```

4. The following GET request

```
    GET http://localhost:63939/api/books HTTP/1.1
    cache-control: no-cache
    Authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IlEwRXhOamMzUXpCRE9VSkZSRVJCUTBNME1UY3dRekl6TkRkQ1F6WTVRVGMzUXpBNFJrUkVOZyJ9.eyJpc3MiOiJodHRwczovL2FuZHljaGlhcmUuZXUuYXV0aDAuY29tLyIsInN1YiI6InFBUjBjckRGTWhXdTI2T2hmU2M5eTNIQ2pzU1RBUEdNQGNsaWVudHMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYzOTM5LyIsImlhdCI6MTUxNjE3MTA5MywiZXhwIjoxNTE2MjU3NDkzLCJhenAiOiJxQVIwY3JERk1oV3UyNk9oZlNjOXkzSENqc1NUQVBHTSIsInNjb3BlIjoicHJvZmlsZSBvcGVuaWQiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.eyT7tEJluAz3Pb1h64kgvYtGpmi81BFBB5UvsirPfkoCtOzJNgMFmH-nk8kTi5n_IxQH3GexJM5qUdlvvfSc5QY_-fZ0JHX7tCYfBVf0xTtWz5gyiSgIkk8HMfBQqh2juQLWcxVImz79LmCULzDa5j3uYgyBYiPPr0vv5-gRjfMmuLtvpQu329oL8-7FXS2Bun6t7q6MSSL7jfTp_yRIF4T3cOTvtK6n9KtP7854Gks00ecnIbIqdmD_IoYYDGoxzxCAYFU5mI7TjjtP5avMy-uKoz05fR-XIsMsYJJYin3xgsMqk15aybwuJy1wOHJcLPS3J4kCKmP-XgVSEAxDgA
    Accept: */*
    Host: localhost:63939
    accept-encoding: gzip, deflate
    Connection: keep-alive
```

returns the following response:

```
	[
	    {
	        "author": "Ray Bradbury",
	        "title": "Fahrenheit 451",
			"ageRestriction": false
	    },
	    {
	        "author": "Gabriel García Márquez",
	        "title": "One Hundred years of Solitude",
			"ageRestriction": false
	    },
	    {
	        "author": "George Orwell",
	        "title": "1984",
			"ageRestriction": false
	    },
	    {
	        "author": "Anais Nin",
	        "title": "Delta of Venus",
			"ageRestriction": true
	    }
	]
```

