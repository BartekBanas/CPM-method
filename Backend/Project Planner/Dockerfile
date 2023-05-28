FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProjectPlanner.Application/ProjectPlanner.Application.csproj", "ProjectPlanner.Application/"]
COPY ["ProjectPlanner.Application.Services/ProjectPlanner.Application.Services.csproj", "ProjectPlanner.Application.Services/"]
COPY ["ProjectPlanner.Business.CriticalPathMethod/ProjectPlanner.Business.CriticalPathMethod.csproj", "ProjectPlanner.Business.CriticalPathMethod/"]
COPY ["ProjectPlanner.Business.TransportationProblem/ProjectPlanner.Business.TransportationProblem.csproj", "ProjectPlanner.Business.TransportationProblem/"]
COPY ["ProjectPlanner.Business.Validation/ProjectPlanner.Business.Validation.csproj", "ProjectPlanner.Business.Validation/"]
COPY ["ProjectPlanner.Application.Controllers/ProjectPlanner.Application.Controllers.csproj", "ProjectPlanner.Application.Controllers/"]
RUN dotnet restore "ProjectPlanner.Application/ProjectPlanner.Application.csproj"
COPY . .
WORKDIR "/src/ProjectPlanner.Application"
RUN dotnet build "ProjectPlanner.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectPlanner.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectPlanner.Application.dll"]
