# JWT Authentication ASP.NET Core Web API
A basic Authentication API with Json Web Token, practice program by [Jayharron Abejar](https://jayharronabejar.info)

### Run the app
If you are using Visual Studio code, just run the app directly in IIS Express

### Login
Since this is a simple app, we just hard coded the user data, let's login using Jay's credential. To test the API, use POSTMAN or any API testing tools
```
POST https://localhost:7079/Auth/Login
BODY
username="jay"
password="123"
```
Once the request is authenticated, it will return a response
```
RESPONSE BODY
id=1
name="Jayharron Abejar"
token="eyJhbGciOiJIU....-TY_srNjWqhN"
```

### Get All user
To test if the token is validated, I have created a route that will return the list of users registered in the APP, which in this case, only Jay.
Lets login by adding the token in the request header.
```
GET https://localhost:7079/Auth
HEADER
Authorization="Bearer eyJhbGciOiJIU....-TY_srNjWqhN"
```
If the token is authorized to retrieve the resource in the server, it will return a response.
```json
// RESPONSE BODY (json)
[
    {
        "id": 1,
        "name": "Jayharron Abejar",
        "username": "jay"
    }
]
```