From node
WORKDIR /user-service
COPY . . 
RUN yarn
RUN npx prisma migrate dev --name init
RUN yarn test
RUN yarn start