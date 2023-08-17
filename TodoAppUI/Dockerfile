# Use an official Nginx image as the base image
FROM node:20-alpine3.17 as build

RUN mkdir -p /app

WORKDIR /app

COPY package.json /app/

RUN npm install

COPY . /app/
RUN npm run build --prod

FROM nginx
COPY --from=build /app/dist/todo-app-ui /usr/share/nginx/html

