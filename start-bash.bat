docker compose -f infrastructure/docker/docker-compose.infrastructure.prod.yml down --remove-orphans
docker compose -f infrastructure/docker/docker-compose.infrastructure.prod.yml up --build -d
