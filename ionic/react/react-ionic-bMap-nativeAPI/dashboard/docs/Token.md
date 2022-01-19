# Token Agreement
Consider security issues, we prepare to add token in http.
## Request
 * Method: ```POST```
 * URL: ```http://x.x.x.x:5000/api/users/authenticate```
 * Headers:  
     ```json
     {
        "Content-Type": "application/json"
     }
    ```
 * Body:
    ```json
    {
        "userName":"xxxxxxxx",
        "password":"xxxxxxxx"
    }
     ```

## Respone
```json
{
    "data":
    {
        "flag":true,         //true: exist;  false: non-existent
        "access_token": "ASDHAKJSHDAxx.SDFSLKDFJSLFX.ASFADS" //exist: token; non-existent:null
    }
}
```