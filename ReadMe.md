# Task Monitoring

### About the Project
This project is created using ASP .Net core 7.0 with InMemory Database.
Used to Create, Read, Update and Delete Task.

### Run and Test the project
- Run Build to install packages
- Create appsettings.json first
- Add JWT configuration
  ```
  {
    "JWT": {
      "ValidAudience": "<your valid audience>",
      "ValidIssuer": "<your valid issuer>",
      "SecretKey": "<your secret key>"
    }
  }
  ```

