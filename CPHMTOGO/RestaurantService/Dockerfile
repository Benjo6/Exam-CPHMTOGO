FROM node:16

# Create app directory
WORKDIR /usr/src/app

# ENV & ARG
ARG DATABASE_HOST
ARG DATABASE_PORT
ARG DATABASE_USER
ARG DATABASE_PASSWORD
ARG PORT

ENV PORT=${PORT}
ENV DATABASE_URL=postgresql://${DATABASE_USER}:${DATABASE_PASSWORD}@${DATABASE_HOST}:${DATABASE_PORT}/

# Bundle app source
COPY RestaurantService/ .

# Install app dependencies
RUN npm install

EXPOSE 5006