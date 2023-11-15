FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

ENV APP_HOME /opt/app
RUN mkdir $APP_HOME
WORKDIR $APP_HOME

COPY PricingService.Api/*.csproj $APP_HOME/PricingService.Api/
COPY OrderService.Api/*.csproj $APP_HOME/OrderService.Api/
COPY OrderService/*.csproj $APP_HOME/OrderService/
RUN cd $APP_HOME/OrderService && dotnet restore

COPY PricingService.Api $APP_HOME/PricingService.Api/
COPY OrderService.Api $APP_HOME/OrderService.Api/
COPY OrderService $APP_HOME/OrderService/
RUN cd OrderService && dotnet build

COPY OrderService.Test/*.csproj $APP_HOME/OrderService.Test/
RUN cd $APP_HOME/OrderService.Test && dotnet restore

COPY OrderService.Test ./order-service/OrderService.Test/
RUN cd $APP_HOME/OrderService.Test && dotnet build

FROM build AS test
WORKDIR $APP_HOME/OrderService.Test
RUN dotnet test --verbosity:normal

FROM build AS publish
WORKDIR $APP_HOME/OrderService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5050
ENV ASPNETCORE_ENVIRONMENT=docker
COPY --from=publish /opt/app/OrderService/out ./
ENTRYPOINT ["dotnet", "OrderService.dll"]