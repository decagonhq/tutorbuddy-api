#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TutorBuddyApi/TutorBuddyApi.csproj", "TutorBuddyApi/"]
RUN dotnet restore "TutorBuddyApi/TutorBuddyApi.csproj"
COPY . .
WORKDIR "/src/TutorBuddyApi"
RUN dotnet build "TutorBuddyApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TutorBuddyApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/publish/TutorBuddyApi/StaticFiles/HTML/ConfirmEmail.html ./
COPY --from=publish /app/publish/TutorBuddyApi/StaticFiles/HTML/ForgotPassword.html ./
COPY --from=publish /app/publish/TutorBuddyApi/StaticFiles/SVG/tutorbuddy.svg ./
ENTRYPOINT ["dotnet", "TutorBuddyApi.dll"]