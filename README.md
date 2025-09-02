Exchange Rates Website

A full-stack sample project consisting of:

Frontend: React (Vite) app using TanStack Table for sortable exchange rate views.

Backend: ASP.NET Core API fetching data from FreeCurrencyAPI
.

Deployment: Dockerized with docker-compose to run client and server together.

Prerequisites

Docker Desktop (or Docker Engine + Compose)

For local dev (optional, without Docker):

.NET 8 SDK

Node.js 18+ and npm/pnpm

Environment Variables

The project uses environment variables defined in a .env file (ignored by git).
⚠️ Do not commit real API keys.

Example .env:

# Server
FreeCurrencyApi__ApiKey=YOUR-REAL-API-KEY
SERVER_PORT=7255
CORS_ALLOWED_ORIGINS=http://localhost:5173

# Client
CLIENT_PORT=5173
VITE_API_BASE_URL=http://localhost:7255


Notes:

ASP.NET Core supports nested config via __ (double underscores).

The API key must be passed as FreeCurrencyApi__ApiKey.

Client must only know the backend base URL (VITE_API_BASE_URL). API keys must never be exposed in client .env.

Local Development
Backend (without Docker)
cd server
dotnet restore
dotnet user-secrets init
dotnet user-secrets set "FreeCurrencyApi:ApiKey" "YOUR-REAL-API-KEY"
dotnet run


Runs on http://localhost:7255
.

Frontend (without Docker)
cd client
npm install
echo "VITE_API_BASE_URL=http://localhost:7255" > .env.local
npm run dev


Runs on http://localhost:5173
.

Running with Docker
docker compose up --build


#Services:

Client → http://localhost:5173

Server → http://localhost:7255/swagger

#API Endpoints

Currencies list: GET /currencies

Exchange rates: GET /rates?base=USD



Security Notes

Never commit .env or secrets to git.

Client .env (VITE_ vars) is public at build time. Do not store API keys there.

In production, configure secrets via environment variables (CI/CD, Docker, or cloud secrets manager).
