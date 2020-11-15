FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "Microservices.sln" "Microservices.sln"

COPY "Libs/LoggingUtils/LoggingUtils.csproj" "Libs/LoggingUtils/LoggingUtils.csproj"
COPY "Libs/DatabaseUtils/DatabaseUtils.csproj" "Libs/DatabaseUtils/DatabaseUtils.csproj"
COPY "Libs/ValidationUtils/ValidationUtils.csproj" "Libs/ValidationUtils/ValidationUtils.csproj"
COPY "JobService/JobService.csproj" "JobService/JobService.csproj"
COPY "CreditService/CreditService.csproj" "CreditService/CreditService.csproj"
COPY "WorkerService/WorkerService.csproj" "WorkerService/WorkerService.csproj"

RUN dotnet restore "Microservices.sln"

# Copy everything else and build
COPY . .
WORKDIR /src/CreditService
RUN dotnet publish --no-restore -c Release -o /app -r linux-x64

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CreditService.dll"]
