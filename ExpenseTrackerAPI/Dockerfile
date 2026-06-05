FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ExpenseTrackerAPI.csproj", "."]
RUN dotnet restore "./ExpenseTrackerAPI.csproj"

COPY . .
RUN dotnet publish "ExpenseTrackerAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ExpenseTrackerAPI.dll"]