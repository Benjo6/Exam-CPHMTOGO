docker compose -f infrastructure/cphmtogo/docker-compose.infrastructure.prod.yml down --remove-orphans
docker compose -f infrastructure/cphmtogo/docker-compose.infrastructure.prod.yml up --build -d
