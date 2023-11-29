# Chat copilot


## Description

Chat Copilot is an innovative chat application built on top of the Semantic Kernel Library. 

## Getting Started

### Dependencies

* [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)  
* [Docker](https://www.docker.com/)
* [Azure OpenAi Service](https://azure.microsoft.com/en-us/products/ai-services/openai-service)

### Installing

* Clone the repository

### Executing program

* Change appsettings.Example.json to appsettings.Development.json
* Replace the placeholder values with your configuration
* Run docker container by executing inside the ChatCopilot directory:  
```
docker-compose build
```
```
docker-compose up
```

## Troubleshooting

1. **Issue with certificate when running docker container:** 
</br>
*System.InvalidOperationException: ‘Unable to configure HTTPS endpoint. No server certificate was specified, and the default developer certificate could not be found or is out of date'* 
</br></br> 
For solution please check: [Link](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-3.1) 



## Authors

Contributors names

* Amadou Coulibaly   
* Hubert Bodek 

## Acknowledgments

* [microsoft chat-copilot](https://github.com/microsoft/chat-copilot)
* [event-reminder](https://github.com/m-jovanovic/event-reminder/tree/main)