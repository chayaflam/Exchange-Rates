# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# אם יש פתרון (sln) – נחמד אבל לא חובה
# COPY ExchangeRates.sln ./

# מעתיקים csproj של כל הפרויקטים לפי תיקיות
COPY ExchangeRates.PL/ExchangeRates.PL.csproj ExchangeRates.PL/
COPY ExchangeRates.BL/ExchangeRates.BL.csproj ExchangeRates.BL/
COPY ExchangeRates.DAL/ExchangeRates.DAL.csproj ExchangeRates.DAL/

# restore לפרויקט ה-Web (יסגור גם על התלויות)
RUN dotnet restore ExchangeRates.PL/ExchangeRates.PL.csproj

# עכשיו מעתיקים את כל שאר הסורסים (מה-context)
COPY . .

# publish לפרויקט ה-Web
RUN dotnet publish ExchangeRates.PL/ExchangeRates.PL.csproj -c Release -o /app/publish

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:7255
EXPOSE 7255

ENTRYPOINT ["dotnet", "ExchangeRates.PL.dll"]
