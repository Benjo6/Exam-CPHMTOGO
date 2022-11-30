From node
WORKDIR /user-service
COPY . . 
RUN yarn
RUN npx prisma generate 
RUN yarn test